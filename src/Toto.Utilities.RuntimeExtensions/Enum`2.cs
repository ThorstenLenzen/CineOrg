using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Toto.Utilities.RuntimeExtensions
{
    /// <summary>Base class for enum-like classes.</summary>
  /// <remarks>
  /// Derived classes must have a default constructor for creation of "invalid" enumeration items.
  /// The default constructor should not be public.
  /// </remarks>
  /// <typeparam name="TEnum">Concrete type of the enumeration.</typeparam>
  /// <typeparam name="TKey">Type of the key.</typeparam>
  [NullableContext(1)]
  [Nullable(0)]
  [TypeDescriptionProvider(typeof (EnumTypeDescriptionProvider))]
  public abstract class Enum<[Nullable(0)] TEnum, [Nullable(2)] TKey> : IEquatable<Enum<TEnum, TKey>>, IEnum
    where TEnum : Enum<TEnum, TKey>
  {
    private static readonly EqualityComparer<TKey> _defaultKeyEqualityComparer = EqualityComparer<TKey>.Default;
    private static readonly Func<TKey, TEnum> _invalidEnumFactory = Enum<TEnum, TKey>.GetInvalidEnumerationFactory();
    private static readonly int _typeHashCode = typeof (TEnum).GetHashCode() * 397;
    [Nullable(new byte[] {2, 1})]
    private static IEqualityComparer<TKey> _keyEqualityComparer;
    [Nullable(new byte[] {2, 1, 1})]
    private static Dictionary<TKey, TEnum> _itemsLookup;
    [Nullable(new byte[] {2, 1})]
    private static IReadOnlyList<TEnum> _items;
    private readonly int _hashCode;
    private readonly string _toString;

    /// <summary>
    /// Equality comparer for keys. Default is <see cref="P:System.Collections.Generic.EqualityComparer`1.Default" />.
    /// Important: This property may be changed once and in static constructor only!
    /// </summary>
    protected static IEqualityComparer<TKey> KeyEqualityComparer
    {
      get
      {
        return Enum<TEnum, TKey>._keyEqualityComparer ?? (IEqualityComparer<TKey>) Enum<TEnum, TKey>._defaultKeyEqualityComparer;
      }
      set
      {
        IReadOnlyList<TEnum> items = Enum<TEnum, TKey>._items;
        if ((items != null ? (items.Count > 0 ? 1 : 0) : 0) != 0)
          throw new InvalidOperationException(string.Format("Setting the {0} must be done in static constructor of {1}.", (object) Enum<TEnum, TKey>.KeyEqualityComparer, (object) typeof (TEnum).FullName));
        Enum<TEnum, TKey>._keyEqualityComparer = value;
      }
    }

    private static Dictionary<TKey, TEnum> ItemsLookup
    {
      get
      {
        return Enum<TEnum, TKey>._itemsLookup ?? (Enum<TEnum, TKey>._itemsLookup = Enum<TEnum, TKey>.GetItems());
      }
    }

    private static IReadOnlyList<TEnum> Items
    {
      get
      {
        return Enum<TEnum, TKey>._items ?? (Enum<TEnum, TKey>._items = (IReadOnlyList<TEnum>) Enum<TEnum, TKey>.ItemsLookup.Values.ToList<TEnum>().AsReadOnly());
      }
    }

    private static Func<TKey, TEnum> GetInvalidEnumerationFactory()
    {
      Type type = typeof (TEnum);
      MethodInfo method = type.GetTypeInfo().GetMethod("CreateInvalid", BindingFlags.Instance | BindingFlags.NonPublic);
      if (method == (MethodInfo) null)
        throw new Exception("The method CreateInvalid hasn't been found in enumeration of type " + type.FullName + ".");
      return (Func<TKey, TEnum>) method.CreateDelegate(typeof (Func<TKey, TEnum>), (object) null);
    }

    private static Dictionary<TKey, TEnum> GetItems()
    {
      Type type = typeof (TEnum);
      FieldInfo[] fields = type.GetTypeInfo().GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
      if (((IEnumerable<FieldInfo>) fields).Any<FieldInfo>())
        ((IEnumerable<FieldInfo>) fields).First<FieldInfo>().GetValue((object) null);
      IEnumerable<TEnum> enums = ((IEnumerable<FieldInfo>) fields).Where<FieldInfo>((Func<FieldInfo, bool>) (f => f.FieldType == type)).Select<FieldInfo, TEnum>((Func<FieldInfo, TEnum>) (f =>
      {
        if (!f.IsInitOnly)
          throw new Exception("The field \"" + f.Name + "\" of enumeration type \"" + type.FullName + "\" must be read-only.");
        TEnum @enum = (TEnum) f.GetValue((object) null);
        if ((object) @enum == null)
          throw new Exception("The field \"" + f.Name + "\" of enumeration type \"" + type.FullName + "\" is not initialized.");
        if (!@enum.IsValid)
          throw new Exception("The field \"" + f.Name + "\" of enumeration type \"" + type.FullName + "\" is not valid.");
        return @enum;
      }));
      Dictionary<TKey, TEnum> dictionary = new Dictionary<TKey, TEnum>(Enum<TEnum, TKey>.KeyEqualityComparer);
      foreach (TEnum @enum in enums)
      {
        if (dictionary.ContainsKey(@enum.Key))
          throw new ArgumentException(string.Format("The enumeration of type \"{0}\" has multiple items with the key \"{1}\".", (object) type.FullName, (object) @enum.Key));
        dictionary.Add(@enum.Key, @enum);
      }
      return dictionary;
    }

    object IEnum.Key
    {
      get
      {
        return (object) this.Key;
      }
    }

    /// <summary>The key of the enumeration item.</summary>
    public TKey Key { [return: NotNull] get; }

    /// <inheritdoc />
    public bool IsValid { get; private set; }

    /// <summary>
    /// Initializes new valid instance of <see cref="T:Thinktecture.Enum`2" />.
    /// </summary>
    /// <param name="key">The key of the enumeration item.</param>
    protected Enum([NotNull] TKey key)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      this.Key = key;
      this.IsValid = true;
      this._hashCode = Enum<TEnum, TKey>._typeHashCode ^ Enum<TEnum, TKey>.KeyEqualityComparer.GetHashCode(this.Key);
      this._toString = this.Key.ToString();
    }

    /// <summary>
    /// Creates an invalid instance of type <typeparamref name="TEnum" />.
    /// </summary>
    /// <remarks>
    /// The code must be deterministic and must not access the qualifier <c>this</c> or any members of it.
    /// The returned instance must not be <c>null</c>.
    /// </remarks>
    /// <param name="key">Key of invalid item.</param>
    /// <returns>An invalid enumeration item.</returns>
    protected abstract TEnum CreateInvalid(TKey key);

    /// <summary>Gets all valid items.</summary>
    /// <returns>A collection with all valid items.</returns>
    public static IReadOnlyList<TEnum> GetAll()
    {
      return Enum<TEnum, TKey>.Items;
    }

    /// <summary>
    /// Gets an enumeration item for provided <paramref name="key" />.
    /// </summary>
    /// <param name="key">The key to return an enumeration item for.</param>
    /// <returns>An instance of <typeparamref name="TEnum" /> if <paramref name="key" /> is not <c>null</c>; otherwise <c>null</c>.</returns>
    [return: Nullable(2)]
    [return: NotNullIfNotNull("key")]
    public static TEnum Get([AllowNull] TKey key)
    {
      if ((object) key == null)
        return default (TEnum);
      TEnum @enum;
      if (!Enum<TEnum, TKey>.ItemsLookup.TryGetValue(key, out @enum))
      {
        @enum = Enum<TEnum, TKey>._invalidEnumFactory(key);
        if ((object) @enum == null)
          throw new Exception("The method CreateInvalid of enumeration type " + typeof (TEnum).FullName + " returned null.");
        @enum.IsValid = false;
      }
      return @enum;
    }

    /// <summary>
    /// Gets a valid enumeration item for provided <paramref name="key" /> if a valid item exists.
    /// </summary>
    /// <param name="key">The key to return an enumeration item for.</param>
    /// <param name="item">A valid instance of <typeparamref name="TEnum" />; otherwise <c>null</c>.</param>
    /// <returns><c>true</c> if a valid item with provided <paramref name="key" /> exists; <c>false</c> otherwise.</returns>
    public static bool TryGet([AllowNull] TKey key, [Nullable(2), NotNullWhen(true)] out TEnum item)
    {
      if ((object) key != null)
        return Enum<TEnum, TKey>.ItemsLookup.TryGetValue(key, out item);
      item = default (TEnum);
      return false;
    }

    /// <inheritdoc />
    public void EnsureValid()
    {
      if (!this.IsValid)
        throw new InvalidOperationException(string.Format("The current enumeration item of type {0} with key {1} is not valid.", (object) typeof (TEnum).FullName, (object) this.Key));
    }

    /// <inheritdoc />
    public bool Equals([Nullable(new byte[] {2, 1, 1})] Enum<TEnum, TKey> other)
    {
      if ((object) other == null || (object) this.GetType() != (object) other.GetType())
        return false;
      if ((object) this == (object) other)
        return true;
      return !this.IsValid && !other.IsValid && Enum<TEnum, TKey>.KeyEqualityComparer.Equals(this.Key, other.Key);
    }

    /// <inheritdoc />
    [NullableContext(2)]
    public override bool Equals(object obj)
    {
      return this.Equals(obj as Enum<TEnum, TKey>);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
      return this._hashCode;
    }

    /// <inheritdoc />
    public override string ToString()
    {
      return this._toString;
    }

    /// <summary>
    /// Implicit conversion to the type of <typeparamref name="TKey" />.
    /// </summary>
    /// <param name="item">Item to covert.</param>
    /// <returns>The <see cref="P:Thinktecture.Enum`2.Key" /> of provided <paramref name="item" /> or <c>null</c> if a<paramref name="item" /> is <c>null</c>.</returns>
    public static implicit operator TKey([Nullable(new byte[] {2, 1, 1})] Enum<TEnum, TKey> item)
    {
      return !(item == (Enum<TEnum, TKey>) null) ? item.Key : default (TKey);
    }

    /// <summary>
    /// Compares to instances of <see cref="T:Thinktecture.Enum`2" />.
    /// </summary>
    /// <param name="item1">Instance to compare.</param>
    /// <param name="item2">Another instance to compare.</param>
    /// <returns><c>true</c> if items are equal; otherwise <c>false</c>.</returns>
    public static bool operator ==([Nullable(new byte[] {2, 1, 1})] Enum<TEnum, TKey> item1, [Nullable(new byte[] {2, 1, 1})] Enum<TEnum, TKey> item2)
    {
      return (object) item1 == null ? (object) item2 == null : item1.Equals(item2);
    }

    /// <summary>
    /// Compares to instances of <see cref="T:Thinktecture.Enum`2" />.
    /// </summary>
    /// <param name="item1">Instance to compare.</param>
    /// <param name="item2">Another instance to compare.</param>
    /// <returns><c>false</c> if items are equal; otherwise <c>true</c>.</returns>
    public static bool operator !=([Nullable(new byte[] {2, 1, 1})] Enum<TEnum, TKey> item1, [Nullable(new byte[] {2, 1, 1})] Enum<TEnum, TKey> item2)
    {
      return !(item1 == item2);
    }
  }
}