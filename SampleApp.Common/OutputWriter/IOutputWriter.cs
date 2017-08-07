using System;

namespace SampleApp.Common.OutputWriter
{
    public interface IOutputWriter
    {
        void WriteLine(string s);

        void SetColor(ConsoleColor color);

        void ResetDefaultColor();
    }
}
