using FluentValidation;
using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;

namespace Albatross.Input.Test {
	public class ResourceNameValidator : AbstractValidator<string?>, ICached<ResourceNameValidator> {
		public ResourceNameValidator() {
			RuleFor(x => x).NotEmpty().MaximumLength(256);
		}
	}

	public sealed class ResourceName : IValidateString<ResourceName> {
		public string Value { get; }
		public ResourceName(string value) {
			this.Value = value;
		}
		public static ResourceName Parse(string input, IFormatProvider? provider) {
			var errors = Validate(input);
			if (!errors.IsValid) {
				throw new ValidationException(errors.Errors);
			}
			return new ResourceName(input);
		}
		public static bool TryParse(string? input, IFormatProvider? provider, [MaybeNullWhen(false)] out ResourceName result) {
			var validationResult = Validate(input);
			if (validationResult.IsValid) {
				result = new ResourceName(input!);
				return true;
			} else {
				result = null;
				return false;
			}
		}
		public static ValidationResult Validate(string? text) =>
			ICached<ResourceNameValidator>.Instance.Validate(text);
	}
}