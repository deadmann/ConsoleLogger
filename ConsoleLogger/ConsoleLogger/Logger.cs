using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleLogger
{
    [DebuggerNonUserCode]
    public static class Logger
    {
        public static int CurrentLine { get { return Console.CursorTop; } }
        public static int NextLine { get { return Console.CursorTop + 1; } }

        public static int WriteLine(DisplayData data, Align align = Align.LeftFill, SplitMode splitMode = SplitMode.None)
        {
            return WriteData(data, false, NextLine, align, splitMode);
        }

        public static int WriteData(DisplayData data, bool cls, int startLine, Align align, SplitMode splitMode)
        {
            return WriteData(new[] {data}, cls, startLine, align, splitMode);
        }

        public static int WriteData(IEnumerable<DisplayData> dataList, bool cls, int startLine, Align align, SplitMode splitMode)
        {
            Console.ResetColor();
            if (startLine > -1 && startLine <= Console.BufferHeight)
            {
                Console.SetCursorPosition(0, startLine);
            }
            var width = Console.BufferWidth;
            //var height = Console.BufferHeight;

            if (cls)
                Console.Clear();

            var cursorBeginPosition = Console.CursorTop;


            var offset = 0;
            foreach (var data in dataList)
            {
                #region Align Left
                //Align Left
                if (align == Align.Left)
                {
                    //string less or equal than screen size
                    if (data.WritableValue.Length <= width)
                    {
                        //string multi-line
                        if (data.WritableValue.Contains("\n"))
                        {
                            var stringDataList = data.WritableValue.Split(Convert.ToChar("\n"));
                            foreach (var stringData in stringDataList)
                            {
                                offset = width - stringData.Length;
                                //var spaceString = "";
                                //for (var i = 0; i < offset; i++) spaceString += " ";
                                if (Console.ForegroundColor != data.Foreground)
                                    Console.ForegroundColor = data.Foreground;
                                if (Console.BackgroundColor != data.Background)
                                    Console.BackgroundColor = data.Background;
                                if (offset == 0)
                                    Console.Write(stringData);
                                else
                                    Console.WriteLine(stringData);
                            }
                        }
                        //string single-line
                        else
                        {
                            offset = width - data.WritableValue.Length;
                            //var spaceString = "";
                            //for (var i = 0; i < offset; i++) spaceString += " ";
                            if (Console.ForegroundColor != data.Foreground)
                                Console.ForegroundColor = data.Foreground;
                            if (Console.BackgroundColor != data.Background)
                                Console.BackgroundColor = data.Background;
                            if (offset == 0)
                                Console.Write(data.WritableValue);
                            else
                                Console.WriteLine(data.WritableValue);
                        }
                    }
                    //string bigger than screen size
                    else
                    {
                        var stringList = new List<string>();
                        var newString = "";
                        var charList = data.WritableValue.ToCharArray();
                        var buffer = false;
                        foreach (var c in charList)
                        {
                            if (c.ToString() == "\n")
                            {
                                buffer = true;
                                if (newString.Trim() != "")
                                {
                                    stringList.Add(newString);
                                }
                                newString = "";
                            }
                            else if (newString.Length < width - 1)
                            {
                                buffer = true;
                                newString += c.ToString();
                            }
                            else
                            {
                                buffer = false;
                                newString += c.ToString();
                                stringList.Add(newString);
                                newString = "";
                            }
                        }
                        if (buffer)
                        {
                            stringList.Add(newString);
                        }

                        foreach (var line in stringList)
                        {
                            //string multi-line
                            if (line.Contains("\n"))
                            {
                                var stringDataList = line.Split(Convert.ToChar("\n"));
                                foreach (string stringData in stringDataList)
                                {
                                    offset = width - stringData.Length;
                                    //var spaceString = "";
                                    //for (int i = 0; i < offset; i++) spaceString += " ";
                                    if (Console.ForegroundColor != data.Foreground)
                                        Console.ForegroundColor = data.Foreground;
                                    if (Console.BackgroundColor != data.Background)
                                        Console.BackgroundColor = data.Background;
                                    if (offset == 0)
                                        Console.Write(stringData);
                                    else
                                        Console.WriteLine(stringData);
                                }
                            }
                            //string single-line
                            else
                            {
                                offset = width - line.Length;
                                //var spaceString = "";
                                //for (int i = 0; i < offset; i++) spaceString += " ";
                                if (Console.ForegroundColor != data.Foreground)
                                    Console.ForegroundColor = data.Foreground;
                                if (Console.BackgroundColor != data.Background)
                                    Console.BackgroundColor = data.Background;
                                if (offset == 0)
                                    Console.Write(line);
                                else
                                    Console.WriteLine(line);
                            }
                        }
                    }
                }
                #endregion

                #region Align Left Fill
                //Align Left Fill
                else if (align == Align.LeftFill)
                {
                    //string less or equal than screen size
                    if (data.WritableValue.Length <= width)
                    {
                        //string multi-line
                        if (data.WritableValue.Contains("\n"))
                        {
                            var stringDataList = data.WritableValue.Split(Convert.ToChar("\n"));
                            foreach (var stringData in stringDataList)
                            {
                                offset = width - stringData.Length;
                                var spaceString = "";
                                for (var i = 0; i < offset; i++) spaceString += " ";
                                if (Console.ForegroundColor != data.Foreground)
                                    Console.ForegroundColor = data.Foreground;
                                if (Console.BackgroundColor != data.Background)
                                    Console.BackgroundColor = data.Background;
                                Console.Write(stringData + spaceString);
                            }
                        }
                        //string single-line
                        else
                        {
                            offset = width - data.WritableValue.Length;
                            var spaceString = "";
                            for (var i = 0; i < offset; i++) spaceString += " ";
                            if (Console.ForegroundColor != data.Foreground)
                                Console.ForegroundColor = data.Foreground;
                            if (Console.BackgroundColor != data.Background)
                                Console.BackgroundColor = data.Background;
                            Console.Write(data.WritableValue + spaceString);
                        }
                    }
                    //string bigger than screen size
                    else
                    {
                        var stringList = new List<string>();
                        var newString = "";
                        var charList = data.WritableValue.ToCharArray();
                        var buffer = false;
                        foreach (var c in charList)
                        {
                            if (c.ToString() == "\n")
                            {
                                buffer = true;
                                if (newString.Trim() != "")
                                {
                                    stringList.Add(newString);
                                }
                                newString = "";
                            }
                            else if (newString.Length < width - 1)
                            {
                                buffer = true;
                                newString += c.ToString();
                            }
                            else
                            {
                                buffer = false;
                                newString += c.ToString();
                                stringList.Add(newString);
                                newString = "";
                            }
                        }
                        if (buffer)
                        {
                            stringList.Add(newString);
                        }

                        foreach (var line in stringList)
                        {
                            //string multi-line
                            if (line.Contains("\n"))
                            {
                                var stringDataList = line.Split(Convert.ToChar("\n"));
                                foreach (var stringData in stringDataList)
                                { 
                                    offset = width - stringData.Length;
                                    var spaceString = "";
                                    for (var i = 0; i < offset; i++) spaceString += " ";
                                    if (Console.ForegroundColor != data.Foreground)
                                        Console.ForegroundColor = data.Foreground;
                                    if (Console.BackgroundColor != data.Background)
                                        Console.BackgroundColor = data.Background;
                                    Console.Write(stringData + spaceString);
                                }
                            }
                            //string single-line
                            else
                            {
                                offset = width - line.Length;
                                var spaceString = "";
                                for (var i = 0; i < offset; i++) spaceString += " ";
                                if (Console.ForegroundColor != data.Foreground)
                                    Console.ForegroundColor = data.Foreground;
                                if (Console.BackgroundColor != data.Background)
                                    Console.BackgroundColor = data.Background;
                                Console.Write(line + spaceString);
                            }
                        }
                    }
                }
                #endregion

                #region Align Center
                //Align Center 
                else if (align == Align.Center)
                {
                    //string less or equal than screen size
                    if (data.WritableValue.Length <= width)
                    {
                        //string multi-line
                        if (data.WritableValue.Contains("\n"))
                        {
                            var stringDataList = data.WritableValue.Split(Convert.ToChar("\n"));
                            foreach (var stringData in stringDataList)
                            {
                                offset = width / 2 - (stringData.Length / 2);
                                var spaceString = "";
                                for (var i = 0; i < offset; i++) spaceString += " ";
                                Console.ResetColor();
                                Console.Write(spaceString);
                                if (Console.ForegroundColor != data.Foreground)
                                    Console.ForegroundColor = data.Foreground;
                                if (Console.BackgroundColor != data.Background)
                                    Console.BackgroundColor = data.Background;
                                if (offset == 0)
                                    Console.Write(stringData);
                                else
                                    Console.WriteLine(stringData);
                            }
                        }
                        //string single-line
                        else
                        {
                            offset = width / 2 - (data.WritableValue.Length / 2);
                            var spaceString = "";
                            for (var i = 0; i < offset; i++) spaceString += " ";
                            Console.ResetColor();
                            Console.Write(spaceString);
                            if (Console.ForegroundColor != data.Foreground)
                                Console.ForegroundColor = data.Foreground;
                            if (Console.BackgroundColor != data.Background)
                                Console.BackgroundColor = data.Background;
                            if (offset == 0)
                                Console.Write(data.WritableValue);
                            else
                                Console.WriteLine(data.WritableValue);
                        }
                    }
                    //string bigger than screen size
                    else
                    {
                        var stringList = new List<string>();
                        var newString = "";
                        var charList = data.WritableValue.ToCharArray();
                        var buffer = false;
                        foreach (var c in charList)
                        {
                            if (c.ToString() == "\n")
                            {
                                buffer = true;
                                if (newString.Trim() != "")
                                {
                                    stringList.Add(newString);
                                }
                                newString = "";
                            }
                            else if (newString.Length < width - 1)
                            {
                                buffer = true;
                                newString += c.ToString();
                            }
                            else
                            {
                                buffer = false;
                                newString += c.ToString();
                                stringList.Add(newString);
                                newString = "";
                            }
                        }
                        if (buffer)
                        {
                            stringList.Add(newString);
                        }

                        foreach (var line in stringList)
                        {
                            //string multi-line
                            if (line.Contains("\n"))
                            {
                                var stringDataList = line.Split(Convert.ToChar("\n"));
                                foreach (var stringData in stringDataList)
                                {
                                    offset = width / 2 - (stringData.Length / 2);
                                    var spaceString = "";
                                    for (int i = 0; i < offset; i++) spaceString += " ";
                                    Console.ResetColor();
                                    Console.Write(spaceString);
                                    if (Console.ForegroundColor != data.Foreground)
                                        Console.ForegroundColor = data.Foreground;
                                    if (Console.BackgroundColor != data.Background)
                                        Console.BackgroundColor = data.Background;
                                    if (offset == 0)
                                        Console.Write(stringData);
                                    else
                                        Console.WriteLine(stringData);
                                }
                            }
                            //string single-line
                            else
                            {
                                offset = width / 2 - (line.Length / 2);
                                var spaceString = "";
                                for (int i = 0; i < offset; i++) spaceString += " ";
                                Console.ResetColor();
                                Console.Write(spaceString);
                                if (Console.ForegroundColor != data.Foreground)
                                    Console.ForegroundColor = data.Foreground;
                                if (Console.BackgroundColor != data.Background)
                                    Console.BackgroundColor = data.Background;
                                if (offset == 0)
                                    Console.Write(line);
                                else
                                    Console.WriteLine(line);
                            }
                        }
                    }
                }
                #endregion

                #region Align Center Fill
                //Align Center Fill
                else if (align == Align.CenterFill)
                {
                    //string less or equal than screen size
                    if (data.WritableValue.Length <= width)
                    {
                        //string multi-line
                        if (data.WritableValue.Contains("\n"))
                        {
                            var stringDataList = data.WritableValue.Split(Convert.ToChar("\n"));
                            foreach (var stringData in stringDataList)
                            {
                                offset = width / 2 - (stringData.Length / 2);
                                var spaceString = "";
                                for (var i = 0; i < offset; i++) spaceString += " ";
                                if (Console.ForegroundColor != data.Foreground)
                                    Console.ForegroundColor = data.Foreground;
                                if (Console.BackgroundColor != data.Background)
                                    Console.BackgroundColor = data.Background;
                                var sLeft = "";
                                for (var i = spaceString.Length + stringData.Length; i < width; i++) sLeft += " ";
                                Console.Write(spaceString + stringData + sLeft);
                            }
                        }
                        //string single-line
                        else
                        {
                            offset = width / 2 - (data.WritableValue.Length / 2);
                            var spaceString = "";
                            for (var i = 0; i < offset; i++) spaceString += " ";
                            if (Console.ForegroundColor != data.Foreground)
                                Console.ForegroundColor = data.Foreground;
                            if (Console.BackgroundColor != data.Background)
                                Console.BackgroundColor = data.Background;
                            var sLeft = "";
                            for (var i = spaceString.Length + data.WritableValue.Length; i < width; i++) sLeft += " ";
                            Console.Write(spaceString + data.WritableValue + sLeft);
                        }
                    }
                    //string bigger than screen size
                    else
                    {
                        var stringList = new List<string>();
                        var newString = "";
                        var charList = data.WritableValue.ToCharArray();
                        var buffer = false;
                        foreach (var c in charList)
                        {
                            if (c.ToString() == "\n")
                            {
                                buffer = true;
                                if (newString.Trim() != "")
                                {
                                    stringList.Add(newString);
                                }
                                newString = "";
                            }
                            else if (newString.Length < width - 1)
                            {
                                buffer = true;
                                newString += c.ToString();
                            }
                            else
                            {
                                buffer = false;
                                newString += c.ToString();
                                stringList.Add(newString);
                                newString = "";
                            }
                        }
                        if (buffer)
                        {
                            stringList.Add(newString);
                        }

                        foreach (var line in stringList)
                        {
                            //string multi-line
                            if (line.Contains("\n"))
                            {
                                var stringDataList = line.Split(Convert.ToChar("\n"));
                                foreach (var stringData in stringDataList)
                                {
                                    offset = width / 2 - (stringData.Length / 2);
                                    var spaceString = "";
                                    for (int i = 0; i < offset; i++) spaceString += " ";
                                    if (Console.ForegroundColor != data.Foreground)
                                        Console.ForegroundColor = data.Foreground;
                                    if (Console.BackgroundColor != data.Background)
                                        Console.BackgroundColor = data.Background;
                                    var sLeft = "";
                                    for (var i = spaceString.Length + stringData.Length; i < width; i++) sLeft += " ";
                                    Console.Write(spaceString + stringData + sLeft);
                                }
                            }
                            //string single-line
                            else
                            {
                                offset = width / 2 - (line.Length / 2);
                                var spaceString = "";
                                for (int i = 0; i < offset; i++) spaceString += " ";
                                if (Console.ForegroundColor != data.Foreground)
                                    Console.ForegroundColor = data.Foreground;
                                if (Console.BackgroundColor != data.Background)
                                    Console.BackgroundColor = data.Background;
                                var sLeft = "";
                                for (int i = spaceString.Length + line.Length; i < width; i++) sLeft += " ";
                                Console.Write(spaceString + line + sLeft);
                            }
                        }
                    }
                }
                #endregion
            }
            //Reset Color
            if ((splitMode == SplitMode.FilledLine && offset == 0) || splitMode == SplitMode.All)
                Console.WriteLine();
            Console.ResetColor();
            return Console.CursorTop - cursorBeginPosition;
        }
    }
}
