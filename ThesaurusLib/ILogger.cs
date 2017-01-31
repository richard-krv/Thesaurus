namespace Ric.ThesaurusLib
{
    public interface ILogger
    {
        void AddLogEntry(string format, params object[] args);
    }
}
