# JPSoft.Profiling

A small profiling API.

## Getting Started

Simply do:
```csharp
var test = Profiler.BuildTest(Profiler.BuildTest((builder) =>
        builder.For<int,int>((a,b) => var c = a + b)
        .WithParameter(2)
        .WithParameter(9)
        .WithIterations(100)
        .WithName("Test")));

var profile = Profiler.Run(test);
```

Or simpler:
```csharp
var profile = Profiler.BuildAndRunTest(Profiler.BuildTest<int,int>((builder) =>
        builder.For<int,int>((a,b) => var c = a + b)
        .WithParameter(2).WithParameter(9)
        .WithIterations(100)
        .WithName("Test")));
```

You can add a timeout condition:
```
(builder) => builder.For(() => Console.Write('Hello world'))
    .WithTimeout(10000)));
```

And then reap consult the generated profile;
```csharp
profile.IsSuccess; /* <-- if test ran without an exception or timeout */
profile.Milliseconds; /* <-- test run time in ms */
profile.MillisecondsPerIteration;  
profile.IterationsPerMillisecond;
profile.StartedOn;
profile.EndedOn;
profile.TaskRunStatus;  /* <-- The original test task status */
profile.Iterations;
profile.Exception;  /* <-- case one was thrown */
```

## Prerequisites 

Must be compatible with **netstandard2.0**

### NuGet Packages

```
Install-Package JPSoft.Profiling
```

## Motivation

Premature optimization maybe the root of all evil but sometimes you just want to test the hell out of a code snippet or maybe an entire method.

I got tired of writing ```for loops``` & ```stopWatch.Start()``` to test my micro optimizations and this was a fun project to do.

Hope you guys enjoy it.

## Author

* **João Palma Sequeira**  -  [JMPSequeira](https://github.com/JMPSequeira)

## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details