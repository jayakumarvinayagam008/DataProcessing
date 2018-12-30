namespace DataProcessing.Application.NumberLookup.Command
{
    public interface ILoopupProcess
    {
        string Process(string lookupFile, string rootPath);
    }
}