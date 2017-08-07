using System;

namespace SampleApp.Common.OutputWriter
{
    public class ConsoleOutputWritter : IOutputWriter
    {
        private readonly ConsoleColor _defaultColor = ConsoleColor.Gray;

        public void WriteLine(string s)
        {
            Console.WriteLine(s);
        }

        public void SetColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        public void ResetDefaultColor()
        {
            Console.ForegroundColor = _defaultColor;
        }
    }
}