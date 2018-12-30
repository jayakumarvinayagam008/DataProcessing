using System.Collections.Generic;

namespace DataProcessing.Application.NumberLookup.Command
{
    public interface IReadNumberLookup
    {
        IEnumerable<Numbers> Read(string filePath);
    }
}