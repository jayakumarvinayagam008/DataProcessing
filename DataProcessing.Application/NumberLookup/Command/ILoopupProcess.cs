using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Application.NumberLookup.Command
{
    public interface ILoopupProcess
    {
        string Process(string lookupFile, string rootPath);
    }
}
