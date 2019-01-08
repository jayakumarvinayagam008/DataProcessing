using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataProcessing.Application.Common
{
    public static class GetSampleFileContent
    {
        public static SampleTemplate Get(string fileSource, string[] samples, int sourceId)
        {
            var sampleUplaod = new SampleTemplate();
            var sampleSource = samples.Where(x => x.Split('_')[1] == $"{sourceId}").FirstOrDefault();
            if (sampleSource != null )
            {
                var fileName = sampleSource.Split('_')[0];
                string fileSourcePath = $"{fileSource}{fileName}.xlsx";
                byte[] fileBytes = System.IO.File.ReadAllBytes(fileSourcePath);
                sampleUplaod = new SampleTemplate
                {
                    content = fileBytes,
                    FileName = fileName
                };
            }           
            return sampleUplaod;
        }
    }
}
