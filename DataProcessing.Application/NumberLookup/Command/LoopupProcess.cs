using DataProcessing.Application.NumberLookup.Query;
using System;
using System.Collections.Generic;
using System.Text;

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
        public string Process(string lookupFile, string rootPath)
        {
            var lookup = _readNumberLookup.Read(lookupFile);
            var sourceLookup = _getNumberLoopUpData.FilterNumberLookUp(lookup);
            var fileName = _saveNumberLookUp.CreateAndSave(sourceLookup, rootPath);
            return fileName;
        }
    }
}
