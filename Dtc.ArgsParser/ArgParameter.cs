using Dtc.ArgsParser.Data;
using System;
using System.Diagnostics;

namespace Dtc.ArgsParser
{
    /// <summary>
    /// struktura jednoho argumentu
    /// - typ, puvodni retezec, klic a hodnota
    /// </summary>
    [DebuggerDisplay("{ArgParamType}, Key:{Key}, Value:{Value}, Values.Count:{Values.Length}")]
    public class ArgParameter
	{
        private const string _quotationMark = "\"";

        public readonly string ArgSource;

        public readonly ArgParamType ArgParamType;

        public readonly string Key;
        public readonly string Value;
		public readonly string[] Values;


        public ArgParameter(string arg)
        {
            DecodeArg(ArgSource = arg, out ArgParamType, out Key, out Value, out Values);
        }


        private static void GetArgParamType(string arg, out ArgParamType argParamType, out string argWithoutSwitchStarter)
        {

            for (int i = 0; i < ArgsParser.SwitchStarters.Length; i++)
            {
                if ((arg != null) && arg.StartsWith(ArgsParser.SwitchStarters[i]))
                {
                    argWithoutSwitchStarter = arg.Substring(ArgsParser.SwitchStarters[i].Length);
                    argParamType = ArgParamType.Switch;
                    return;
                }
            }

            argWithoutSwitchStarter = arg;
            argParamType = ArgParamType.Command;
        }



        private static void DecodeArg(string arg, out ArgParamType paramType, out string key, out string value, out string[] values)
        {
            GetArgParamType(arg, out paramType, out string argWithoutSwitchStarter);

            // rozparsuju argument na switch:value - pokud nejake value existuje 
            int i = argWithoutSwitchStarter.IndexOf(ArgsParser.SwitchValueSeparator);
            if (i < 0)
            {
                key = argWithoutSwitchStarter;
                value = string.Empty;
                values = new string[0];
            }
            else
            {
                key = argWithoutSwitchStarter.Substring(0, i);
                value = argWithoutSwitchStarter.Substring(i + 1, argWithoutSwitchStarter.Length - i - 1);
                RemoveQuotationsMark(ref value);
                // rozparsovani Values[] pokud je jich vic
                values = value.Split(ArgsParser.ValueSeparators);
            }
        }

        /// <summary>
        /// replace _quotationMark from start and ending of parameter value
        /// </summary>
        private static void RemoveQuotationsMark(ref string value)
        {
            if (!string.IsNullOrEmpty(value) && value.StartsWith(_quotationMark) && value.EndsWith(_quotationMark))
            {
                value = value.Substring(1, value.Length - 2);
            }
        }
    }
}
