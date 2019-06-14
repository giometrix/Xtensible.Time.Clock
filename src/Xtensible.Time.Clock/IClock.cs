using System;

namespace Xtensible.Time.Domain
{
	public interface IClock
	{
		DateTimeOffset MinValue { get; }
		DateTimeOffset MaxValue { get; }
		DateTimeOffset UtcNow { get; }
		DateTimeOffset Now { get; }
	}
}