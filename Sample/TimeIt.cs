using System;
using System.Diagnostics;

namespace Sample
{
	public class TimeIt : IDisposable
	{
		private readonly string _name;
		private readonly Stopwatch _stopWatch;
		private ConsoleColor _previousColor;

		public TimeIt(string name)
		{
			_name = name;
			_stopWatch = Stopwatch.StartNew();
		}


		public void Dispose()
		{
			_stopWatch.Start();
			
			_previousColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.DarkBlue;
			Console.WriteLine($"{_name} took {_stopWatch.ElapsedMilliseconds} ms");
			Console.ForegroundColor = _previousColor;
		}
	}
}