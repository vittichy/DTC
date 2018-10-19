namespace Dtc.ArgsParser.Data
{
    /// <summary>
    /// urcuje typ argumentu - prikaz nebo prepinac
    /// </summary>
    public enum ArgParamType 
	{
        /// <summary>
        /// arg command
        /// </summary>
		Command, 

        /// <summary>
        /// arg switch (-help etc)
        /// </summary>
		Switch
	};
}
