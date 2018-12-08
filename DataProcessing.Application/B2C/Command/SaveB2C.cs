using System;
using DataProcessing.CommonModels;

namespace DataProcessing.Application.B2C.Command
{
    public class SaveB2C:ISaveB2C
    {
        public SaveB2C()
        {
        }

        public UploadSummary Save(string filePath)
        {
            return new UploadSummary();
        }
    }
}
