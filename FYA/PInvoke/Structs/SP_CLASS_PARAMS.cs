using System.Runtime.InteropServices;

namespace FYA.PInvoke.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct SP_CLASS_PARAMS
    {
        #region Var
        public readonly int Size;
        public readonly DiFunction Function;
        public readonly StateChangeAction State;
        public readonly Scopes Scope;
        public readonly int Profile;
        #endregion

        #region Init
        public SP_CLASS_PARAMS(Scopes Scope, DiFunction Function, StateChangeAction State) : this(8, Scope, Function, State) { }

        public SP_CLASS_PARAMS(int Size, Scopes Scope, DiFunction Function, StateChangeAction State)
        {
            Profile = 0;
            this.Size = Size;
            this.Scope = Scope;
            this.Function = Function;
            this.State = State;
        }
        #endregion
    }
}
