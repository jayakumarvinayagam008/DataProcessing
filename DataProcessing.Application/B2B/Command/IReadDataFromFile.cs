using DataProcessing.Application.B2B.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.B2B.Command
{
    public interface IReadDataFromFile
    {
        (IEnumerable<BusinessToBusinesModel>, int) ReadFileData(string filePath);
    }
}
