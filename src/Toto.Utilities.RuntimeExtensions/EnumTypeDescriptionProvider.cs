using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Toto.Utilities.RuntimeExtensions
{
    // <summary>
    /// Type descriptor provider for <see cref="T:Thinktecture.Enum`2" />.
    /// </summary>
    [NullableContext(1)]
    [Nullable(0)]
    public class EnumTypeDescriptionProvider : TypeDescriptionProvider
    {
        /// <summary>
        /// Initializes a new instance of <see cref="T:Thinktecture.EnumTypeDescriptionProvider" />.
        /// </summary>
        public EnumTypeDescriptionProvider()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="T:Thinktecture.EnumTypeDescriptionProvider" />.
        /// </summary>
        /// <param name="parent">Parent provider.</param>
        public EnumTypeDescriptionProvider(TypeDescriptionProvider parent)
            : base(parent)
        {
        }

        /// <inheritdoc />
        public override ICustomTypeDescriptor GetTypeDescriptor(
            Type objectType,
            object instance)
        {
            return (ICustomTypeDescriptor) new EnumTypeDescriptor(base.GetTypeDescriptor(objectType, instance), objectType);
        }
    }
}