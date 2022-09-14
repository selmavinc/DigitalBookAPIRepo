using System.Diagnostics.Metrics;

namespace UserRegistartion.Controllers
{
    public class DbCredentials
    {
        public string password { get; set; }
        public string userName { get; set; }
        public string Token { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}