using FluentValidation;

namespace Albatross.Input {
	public interface IRequest<T> where T : class, IRequest<T> {
		T Sanitize();
		static abstract AbstractValidator<T> Validator { get; }
	}
}
