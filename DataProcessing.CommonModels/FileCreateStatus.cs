namespace DataProcessing.CommonModels
{
    public enum FileCreateStatus
    {
        Started = 0,
        InProgress = 1,
        Completed = 2
    }

    public static class MessageContainer
    {
        public static string SearchFile = "Download request in progress, please try again.";
    }
}