namespace DataProcessing.CommonModels
{
    public static class DownloadTemplateType
    {
        private static string[] tempalteType = new string[] {
            "Business2Business",
            "Business2Customer",
            "CustomerData"
        };

        public static string GetTemplateName(int index) => tempalteType[index];
    }
}