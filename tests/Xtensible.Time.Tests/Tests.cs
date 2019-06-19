using System;
using Xunit;
using Xunit.Sdk;

namespace Xtensible.Time.Tests
{
	public class Tests
	{
		public Tests()
		{
			Clock.Default = new WallClock();
		}

		[Theory]
		[InlineData(60)]
		[InlineData(0)]
		[InlineData(-42)]
		public void adjust_mock_clock_time(double adjustment)
		{
			var time = new DateTimeOffset(2019, 7, 1, 14, 0, 0, TimeSpan.Zero);
			Clock.Default = new MockClock(time);
			Clock.AsMockClock().Adjust(adjustment);
			Assert.Equal(time.AddMilliseconds(adjustment), Clock.Default.UtcNow);
		}

		[Fact]
		public void setting_default_clock_to_null_throws_exception()
		{
			_ = Assert.Throws<ArgumentNullException>(() => Clock.Default = null);
		}

		[Theory]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000)]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000 * 5)]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000 * 60)]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000 * 60 * 60)]
		[InlineData("2019-07-01T07:22:16.3000000Z", (long) 1000 * 60 * 60  * 24)]
		public void round_up_date_time_offset(string time, long intervalInMilliseconds)
		{
			var t = DateTimeOffset.Parse(time);
			var r = t.RoundUp(TimeSpan.FromMilliseconds(intervalInMilliseconds));
			Assert.True(r > t);

			switch (intervalInMilliseconds)
			{
				case 1000:
					Assert.Equal(17, r.Second);
					break;
				case 1000 * 5:
					Assert.Equal(20, r.Second);
					break;
				case 1000 * 60:
					Assert.Equal(23, r.Minute);
					break;
				case 1000 * 60 * 60:
					Assert.Equal(8, r.Hour);
					break;
				case 1000 * 60 * 60 * 24:
					Assert.Equal(2, r.Day);
					break;
				default:
					throw new ArgumentException();
			}
		}

		[Theory]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000)]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000 * 5)]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000 * 60)]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000 * 60 * 60)]
		[InlineData("2019-07-01T07:22:16.3000000Z", (long)1000 * 60 * 60 * 24)]
		public void round_down_date_time_offset(string time, long intervalInMilliseconds)
		{
			var t = DateTimeOffset.Parse(time);
			var r = t.RoundDown(TimeSpan.FromMilliseconds(intervalInMilliseconds));
			Assert.True(r < t);

			switch (intervalInMilliseconds)
			{
				case 1000:
					Assert.Equal(16, r.Second);
					break;
				case 1000 * 5:
					Assert.Equal(15, r.Second);
					break;
				case 1000 * 60:
					Assert.Equal(22, r.Minute);
					break;
				case 1000 * 60 * 60:
					Assert.Equal(7, r.Hour);
					break;
				case 1000 * 60 * 60 * 24:
					Assert.Equal(1, r.Day);
					break;
				default:
					throw new ArgumentException();
			}
		}

		[Theory]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000)]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000 * 5)]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000 * 60)]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000 * 60 * 60)]
		[InlineData("2019-07-01T07:22:16.3000000Z", (long)1000 * 60 * 60 * 24)]
		public void round_nearest_date_time_offset(string time, long intervalInMilliseconds)
		{
			var t = DateTimeOffset.Parse(time);
			var r = t.RoundNearest(TimeSpan.FromMilliseconds(intervalInMilliseconds));
			
			switch (intervalInMilliseconds)
			{
				case 1000:
					Assert.Equal(16, r.Second);
					break;
				case 1000 * 5:
					Assert.Equal(15, r.Second);
					break;
				case 1000 * 60:
					Assert.Equal(22, r.Minute);
					break;
				case 1000 * 60 * 60:
					Assert.Equal(7, r.Hour);
					break;
				case 1000 * 60 * 60 * 24:
					Assert.Equal(1, r.Day);
					break;
				default:
					throw new ArgumentException();
			}
		}

		[Theory]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000)]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000 * 5)]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000 * 60)]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000 * 60 * 60)]
		[InlineData("2019-07-01T07:22:16.3000000Z", (long)1000 * 60 * 60 * 24)]
		public void round_up_date_time(string time, long intervalInMilliseconds)
		{
			var t = DateTimeOffset.Parse(time).DateTime;
			var r = t.RoundUp(TimeSpan.FromMilliseconds(intervalInMilliseconds));
			Assert.True(r > t);

			switch (intervalInMilliseconds)
			{
				case 1000:
					Assert.Equal(17, r.Second);
					break;
				case 1000 * 5:
					Assert.Equal(20, r.Second);
					break;
				case 1000 * 60:
					Assert.Equal(23, r.Minute);
					break;
				case 1000 * 60 * 60:
					Assert.Equal(8, r.Hour);
					break;
				case 1000 * 60 * 60 * 24:
					Assert.Equal(2, r.Day);
					break;
				default:
					throw new ArgumentException();
			}
		}

		[Theory]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000)]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000 * 5)]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000 * 60)]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000 * 60 * 60)]
		[InlineData("2019-07-01T07:22:16.3000000Z", (long)1000 * 60 * 60 * 24)]
		public void round_down_date_time(string time, long intervalInMilliseconds)
		{
			var t = DateTimeOffset.Parse(time).DateTime;
		
			var r = t.RoundDown(TimeSpan.FromMilliseconds(intervalInMilliseconds));
			Assert.True(r < t);

			switch (intervalInMilliseconds)
			{
				case 1000:
					Assert.Equal(16, r.Second);
					break;
				case 1000 * 5:
					Assert.Equal(15, r.Second);
					break;
				case 1000 * 60:
					Assert.Equal(22, r.Minute);
					break;
				case 1000 * 60 * 60:
					Assert.Equal(7, r.Hour);
					break;
				case 1000 * 60 * 60 * 24:
					Assert.Equal(1, r.Day);
					break;
				default:
					throw new ArgumentException();
			}
		}

		[Theory]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000)]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000 * 5)]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000 * 60)]
		[InlineData("2019-07-01T07:22:16.3000000Z", 1000 * 60 * 60)]
		[InlineData("2019-07-01T07:22:16.3000000Z", (long)1000 * 60 * 60 * 24)]
		public void round_nearest_date_time(string time, long intervalInMilliseconds)
		{
			var t = DateTimeOffset.Parse(time).DateTime;
			var r = t.RoundNearest(TimeSpan.FromMilliseconds(intervalInMilliseconds));

			switch (intervalInMilliseconds)
			{
				case 1000:
					Assert.Equal(16, r.Second);
					break;
				case 1000 * 5:
					Assert.Equal(15, r.Second);
					break;
				case 1000 * 60:
					Assert.Equal(22, r.Minute);
					break;
				case 1000 * 60 * 60:
					Assert.Equal(7, r.Hour);
					break;
				case 1000 * 60 * 60 * 24:
					Assert.Equal(1, r.Day);
					break;
				default:
					throw new ArgumentException();
			}
		}

	}
}
