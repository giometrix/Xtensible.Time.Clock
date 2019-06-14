namespace Xtensible.Time.Domain
{
	using System;

	public interface IClock
	{
		DateTimeOffset MinValue { get; }

		DateTimeOffset MaxValue { get; }

		DateTimeOffset UtcNow { get; }

		DateTimeOffset Now { get; }
	}
}
