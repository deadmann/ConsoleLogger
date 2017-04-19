using System;
using System.Diagnostics;

namespace ConsoleLogger
{
    [DebuggerNonUserCode]
    public class DisplayData
    {
        public static implicit operator DisplayData(string s)
        {
            return new DisplayData(s);
        }

        public static implicit operator string(DisplayData data)
        {
            return data.WritableValue;
        }

        public DisplayData(string strData)
        {
            WritableValue = strData;
            Foreground = ConsoleColor.White;
            Background = ConsoleColor.Black;
        }

        public DisplayData(string strData, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            WritableValue = strData;
            Foreground = foregroundColor;
            Background = backgroundColor;
        }

        public string WritableValue { get; set; }

        public ConsoleColor Foreground { get; set; }

        public ConsoleColor Background { get; set; }
    }
}