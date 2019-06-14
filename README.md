## Xtensible.Time

An easy to use mockable clock. Pass `WallClock` to your services in production/dev code and use MockClock in unit tests; or, if you don't want to pass around a clock, do `Clock.Default = new MockClock();` in your unit tests.
