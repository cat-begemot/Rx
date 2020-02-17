using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Timers;

namespace Chapter2
{
    class Program
    {
        static void Main(string[] args)
        {
	        UseInterval();

	        Console.ReadLine();
        }

        private static void UseIntervalTimer()
        {
	        var timer = Observable.Timer(TimeSpan.FromSeconds(1));
	        timer.Subscribe(value => Console.WriteLine("Some action"), () => Console.WriteLine("Completed"));
        }

		private static void UseInterval()
        {
	        var interval = Observable.Interval(TimeSpan.FromMilliseconds(250));
	        interval.Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
        }

        private static void UseRange()
        {
	        var range = Observable.Range(10, 15);
			range.Subscribe(Console.WriteLine, () => Console.WriteLine("Completed.")).Dispose();
        }

        private static void NonBlockingEventDriven()
        {
	        var observable = Observable.Create<string>(observer =>
	        {
		        var timer = new System.Timers.Timer();
		        timer.Interval = 1000;
		        timer.Elapsed += (s, e) => observer.OnNext("tick...");
		        timer.Elapsed += OnTimerElapsed;
				timer.Start();

				return () =>
				{
					timer.Elapsed -= OnTimerElapsed;
					timer.Dispose();
				};
	        });

	        var subscription = observable.Subscribe(Console.WriteLine);

	        Console.ReadLine();
			subscription.Dispose();
			Console.ReadLine();

			void OnTimerElapsed(object sender, ElapsedEventArgs e)
            {
	            Console.WriteLine(e.SignalTime); 
            }
        }

       

        private static void UseCreate()
        {
	        var values = Observable.Create<string>(observer =>
	        {
		        observer.OnNext("a");
		        observer.OnNext("b");
		        observer.OnCompleted();
		        Thread.Sleep(1000);

		        return Disposable.Create(() => Console.WriteLine("Observer has unsubscribed."));
	        });

	        values.Subscribe(Console.WriteLine);
        }
    }
}
