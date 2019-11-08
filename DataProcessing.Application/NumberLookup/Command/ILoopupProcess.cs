using System;
using System.Collections.Generic;

namespace DataProcessing.Application.NumberLookup.Command
{
    public interface ILoopupProcess
    {
        (IEnumerable<string>, string) Process(string lookupFile, string rootPath, string content);
    }
}