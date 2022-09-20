using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using PaymentFunctionApp.DatabaseEntity;
using System.Data;

namespace PaymentFunctionApp
{
    public static class Function1
    {
        [FunctionName("CreatePurchase")]
        public static async Task<IActionResult> CreateTask(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "purchase")] HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<Purchase>(requestBody);
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var selquery = @"Select * from [Purchase] Where EmailId = @email and BookId=@bookId";
                    SqlCommand selcommand = new SqlCommand(selquery, connection);
                    selcommand.Parameters.AddWithValue("@email", input.EmailId);
                    selcommand.Parameters.AddWithValue("@bookId", input.BookId);
                    SqlDataAdapter da = new SqlDataAdapter(selcommand);
                    da.Fill(dt);
                    //if (!String.IsNullOrEmpty(input.EmailId))
                    if (dt.Rows.Count==0)
                    {
                        var PurchaseDate = DateTime.Now.ToString("yyyy-MM-dd");
                        //purchase.PaymentMode = "Online";
                        var query = $"INSERT INTO [Purchase] (EmailId,BookId,PurchaseDate,PaymentMode) VALUES('{input.EmailId}' , '{input.BookId}' , '{PurchaseDate}' , '{input.PaymentMode}')";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
                return new BadRequestResult();
            }
            return new OkResult();
        }
    }
}
