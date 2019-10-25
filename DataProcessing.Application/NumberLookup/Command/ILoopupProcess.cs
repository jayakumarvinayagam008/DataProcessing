using System.Collections.Generic;

namespace DataProcessing.Application.NumberLookup.Command
{
    public interface ILoopupProcess
    {
        string Process(string lookupFile, string rootPath, string content, IEnumerable<string> filters);
    }
}