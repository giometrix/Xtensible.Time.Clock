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

	}
}
