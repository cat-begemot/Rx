using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Xml;

namespace Sample
{
	class Program
    {
        static void Main(string[] args)
        {
	        IObservable<string> value = Observable.Throw<string>(new Exception());
	        value.Subscribe(Console.WriteLine);



        }

        private static void UseIObservableAndIObserver()
        {
			var provider = new LocationTracker();
			
			var reporter1 = new LocationReporter("FixedGPS");
			var reporter2 = new LocationReporter("MobileGPS");

			reporter1.Subscribe(provider);
			reporter2.Subscribe(provider);

			provider.TrackLocation(new Location(47.6456, -122.1312));
			reporter1.Unsubscribe();
			provider.TrackLocation(new Location(47.6677, -122.1199));
			provider.TrackLocation(null);
			provider.EndTransmission();
		}

		private static void UseUnsubscribe()
		{
			var values = new Subject<int>();
			var firstSubscription =
				values.Subscribe(value =>
					Console.WriteLine($"1st subscription received value: {value}"));
			var secondSubscription =
				values.Subscribe(value =>
					Console.WriteLine($"2nd subscription received value: {value}"));

			values.OnNext(0);
			values.OnNext(1);
			values.OnNext(2);
			values.OnNext(3);

			firstSubscription.Dispose();
			Console.WriteLine("Disposed of 1st subscription.");

			values.OnNext(4);
			values.OnNext(5);
		}

		private static void UseBehaviorSubject()
		{
			var subject = new BehaviorSubject<string>("a");
			
			subject.Subscribe(Console.WriteLine);
		}

        private static void UseReplaySubject()
        {
	        var subject = new ReplaySubject<string>(bufferSize: 2);

	        subject.OnNext("a1");
	        subject.OnNext("a2");
	        subject.OnNext("a3");

	        subject.Subscribe(value => Console.WriteLine(value));

	        subject.OnNext("b");
	        subject.OnNext("c");
        }

		private static void UseSubject()
        {
	        var subject = new Subject<string>();

			subject.Subscribe(value => Console.WriteLine(value));

			subject.OnNext("a");
			subject.OnNext("b");
			subject.OnNext("c");
        }

		private static void UseNumberSequence()
		{
			var numbers = new MySequenceOfNumbers();
			var observer = new MyConsoleObserver<int>();

			numbers.Subscribe(observer);
		}
    }
}
