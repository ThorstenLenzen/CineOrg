using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Toto.Utilities.RuntimeExtensions
{
    [CompilerGenerated]
    [Embedded]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
    internal sealed class NullableAttribute : Attribute
    {
        public readonly byte[] NullableFlags;

        public NullableAttribute([In] byte obj0)
        {
            // ISSUE: reference to a compiler-generated field
            this.NullableFlags = new byte[1]{ obj0 };
        }

        public NullableAttribute([In] byte[] obj0)
        {
            // ISSUE: reference to a compiler-generated field
            this.NullableFlags = obj0;
        }
    }
}