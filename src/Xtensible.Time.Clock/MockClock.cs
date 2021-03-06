﻿using System;

namespace Xtensible.Time
{
	public class MockClock : IClock
	{
		public DateTimeOffset UtcNow => Now.ToUniversalTime();

		public DateTimeOffset MinValue => DateTimeOffset.MinValue;

		public DateTimeOffset MaxValue => DateTimeOffset.MaxValue;

		public DateTimeOffset Now { get; private set; }

		public void Adjust(TimeSpan timeSpan)
		{
			Now = Now.Add(timeSpan);
		}

		public void Adjust(double timeInMilliseconds)
		{
			Adjust(TimeSpan.FromMilliseconds(timeInMilliseconds));
		}

		public void Adjust(DateTimeOffset time)
		{
			Now = time;
		}

		public MockClock()
		{
			Now = DateTimeOffset.UtcNow;
		}

		public MockClock(DateTimeOffset time)
		{
			Now = time;
		}
	}
}
