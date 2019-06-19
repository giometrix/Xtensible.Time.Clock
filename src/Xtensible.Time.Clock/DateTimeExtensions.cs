using System;

namespace Xtensible.Time
{
	public static class DateTimeExtensions
	{
		public static DateTimeOffset RoundUp(this DateTimeOffset time, TimeSpan interval)
		{
			var modticks = time.Ticks % interval.Ticks;
			var delta = modticks != 0 ? interval.Ticks - modticks : 0;
			return new DateTimeOffset(time.Ticks + delta, TimeSpan.Zero);
		}

		public static DateTimeOffset RoundDown(this DateTimeOffset time, TimeSpan interval)
		{
			var delta = time.Ticks % interval.Ticks;
			return new DateTimeOffset(time.Ticks - delta, TimeSpan.Zero);
		}

		public static DateTimeOffset RoundNearest(this DateTimeOffset time, TimeSpan interval)
		{
			var delta = time.Ticks % interval.Ticks;
			bool roundUp = delta > interval.Ticks / 2;
			var offset = roundUp ? interval.Ticks : 0;

			return new DateTimeOffset(time.Ticks + offset - delta, TimeSpan.Zero);
		}

		public static DateTime RoundUp(this DateTime time, TimeSpan interval)
		{
			return RoundUp((DateTimeOffset)time, interval).DateTime;
		}

		public static DateTime RoundDown(this DateTime time, TimeSpan interval)
		{
			return RoundDown((DateTimeOffset)time, interval).DateTime;
		}

		public static DateTime RoundNearest(this DateTime time, TimeSpan interval)
		{
			return RoundNearest((DateTimeOffset)time, interval).DateTime;
		}
	}
}
