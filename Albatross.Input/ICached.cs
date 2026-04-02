namespace Albatross.Input {
	public interface ICached<out T> where T:ICached<T>, new() {
		public static T Instance { get; } = new();
	}
}