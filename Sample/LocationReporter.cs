using System;

namespace Sample
{
	public class LocationReporter : IObserver<Location>
	{
		private IDisposable _unsubscriber;

		public LocationReporter(string name)
		{
			Name = name;
		}

		public string Name { get; }

		public virtual void Subscribe(IObservable<Location> provider)
		{
			if (provider != null)
				_unsubscriber = provider.Subscribe(this);
		}

		public virtual void OnCompleted()
		{
			Console.WriteLine($"The Location Tracker has complited transimitting data to {Name}");
			Unsubscribe();
		}

		public virtual void OnError(Exception error)
		{
			Console.WriteLine($"{Name}: the location cannot be determined");
		}

		public virtual void OnNext(Location value)
		{
			Console.WriteLine($"{Name}: The current location is {value.Latitude} {value.Longitude}");
		}

		public virtual void Unsubscribe()
		{
			_unsubscriber.Dispose();
		}
	}
}