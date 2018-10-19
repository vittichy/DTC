using Dtc.ArgsParser.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dtc.ArgsParser
{
    /// <summary>
    /// Trida pro parsovani args[] parametru konzolove aplikace
    /// - rozpasuje args[] do pole Param[]
    /// - priklady args[] retezcu:
    /// prepinac		- PTCommand
    /// -help			- PTSwitch bez Values
    /// /size:300		- PTSwitch, Value a Values[] budou naplneny
    /// -size:300,50    - PTSwitch, Value a Values[] budou naplneny   -> to ted urcite nefunguje ;-)
    /// atd..
    /// 
    /// 2005 kveten
    ///		[+] funkcni
    ///		
    /// </summary>
    public class ArgsParser  // :IEnumerator,IEnumerable
    {
        /// <summary>
        /// retezce, kterymi muze zacinat switch (delsi na zacatek pole, aby fungovala fce  RemoveSwitch() !
        /// </summary>
        public static string[] SwitchStarters = { "--", "-", "/" };

        /// <summary>
        /// znaky, kterymi se deli jednotlive Values[] - napr: /switch:val1,val2,val3
        /// </summary>
        public static char[] ValueSeparators = { ';', ',' };

        /// <summary>
        /// delici znak mezi key a value u switche
        /// </summary>
        public static char SwitchValueSeparator = ':';

        /// <summary>
        /// dekodovane pole parametru podle argv[]
        /// </summary>
        public List<ArgParameter> ArgParameterSet { get; private set; } = new List<ArgParameter>();


        /// <summary>
        /// Vraci seznam commandu
        /// </summary>
        public List<string> CommandNames { get { return ArgParameterSet.Where(p => p.ArgParamType == ArgParamType.Command).Select(p => p.Key).ToList(); } }


        /// <summary>
        /// Vraci prvni command
        /// </summary>
        public string Command { get { return CommandNames.FirstOrDefault(); } }


        /// <summary>
        /// konstruktor 
        /// - na vstup pole argumentu z command line programu
        /// </summary>
        public ArgsParser(string[] args)
        {
            ArgParameterSet = (args != null) ? args.Select(p => new ArgParameter(p)).ToList() : new List<ArgParameter>();
        }

        
        public ArgParameter GetSwitch(params string[] nameSet)
        {
            return FindKeysByName(nameSet).FirstOrDefault(p => p.ArgParamType == ArgParamType.Switch);
        }


        //public ArgParameter GetCommand(params string[] nameSet)
        //{
        //    return FindKeysByName(nameSet).FirstOrDefault(p => p.ArgParamType == ArgParamType.Command);
        //}


    




        //public ArgParameter GetFirstCommand()
        //{
        //    return ArgParameterSet.FirstOrDefault(p => p.ArgParamType == ArgParamType.Command);
        //}


        private List<ArgParameter> FindKeysByName(string name)
        {
            var result = ArgParameterSet.Where(p => string.Compare(name, p.Key, StringComparison.OrdinalIgnoreCase) == 0).ToList();
            return result;
        }


        private List<ArgParameter> FindKeysByName(params string[] nameSet)
        {
            var result = nameSet.ToList().SelectMany(p => FindKeysByName(p)).ToList();
            return result;
        }

    }
}