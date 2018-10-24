namespace Dtc.Log.Data
{
    public class LogFileWithState
    {
        public readonly string FileName;
        public readonly LogState LogState;

        public LogFileWithState(string fileName, LogState logState)
        {
            FileName = fileName;
            LogState = logState;
        }

    }
}
