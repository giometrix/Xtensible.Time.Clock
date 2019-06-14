﻿using System;

namespace Xtensible.Time.Domain
{
	public class WallClock : IClock
	{
		public DateTimeOffset MinValue => DateTimeOffset.MinValue;
		public DateTimeOffset MaxValue => DateTimeOffset.MaxValue;
		public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
		public DateTimeOffset Now => DateTimeOffset.Now;
	}
}