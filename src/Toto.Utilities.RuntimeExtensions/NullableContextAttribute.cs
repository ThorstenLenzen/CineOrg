using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Toto.Utilities.RuntimeExtensions
{
    [CompilerGenerated]
    [Embedded]
    [AttributeUsage(AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
    internal sealed class NullableContextAttribute : Attribute
    {
        public readonly byte Flag;

        public NullableContextAttribute([In] byte obj0)
        {
            // ISSUE: reference to a compiler-generated field
            this.Flag = obj0;
        }
    }
}