namespace DataProcessing.Core.Web.Models
{
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}