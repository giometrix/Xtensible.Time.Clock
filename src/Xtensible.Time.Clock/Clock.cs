using System;

namespace Xtensible.Time
{
	public static class Clock
	{
		private static IClock _default = new WallClock();

		/// <summary>
		/// Switch default clock to MockClock for testing purposes (e.g. unit tests)
		/// </summary>
		public static IClock Default { get => _default; set => _default = value ?? throw new ArgumentNullException(nameof(Default)); }
		public static MockClock AsMockClock() => Default is MockClock ? Default as MockClock : new MockClock(DateTimeOffset.UtcNow);
	}
}
