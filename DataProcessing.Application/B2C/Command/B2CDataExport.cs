using System;
using System.Collections.Generic;
using System.Text;
using DataProcessing.Application.Common;
using DataProcessing.Persistence;

namespace DataProcessing.Application.B2C.Command
{
    public class B2CDataExport : IB2CDataExport
    {
        public string Export(List<BusinessToCustomer> businessToBusinesses, string fileRootPath, int range)
        {
            var fileName = GetGuid.Get();
            string fileType = "xlsx";
            string fileCsvType = "csv";
            var filePath = $"{fileRootPath}{fileName}.{fileType}";
            var fileCsvPath = $"{fileRootPath}{fileName}.{fileCsvType}";

            return fileName;
        }
    }
}
