using System;
using System.IO;
using System.IO.Compression;

namespace DataProcessing.Core.Web.Controllers
{
    public class GetFileContent
    {
        public byte[] GetFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                var folderName = Path.ChangeExtension(filePath, null);
                if(Directory.Exists(folderName))
                {
                    // create zip
                    IZipImplementation zipImplementation = new ZipImplementation();
                    filePath = $"{folderName}.zip";
                    zipImplementation.CreateZipFile(folderName, filePath);
                    // update file path
                }
            }
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return fileBytes;
        }
    }


    public interface IZipImplementation
    {
        string CreateZipFile(string sourceFolder, string filePath);        
    }
    public class ZipImplementation : IZipImplementation
    {
        public string CreateZipFile(string sourceFolder, string filePath)
        {
            try
            {
                ZipFile.CreateFromDirectory(sourceFolder, filePath);
                return filePath;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }

    }
/*
 public interface IZipImplementation
    {
        string CreateZipFile(string sourceFolder, string filePath);
        string UnZipFile(string zipPath, string extractPath);
    }
    public class ZipImplementation : IZipImplementation
    {   
        public string CreateZipFile(string sourceFolder, string filePath)
        {
            try
            {
                ZipFile.CreateFromDirectory(sourceFolder, filePath);
                return filePath;
            }
            catch (Exception)
            {
                return string.Empty;
            }            
        }

        public string UnZipFile(string zipPath, string extractPath)
        {
            try
            {
                ZipFile.ExtractToDirectory(zipPath, extractPath);
                return extractPath;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
     */
