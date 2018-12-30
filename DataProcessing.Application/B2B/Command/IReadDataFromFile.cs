using DataProcessing.Application.B2B.Common;
using System.Collections.Generic;

namespace DataProcessing.Application.B2B.Command
{
    public interface IReadDataFromFile
    {
        (IEnumerable<BusinessToBusinesModel>, int) ReadFileData(string filePath);
    }
}