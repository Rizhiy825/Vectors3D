using BenchmarkDotNet.Attributes;
using Vectors3D;

namespace Benchmark;

[MemoryDiagnoser]
[SimpleJob(warmupCount: 2, iterationCount: 10)]
public class ClassVector
{
    private Segment3D firstSegment;
    private Segment3D secondSegment;

    [GlobalSetup]
    public void Setup()
    {
        var firstStart = new Vector3D(10, 10, 3);
        var firstEnd = new Vector3D(0, 10, 5);
        var secondStart = new Vector3D(10, 10, 8);
        var secondEnd = new Vector3D(0, 10, 0);
        firstSegment = new Segment3D(firstStart, firstEnd);
        secondSegment = new Segment3D(secondStart, secondEnd);
    }

    [Benchmark]
    public void Run()
    {
        for (int i = 0; i < 10000; i++)
        {
            var result = Segment3D.Intersect(firstSegment, secondSegment);
        }
    }
}