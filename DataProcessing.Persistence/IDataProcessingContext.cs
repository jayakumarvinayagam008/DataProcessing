using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing.Persistence
{
    public interface IDataProcessingContext
    {
        IMongoCollection<BusinessToBusiness> BusinessToBusiness { get; }
        IMongoCollection<BisinessCategory> BisinessCategories { get; }
    }
}
