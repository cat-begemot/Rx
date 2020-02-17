﻿using System;
using System.Reactive.Disposables;
using System.Threading.Channels;

namespace Sample
{
	public class MySequenceOfNumbers : IObservable<int>
	{
		public IDisposable Subscribe(IObserver<int> observer)
		{
			observer.OnNext(1);
			observer.OnNext(2);
			observer.OnNext(3);
			observer.OnCompleted();

			return Disposable.Create(() => Console.WriteLine("Object is disposed."));
		}
	}
}