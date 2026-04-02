using FluentValidation.Results;

namespace Albatross.Input {
	public static class Extensions {
		public static ValidationResult Validate<T>(this T request, out T sanitized) where T : class, IRequest<T> {
			sanitized = request.Sanitize();
			return T.Validator.Validate(sanitized);
		}
	}
}