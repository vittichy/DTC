namespace Dtc.ArgsParser.Data
{
    /// <summary>
    /// arg type
    /// </summary>
    public enum ArgParamType 
	{
        /// <summary>
        /// arg command (help etc)
        /// </summary>
		Command, 

        /// <summary>
        /// arg switch (-help, -file:1.txt etc)
        /// </summary>
		Switch
	};
}
