namespace Xtensible.Time.Clock
{
	using System;
	using Xtensible.Time.Domain;

	public static class Clock
	{
		/// <summary>
		/// Switch default clock to MockClock for testing purposes (e.g. unit tests)
		/// </summary>
		public static IClock Default { get; set; } = new WallClock();

		public static MockClock AsMockClock() => Default is MockClock ? Default as MockClock : new MockClock(DateTimeOffset.UtcNow);
	}
}
