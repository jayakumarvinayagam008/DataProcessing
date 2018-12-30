namespace DataProcessing.Core.Web.Controllers
{
    public class GetFileContent
    {
        public byte[] GetFile(string filePath)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return fileBytes;
        }
    }
}