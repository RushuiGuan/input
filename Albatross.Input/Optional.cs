using System;

namespace Albatross.Input {
	/// <summary>
	/// Represents an optionally-present reference-type value, distinguishing between a field that was
	/// omitted from a request and one that was explicitly set to <c>null</c>.
	/// </summary>
	/// <remarks>
	/// A plain nullable <typeparamref name="T"/>? cannot tell the difference between two semantically
	/// different cases in a partial-update (HTTP PATCH) scenario:
	/// <list type="table">
	///   <listheader><term>HasValue</term><term>Value</term><term>Meaning</term></listheader>
	///   <item><term>false</term><term>null</term><term>Field was absent — leave it unchanged.</term></item>
	///   <item><term>true</term><term>null</term><term>Field was explicitly nullified — clear it.</term></item>
	///   <item><term>true</term><term>non-null</term><term>Field was supplied — update it.</term></item>
	/// </list>
	/// <example>
	/// <code>
	/// public record PatchUserRequest {
	///     public Optional&lt;string&gt; Nickname { get; init; }
	/// }
	///
	/// void ApplyPatch(User user, PatchUserRequest req) {
	///     if (req.Nickname.HasValue) {
	///         user.Nickname = req.Nickname.Value; // null means "clear it"
	///     }
	///     // else: field was omitted — user.Nickname is left untouched
	/// }
	/// </code>
	/// </example>
	/// </remarks>
	/// <typeparam name="T">The type of the wrapped value. Must be a reference type.</typeparam>
	public readonly record struct Optional<T> where T : class {
		public Optional() {
			HasValue = false;
			Value = null;
		}
		public Optional(bool hasValue, T? value) {
			this.HasValue = hasValue;
			Value = value;
		}
		/// <summary>The wrapped value, or <c>null</c> if the field was absent or explicitly nullified.</summary>
		public T? Value { get; }
		/// <summary><c>true</c> if the field was present in the request (even if its value is <c>null</c>); otherwise <c>false</c>.</summary>
		public bool HasValue { get; }

		/// <summary>
		/// Applies <paramref name="func"/> to <see cref="Value"/> and returns a new <see cref="Optional{T}"/>
		/// with the result. Returns the current instance unchanged when <see cref="HasValue"/> is <c>false</c>
		/// or <see cref="Value"/> is <c>null</c>.
		/// </summary>
		public Optional<T> Update(Func<T, T> func) {
			if (!HasValue || Value == null) {
				return this;
			}
			return new Optional<T>(true, func(Value));
		}
	}
}