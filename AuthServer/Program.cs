using AuthServer.DatabaseEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITokenService>(new TokenService());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost("/validate", [AllowAnonymous] (UserValidationModel request, HttpContext http, ITokenService tokenService) =>
{
    if (request.validateCredential(request.userName,request.password))
    {
        var token = tokenService.BuildToken(builder.Configuration["Jwt:Key"],
                                            builder.Configuration["Jwt:Issuer"],
                                            new[]
                                            {
                                                        builder.Configuration["Jwt:Aud1"],
                                                        builder.Configuration["Jwt:Aud2"],
                                                        builder.Configuration["Jwt:Aud3"]
                                                    },
                                            request.userName);
        return new
        {
            Token = token,
            IsAuthenticated = true,
            username=request.userName,
            Role= request.GetRole(request.userName),
            user= request.GetUser(request.userName),
            emailID=request.GetEmailID(request.userName),
        };
    }
    return new
    {
        Token = string.Empty,
        IsAuthenticated = false,
        username = string.Empty,
        Role = string.Empty,
        user=0,
        emailID=string.Empty,
    };
})
.WithName("Validate");

app.Run();

//internal record UserValidationRequestModel([Required] string UserName, [Required] string Password);

internal interface ITokenService
{
    string BuildToken(string key, string issuer, IEnumerable<string> audience, string userName);
}

internal class TokenService : ITokenService
{
    private TimeSpan ExpiryDuration = new TimeSpan(20, 30, 0);
    public string BuildToken(string key, string issuer, IEnumerable<string> audience, string userName)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, userName),
        };
        claims.AddRange(audience.Select(aud => new Claim(JwtRegisteredClaimNames.Aud, aud)));
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
            expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}
