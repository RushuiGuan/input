using FluentValidation.Results;

namespace Albatross.Input {
	public static class Extensions {
		public static ValidationResult Validate<T>(this T request) where T : class, IRequest<T> {
			return T.Validator.Validate(request);
		}
	}
}