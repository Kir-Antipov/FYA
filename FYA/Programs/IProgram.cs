using System;
using System.Collections.Generic;

namespace FYA.Programs
{
    public interface IProgram
    {
        string Name { get; }
        string Description { get; }

        void Run(IEnumerable<string> Arguments);

        void PrintException(string Error);
        void PrintException(Exception Error);
    }
}
