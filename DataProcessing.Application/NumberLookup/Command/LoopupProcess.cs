using DataProcessing.Application.NumberLookup.Query;
using DataProcessing.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataProcessing.Application.NumberLookup.Command
{
    public class LoopupProcess : ILoopupProcess
    {
        private readonly IReadNumberLookup _readNumberLookup = null;
        private readonly IGetNumberLoopUpData _getNumberLoopUpData = null;
        private readonly ISaveNumberLookUp _saveNumberLookUp = null;
        private readonly ISaveNumberLookupResult _saveNumberLookupResult = null;
        public LoopupProcess(IReadNumberLookup readNumberLookup, IGetNumberLoopUpData getNumberLoopUpData,
            ISaveNumberLookUp saveNumberLookUp, ISaveNumberLookupResult saveNumberLookupResult)
        {
            _readNumberLookup = readNumberLookup;
            _getNumberLoopUpData = getNumberLoopUpData;
            _saveNumberLookUp = saveNumberLookUp;
            _saveNumberLookupResult = saveNumberLookupResult;
        }

        public (IEnumerable<string>, string) Process(string lookupFile, string rootPath,string content)
        {
            IEnumerable<Numbers> lookup = new List<Numbers>();
            if(!string.IsNullOrWhiteSpace(lookupFile))
            {
                lookup = _readNumberLookup.Read(lookupFile);
            }            
            var lookupfromText = _readNumberLookup.ReadFromContent(content);
            lookup = lookup.Concat(lookupfromText).Distinct();
            var sourceLookup = _getNumberLoopUpData.FilterNumberLookUp(lookup);
            // filter 
            IList<NumberLookupResult> numberLookupResults = new List<NumberLookupResult>();
            string searchId = GetGUID();
            foreach (var item in sourceLookup)
            {
                numberLookupResults.Add(new NumberLookupResult { 
                    Circle= item.Circle,
                    Operator = item.Operator,
                    Phone = item.Phone,                
                    SearchId = searchId,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now
                });
            }            
            _saveNumberLookupResult.SaveResult(numberLookupResults);
            (IEnumerable<string>, string) operators = (sourceLookup.Select(x => x.Operator).Distinct(), searchId);

            //sourceLookup = sourceLookup.Where(x => filters.Contains(x.Operator));
            //var fileName = _saveNumberLookUp.CreateAndSave(sourceLookup, rootPath);
            return operators;
        }

        private string GetGUID()
        {
            Guid guid = Guid.NewGuid();
            return $"{guid.ToString()}{DateTime.Now.ToString("yyyyMMddhhmmss")}";
        }
    }
}