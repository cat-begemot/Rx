using System;
using System.Collections.Generic;

namespace Sample
{
	public class LocationTracker : IObservable<Location>
	{
		private List<IObserver<Location>> _observers;
		
		public LocationTracker()
		{
			_observers = new List<IObserver<Location>>();
		}

		public IDisposable Subscribe(IObserver<Location> observer)
		{
			if (!_observers.Contains(observer))
				_observers.Add(observer);

			return new Unsubsriber(_observers, observer);
		}

		public void TrackLocation(Nullable<Location> location)
		{
			foreach(var observer in _observers)
				if(location.HasValue)
					observer.OnNext(location.Value);
				else
					observer.OnError(new LocationUnknownException());
		}

		public void EndTransmission()
		{
			foreach(var observer in _observers.ToArray())
				if(_observers.Contains(observer))
					observer.OnCompleted();

			_observers.Clear();
		}

		private class Unsubsriber : IDisposable
		{
			private List<IObserver<Location>> _observers;
			private IObserver<Location> _observer;

			public Unsubsriber(List<IObserver<Location>> observers, IObserver<Location> observer)
			{
				_observers = observers;
				_observer = observer;
			}

			public void Dispose()
			{
				if (_observer != null && _observers.Contains(_observer))
					_observers.Remove(_observer);
			}
		}
	}
}