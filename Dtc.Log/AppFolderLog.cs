using System;
using System.IO;
using Dtc.Log.Data;
using Dtc.Log.Base;

namespace Dtc.Log
{
    /// <summary>
    /// VtLog zapisujici do APP folderu
    /// </summary>
    public class AppFolderLog : LogBase
    {
        public AppFolderLog(Type declaringType, string appDataSubFolder, string iniFileName, bool writeStartInfo = true)
            : base(declaringType, GetFullLogFileName(appDataSubFolder, iniFileName), writeStartInfo)
        {
        }

        public AppFolderLog(Type declaringType, string iniFileName, bool writeStartInfo = true)
            : base(declaringType, GetFullLogFileName(GetExeLogFileName(), iniFileName), writeStartInfo)
        {
        }
        public AppFolderLog(Type declaringType, bool writeStartInfo = true)
            : base(declaringType, GetFullLogFileName(GetExeLogFileName(), GetExeLogFileName() + DEFAULT_EXT), writeStartInfo)
        {
        }


        /// <summary>
        /// zkonstruje log filename v APP foldru usera
        /// </summary>
        private static string GetFullLogFileName(string appDataSubFolder, string iniFileName)
        {
            var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var folder = Path.Combine(appDataFolder, appDataSubFolder);
            Directory.CreateDirectory(folder);

            var fileName = Path.Combine(folder, iniFileName);
            return fileName;
        }
    }
}
