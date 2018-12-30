namespace DataProcessing.Core.Web.Models
{
    public class NumberLookup
    {
        public string Operator { get; set; }
        public string Circle { get; set; }
        public string Series { get; set; }
        public bool IsUploaded { get; set; }
        public string UploadMessage { get; set; }
    }
}