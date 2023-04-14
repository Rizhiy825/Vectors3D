using System.Collections.Generic;
using System.Numerics;
using BenchmarkDotNet.Attributes;
using Vectors3D;

namespace Benchmark;

[MemoryDiagnoser]
[SimpleJob(warmupCount: 2, iterationCount: 10)]
public class DotNetVector
{
    private Vector3 firstStart;
    private Vector3 firstEnd;
    private Vector3 secondStart;
    private Vector3 secondEnd;

    [GlobalSetup]
    public void Setup()
    {
        firstStart = new Vector3(10, 10, 3);
        firstEnd = new Vector3(0, 10, 5);
        secondStart = new Vector3(10, 10, 8);
        secondEnd = new Vector3(0, 10, 0);
    }

    [Benchmark]
    public void Run()
    {
        for (int i = 0; i < 10000; i++)
        {
            var result = Intersect(firstStart, firstEnd, secondStart, secondEnd);
        }
    }

    private Vector3 Intersect(Vector3 firstStart, Vector3 firstEnd, Vector3 secondStart, Vector3 secondEnd)
    {
        if (firstStart == null || 
            firstEnd == null || 
            secondStart == null || 
            secondEnd == null) throw new ArgumentException();

        var d1 = firstEnd - firstStart;
        var d2 = secondEnd - secondStart;

        var cross = Vector3.Cross(d1, d2);

        // Проверяем длину произведения векторов. Если она == 0, то либо отрезки
        // параллельны, либо коллинеарны. Тогда возвращаем null
        if (cross.Length() < 1e-9d)
        {
            throw new ArgumentException();
        }

        var r = secondStart - firstStart;

        // Находим параметры s и t из параметрических уравнений
        var s = Vector3.Dot(Vector3.Cross(r, d2), cross) / cross.LengthSquared();
        var t = Vector3.Dot(Vector3.Cross(r, d1), cross) / cross.LengthSquared();

        // Проверяем параметры. Если условие не выполнено, значит у отрезков нет общей точки
        if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
        {
            var intersectionPoint = firstStart + s * d1;

            return intersectionPoint;
        }

        throw new ArgumentException();
    }
}