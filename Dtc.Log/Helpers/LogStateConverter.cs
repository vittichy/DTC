using Dtc.Log.Data;
using System;

namespace Dtc.Log.Helpers
{
    static class LogStateConverter
	{
		public static ConsoleColor ToColor(LogState state)
		{
			switch (state)
			{
				case LogState.Info:
					return ConsoleColor.Yellow;
				case LogState.Debug:
					return ConsoleColor.Magenta;
				case LogState.Error:
					return ConsoleColor.Red;
				case LogState.Ok:
					return ConsoleColor.Green;
			}
			return Console.ForegroundColor; // jinak vracim vychozi barvu 
		}


		public static string ToString(LogState state)
		{
			switch (state)
			{
				case LogState.None:
					return null;
				case LogState.Info:
					return "Info";
				case LogState.Debug:
					return "Ddb";
				case LogState.Error:
					return "Err";
				case LogState.Ok:
					return "Ok";
				default:
					return "?";
			}
		}
	}
}
