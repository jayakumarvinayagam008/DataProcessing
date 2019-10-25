using DataProcessing.Application.NumberLookup.Query;
using System.Collections.Generic;
using System.Linq;

namespace DataProcessing.Application.NumberLookup.Command
{
    public class LoopupProcess : ILoopupProcess
    {
        private readonly IReadNumberLookup _readNumberLookup = null;
        private readonly IGetNumberLoopUpData _getNumberLoopUpData = null;
        private readonly ISaveNumberLookUp _saveNumberLookUp = null;

        public LoopupProcess(IReadNumberLookup readNumberLookup, IGetNumberLoopUpData getNumberLoopUpData,
            ISaveNumberLookUp saveNumberLookUp)
        {
            _readNumberLookup = readNumberLookup;
            _getNumberLoopUpData = getNumberLoopUpData;
            _saveNumberLookUp = saveNumberLookUp;
        }

        public string Process(string lookupFile, string rootPath,string content, IEnumerable<string> filters)
        {
            var lookup = _readNumberLookup.Read(lookupFile);
            var lookupfromText = _readNumberLookup.ReadFromContent(content);
            lookup = lookup.Concat(lookupfromText).Distinct();
            var sourceLookup = _getNumberLoopUpData.FilterNumberLookUp(lookup);
            // filter 
            sourceLookup = sourceLookup.Where(x => filters.Contains(x.Operator));
            var fileName = _saveNumberLookUp.CreateAndSave(sourceLookup, rootPath);
            return fileName;
        }
    }
}