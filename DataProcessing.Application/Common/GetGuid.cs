using System;

namespace DataProcessing.Application.Common
{
    public static class GetGuid
    {
        public static string Get()
        {
            Guid guid = Guid.NewGuid();
            return $"{guid.ToString()}{DateTime.Now.ToString("yyyyMMddhhmmss")}";
        }
    }
}