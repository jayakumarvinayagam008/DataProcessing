namespace DataProcessing.CommonModels
{
    public class FileAvailable
    {
        public bool IsAvailable { get; set; }
        public string Message { get; set; }
    }

    public class SearchRequestCheck
    {
        public string SearchId { get; set; }
        public string Type { get; set; }
    }
}