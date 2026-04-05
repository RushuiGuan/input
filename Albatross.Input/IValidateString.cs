using FluentValidation.Results;
using System;

namespace Albatross.Input {
	public interface IValidateString<T> : IParsable<T> where T : class, IValidateString<T> {
		string Value { get; }
		static abstract ValidationResult Validate(string? text);
	}
}