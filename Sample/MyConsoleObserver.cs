using System;

namespace Sample
{
	public class MyConsoleObserver<T> : IObserver<T>
	{
		public void OnCompleted()
		{
			Console.WriteLine("Sequence terminated.");
		}

		public void OnError(Exception error)
		{
			Console.WriteLine($"Received error: {error.ToString()}");

		}

		public void OnNext(T value)
		{
			Console.WriteLine($"Received value: {value.ToString()}");
		}
	}
}