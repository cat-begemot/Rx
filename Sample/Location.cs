﻿namespace Sample
{
	public struct Location
	{
		public Location(double latitude, double longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}

		public double Latitude { get; }

		public double Longitude { get; }
	}
}