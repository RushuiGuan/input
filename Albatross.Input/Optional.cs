namespace Albatross.Input {
	public readonly record struct Optional<T> where T : class {
		public Optional() {
			HasValue = false;
			Value = null;
		}
		public Optional(bool hasValue, T? value) {
			this.HasValue = hasValue;
			Value = value;
		}
		public T? Value { get; }
		public bool HasValue { get; }
		
		public Optional<T> Update(Func<T, T> func) {
			if (!HasValue || Value == null) {
				return this;
			}
			return new Optional<T>(true, func(Value));
		}
	}
}