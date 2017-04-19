using System.Diagnostics;

namespace GabrielWinFormTest.Helpers.Logger
{
    /// <summary>
    /// Create Empty Spliter Line, If The Condition Meet
    /// </summary>
    public enum SplitMode
    {
        /// <summary>
        /// Don't Put Spliter Line, Even if Line Is Filled
        /// </summary>
        None,
        /// <summary>
        /// Put Spliter Line if Last Line is Filled
        /// </summary>
        FilledLine,
        /// <summary>
        /// Put Spliter Line All The Time
        /// </summary>
        All 
        
    }
}