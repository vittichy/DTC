using Dtc.Log.Base;
using Dtc.Log.Data;
using System;

namespace Dtc.Log
{
    public class FileLog : LogBase
    {
        public void SetFileName(string logFileName)
        {
            _logFileName = logFileName;
        }


        public void SetFileName(string fileName, LogState state)
        {
            _logFileNames.RemoveAll(p => p.LogState == state);
            _logFileNames.Add(new LogFileWithState(fileName, state));
        }


        public FileLog(Type declaringType, string logFileName = null, bool writeStartInfo = true, bool writeListOfLoadedDll = true) 
            : base(declaringType, logFileName, writeStartInfo, writeListOfLoadedDll: writeListOfLoadedDll)
        {
        }
    }
}
