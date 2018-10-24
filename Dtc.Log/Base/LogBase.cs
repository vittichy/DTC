using Dtc.Common.Extensions;
using Dtc.Log.Data;
using Dtc.Log.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Dtc.Log.Base
{
    public abstract class LogBase
    {
        protected static readonly string[] EXE_EXTENSIONS = {
            ".vshost.exe",
            ".exe"
        };
        protected static readonly string DEFAULT_LOG_FILENAME = "DTC_LOG";
        protected static readonly string DEFAULT_EXT = ".log";

        private readonly Type _declaringType;

        /// <summary>
        /// Jmeno souboru s hlavnim logem
        /// </summary>
        protected string _logFileName;

        /// <summary>
        /// Jmena logu pro zapis zprav jen urcite priority
        /// </summary>
        protected readonly List<LogFileWithState> _logFileNames = new List<LogFileWithState>();
        
        public bool ShowDateTime { get; protected set; } = true;


        protected LogBase(Type declaringType, string logFileName, bool writeStartInfo = false, string startInfo = null, bool writeListOfLoadedDll = false)
        {
            _declaringType = declaringType;
            _logFileName = logFileName;

            if (writeStartInfo)
            {
                WriteStartInfo(startInfo);
            }
            if (writeListOfLoadedDll)
            {
                WriteListOfLoadedDll();
            }
        }


        public void WriteStartInfo(string startInfo)
        {
            Write(new string('=', 60));
            Write(string.Format("START - {0}", startInfo));
            Write(string.Format("  Assembly:{0}", _declaringType.Assembly?.FullName));
            Write(string.Format("  BuildDate:{0}", GetLinkerTime(_declaringType.Assembly)));
            Write(string.Format("  Location:{0}", _declaringType.Assembly?.Location));
            Write(string.Format("  UserInteractiveMode:{0} WorkingSet:{1}", Environment.UserInteractive, Environment.WorkingSet));
            Write(string.Format("  OS:{0} CLR:{1}", Environment.OSVersion, Environment.Version));
            Write(string.Format("  MachineName:{0} UserDomainName:{1} UserName:{2}", Environment.MachineName, Environment.UserDomainName, Environment.UserName));
            Write(string.Format("  CD:{0}", Environment.CurrentDirectory));
            Write(new string('=', 60));
        }


        /// <summary>
        /// vypis nahranych net dll - mimo tech z adresare windows
        /// </summary>
        public void WriteListOfLoadedDll()
        {
            Write("Loaded modules:");
            Process.GetCurrentProcess().Modules
                .OfType<ProcessModule>()
                    .Where(p => !(p.FileName.IndexOf(":\\WINDOWS\\", StringComparison.OrdinalIgnoreCase) >= 0))
                        .ToList()
                            .ForEach(p => Write(p.FileName));
            Write(new string('=', 60));
        }


        /// <summary>
        /// ziska build date z assembly
        /// Version z assembly musi mit spravny format (je generovano z VS pri nastaveni AssemblyInfo.cs na [assembly: AssemblyVersion("1.0.*")] 
        /// viz:
        /// http://stackoverflow.com/questions/356543/can-i-automatically-increment-the-file-build-version-when-using-visual-studio
        /// http://stackoverflow.com/questions/324245/asp-net-show-application-build-date-info-at-the-bottom-of-the-screen/324279#324279
        /// ale to je celkem k nicemu, tak pouziju cas ze souboru:
        /// http://stackoverflow.com/questions/1600962/displaying-the-build-date
        /// </summary>
        private DateTime GetLinkerTime(Assembly assembly)
        {
            var filePath = assembly.Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;

            var buffer = new byte[2048];
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                stream.Read(buffer, 0, 2048);
            }

            var offset = BitConverter.ToInt32(buffer, c_PeHeaderOffset);
            var secondsSince1970 = BitConverter.ToInt32(buffer, offset + c_LinkerTimestampOffset);
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var linkTimeUtc = epoch.AddSeconds(secondsSince1970);
            return linkTimeUtc;
        }



        public void Write(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor, LogState state = LogState.None)
        {
            string toFileText;
            var timeStr = toFileText = GetTimeStr();
            Console.Write(timeStr);

            if (state != LogState.None)
            {
                var stateStr = LogStateConverter.ToString(state);
                if (!string.IsNullOrEmpty(stateStr))
                {
                    stateStr = string.Format("[{0}] ", stateStr);
                }
                var prevForeColor = Console.ForegroundColor;
                Console.ForegroundColor = LogStateConverter.ToColor(state);
                Console.Write(stateStr);
                Console.ForegroundColor = prevForeColor;
                toFileText += stateStr;
            }

            var prevForegroundColor = Console.ForegroundColor;
            var prevBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            text = string.Format("{0}", text);
            toFileText += text;
            Console.WriteLine(text);
            Console.ForegroundColor = prevForegroundColor;
            Console.BackgroundColor = prevBackgroundColor;

            LogToFile(toFileText, state);
        }


        public void Write(string text, ConsoleColor foregroundColor, LogState state = LogState.None)
        {
            Write(text, foregroundColor, Console.BackgroundColor, state);
        }


        public void Write(string text, LogState state = LogState.None)
		{
            Write(text, Console.ForegroundColor, Console.BackgroundColor, state);
		}


		public void Write(Exception ex, bool debugInfo = true, LogState state = LogState.Error)
		{
            Write("Exception: ".PadRight(60, '='), state);
            if (ex != null)
            {
                Write(string.Format("Type: {0}", ex.GetType().FullName), state);
                Write(string.Format("Message: {0}", ex.Message), state);
                var webEx = ex as WebException;
                if (webEx != null)
                {
                    Write(string.Format("WebException.Status: {0}", webEx.Status), state);
                    if (webEx.Status == WebExceptionStatus.ProtocolError)
                    {
                        Console.WriteLine("WebException.Response.Status Code: {0}", ((HttpWebResponse)webEx.Response).StatusCode);
                        Console.WriteLine("WebException.Response.Status Description: {0}", ((HttpWebResponse)webEx.Response).StatusDescription);
                    }
                }

                var exceptionsSet = ex.InnerExceptionsSet();
                if (exceptionsSet.Count > 1)
                {
                    Write("Exception list: ", state);
                    exceptionsSet.ForEach(p => Write(p, state));
                }

                if (debugInfo)
                {
                    var newLineSeparator = new string[] { Environment.NewLine };
                    Write("Source: ", state);
                    ex.Source.Split(newLineSeparator, StringSplitOptions.None).ToList().ForEach(p => Write(p, state));

                    Write("StackTrace: ", state);
                    //ar exceptionsSet = ex.InnerExceptionsSet();
                    ex.StackTrace.Split(newLineSeparator, StringSplitOptions.None).ToList().ForEach(p => Write(p, state));
                }
            }
            else
            {
                Write("[exception is null ?]");
            }
            Write(string.Empty.PadRight(60, '='), state);
        }
        

        private void LogToFile(string toFileText, LogState state)
		{
            var text = (toFileText ?? string.Empty) + Environment.NewLine;

            // hlavni log file
            AppendTextToLogFile(_logFileName, text);

            // log file pouze pro urcitou LogState (nemusi byt definovany)
            AppendTextToLogFile(_logFileNames?.FirstOrDefault(p => (p.LogState == state))?.FileName, text);
        }


        private void AppendTextToLogFile(string fileName, string text)
        {
            if(!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(text))
            {
                File.AppendAllText(fileName, text);
            }
        }

		
		private string GetTimeStr()
		{
			if (ShowDateTime)
			{
				return string.Format("{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}.{6:000} ", 
                    DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
			}
			else
			{
				return string.Empty;
			}
		}

        protected static string GetExeLogFileName()
        {
            var appName = AppDomain.CurrentDomain.FriendlyName;
            // exe filename bez koncove casti
            var exeFilename = EXE_EXTENSIONS.Where(p => appName.EndsWith(p, StringComparison.InvariantCultureIgnoreCase))
                                            .Select(p => appName.RemoveEndText(p))
                                            .FirstOrDefault();

            return exeFilename ?? DEFAULT_LOG_FILENAME;
        }

    }
}