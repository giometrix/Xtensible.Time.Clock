using System;
using System.Diagnostics;

namespace Xtensible.Time.Domain
{
	public class MockClock : IClock
	{
		private DateTimeOffset _time;
		public DateTimeOffset UtcNow => _time.ToUniversalTime();
		public DateTimeOffset MinValue => DateTimeOffset.MinValue;
		public DateTimeOffset MaxValue => DateTimeOffset.MaxValue;
		public DateTimeOffset Now => _time;


		public void Adjust(TimeSpan timeSpan)
		{

			_time = _time.Add(timeSpan);
		}

		public void Adjust(DateTimeOffset time)
		{

			_time = time;
		}


		public MockClock()
		{
			_time = DateTimeOffset.UtcNow;
		}

		public MockClock(DateTimeOffset time)
		{
			_time = time;
		}
	}
}