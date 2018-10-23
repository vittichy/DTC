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
        /// switch start signs - for example --help, -help, /help
        /// </summary>
        public static string[] SwitchStarters = { "--", "-", "/" };

        /// <summary>
        /// signs for separate values - for example: /switch:val1,val2,val3
        /// </summary>
        public static char[] ValueSeparators = { ';', ',' };

        /// <summary>
        /// command and value separator - for example: -file:'c:\file.txt'
        /// </summary>
        public static char SwitchValueSeparator = ':';

        /// <summary>
        /// parsed list from source args[] array
        /// </summary>
        public List<ArgParameter> ArgParameterSet { get; private set; } = new List<ArgParameter>();


        public List<ArgParameter> Commands { get { return ArgParameterSet.Where(p => p.ArgParamType == ArgParamType.Command).ToList(); } }

        public List<ArgParameter> Switches { get { return ArgParameterSet.Where(p => p.ArgParamType == ArgParamType.Switch).ToList(); } }


        /// <summary>
        /// constructor
        /// </summary>
        public ArgsParser(string[] args)
        {
            ArgParameterSet = (args != null) ? args.Select(p => new ArgParameter(p)).ToList() : new List<ArgParameter>();
        }


        public ArgParameter GetSwitch(params string[] nameSet)
        {
            return FindArgByName(Switches, nameSet).FirstOrDefault();
        }

        public string GetSwitchValue(params string[] nameSet)
        {
            return FindArgByName(Switches, nameSet).FirstOrDefault()?.Value;
        }
        
        public ArgParameter GetCommand(params string[] nameSet)
        {
            return FindArgByName(Commands, nameSet).FirstOrDefault();
        }

        public bool CommandExist(params string[] nameSet)
        {
            return (GetCommand(nameSet) != null);
        }
        

        private List<ArgParameter> FindArgByName(List<ArgParameter> argParamSet, params string[] nameSet)
        {
            // case-sensititve compare!
            List<ArgParameter> FindArgByName(string name)
            {
                return ArgParameterSet.Where(p => string.Compare(name, p.Key, StringComparison.InvariantCulture) == 0).ToList();
            }

            var result = nameSet.ToList().SelectMany(p => FindArgByName(p)).ToList();
            return result;
        }

    }
}