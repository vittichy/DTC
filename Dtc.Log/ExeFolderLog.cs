using Dtc.Log.Base;
using System;
using System.IO;

namespace Dtc.Log
{
    public class ExeFolderLog : FileLog
    {
 
        public ExeFolderLog(Type declaringType, bool writeStartInfo = true, bool writeListOfLoadedDll = true) 
            : base(declaringType, GetFullLogFileName(), writeStartInfo, writeListOfLoadedDll)
        {
        }
        

        /// <summary>
        /// zkonstruje vlastni filename logu dle aktualniho exe
        /// </summary>
        private static string GetFullLogFileName()
        {
            string appName = GetExeLogFileName();
            var result = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, appName + DEFAULT_EXT);
            return result;
        }

    }
}
