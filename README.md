## Xtensible.Time
[![Nuget](https://img.shields.io/nuget/dt/Xtensible.Time.Clock.svg?logo=Nuget&style=flat-square)](https://www.nuget.org/packages/Xtensible.Time.Clock)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/aba95264062f4ab98724a5cf4daedff0)](https://www.codacy.com/manual/giometrix/Xtensible.Time.Clock?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=giometrix/Xtensible.Time.Clock&amp;utm_campaign=Badge_Grade)
### TL;DR
An easy to use mockable clock. Pass `WallClock` to your services in production/dev code and use `MockClock` in unit tests; or, if you don't want to pass around a clock, do `Clock.Default = new MockClock();` in your unit tests.

Also includes useful rounding extension (`RoundUp`, `RoundDown`, and `RoundNearest`) that can be used on any `DateTime` or `DateTimeOffset`, including the time that comes from `Clock`.

### Usage
#### `MockClock` and `WallClock`
Xtensible.Time supports two styles.  You can write services that take in a clock object like so:
```csharp
public class MyService()
{
  private IClock Clock {get;}
  public MyService(IClock clock)
  {
    Clock = clock;
  }
  
  public DateTimeOffset GetTime()
  {
    return Clock.Default.Now;
  }
}
```

In your app (desktop, asp.net, xamarin, etc), you'd inject a `WallClock` like so:
```csharp
var service = new MyService(new WallClock());
```
and in your unit tests, you'd inject a `MockClock` like so:
```csharp
var service = new MyService(new MockClock(new DateTimeOffset(2019, 7, 1, 14, 0, 0, TimeSpan.Zero)));
```

If you find passing around clock objects cumbersome, you can override the `Default` clock in the static `Clock` object in your unit tests' constructor.  By default, the `Default` clock is a `WallClock`:
```csharp
  Clock.Default = new MockClock(new DateTimeOffset(2019, 7, 1, 14, 0, 0, TimeSpan.Zero));
```

### Adjusting the `MockClock`'s time
`MockClock` has an `Adjust` method that can update the clock.  You can use timespans or an adjustment in milliseconds:
```csharp
  var clock = new MockClock(new DateTimeOffset(2019, 7, 1, 14, 0, 0, TimeSpan.Zero));
  // new time will be Jul 1, 2019 14:01:00
  clock.Adjust(60000);
```

For your convenience, `Clock` has an `AsMockClock()` method so that you can do the following in your unit tests:
```csharp
  // new time will be Jul 1, 2019 14:01:00
  Clock.AsMockClock().Adjust(60000);
```

#### Rounding
New with 1.1, rounding extension methods have been included (`RoundUp()`, `RoundDown()`, `RoundNearest()`).  These can be used on any `DateTime` or `DateTimeOffset`.

Examples:
```csharp
var t = DateTimeOffset.Parse("2019-07-01T07:22:16.3000000Z");
// expected: Jul 1 7:22:16
var roundedDownTime = t.RoundDown(TimeSpan.FromSeconds(1));
```

Since the extension methods work with any `DateTime` or `DateTimeOffset`, they of course will work with `Clock`:
```csharp
  Clock.Default = new MockClock(new DateTimeOffset(2019, 7, 1, 14, 0, 0, TimeSpan.Zero));
  // expected: Jul 1 7:22:16
  var roundedDownTime = Clock.Default.UtcNow.RoundDown(TimeSpan.FromSeconds(1));
```
