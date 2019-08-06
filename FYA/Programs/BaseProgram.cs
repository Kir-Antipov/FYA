using System;
using System.Collections.Generic;

namespace FYA.Programs
{
    public abstract class BaseProgram : IProgram
    {
        #region Var
        public string Name { get; }
        public virtual string Usage => string.Empty;
        public virtual string Description => string.Empty;
        #endregion

        #region Init
        public BaseProgram()
        {
            string typeName = GetType().Name;
            int index = typeName.ToLower().IndexOf("program");
            Name = index == -1 ? typeName : index > 0 ? typeName.Substring(0, index) : typeName.Substring(7);
        }
        #endregion

        #region Functions
        public abstract void Run(IEnumerable<string> Arguments);

        public virtual void PrintException(Exception Exception) => PrintException(Exception.Message);
        public virtual void PrintException(string Exception)
        {
            Console.WriteLine("An error occurred while executing the program:");
            Console.WriteLine("\n {0}", Exception);
        }

        public override string ToString() => Name;
        public override int GetHashCode() => Name.GetHashCode();
        public override bool Equals(object obj) => obj is BaseProgram o && o.Name == Name;
        #endregion
    }
}
