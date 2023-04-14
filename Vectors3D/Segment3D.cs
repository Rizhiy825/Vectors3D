namespace Vectors3D;

public class Segment3D
{
    public Vector3D Start { get; }
    public Vector3D End { get; }

    public Segment3D(Vector3D start, Vector3D end)
    {
        Start = start; End = end;
    }

    public static Vector3D Intersect(Segment3D first, Segment3D second)
    {
        if (first == null || second == null) throw new ArgumentException();

        var d1 = first.End - first.Start;
        var d2 = second.End - second.Start;

        var cross = Vector3D.Cross(d1, d2);

        // Проверяем длину произведения векторов. Если она == 0, то либо отрезки
        // параллельны, либо коллинеарны. Тогда возвращаем null
        if (cross.Length < 1e-9d)
        {
            return null;
        }
        
        var r = second.Start - first.Start;

        // Находим параметры s и t из параметрических уравнений
        var s = Vector3D.Scalar(Vector3D.Cross(r, d2), cross) / Vector3D.Scalar(cross, cross);
        var t = Vector3D.Scalar(Vector3D.Cross(r, d1), cross) / Vector3D.Scalar(cross, cross);

        // Проверяем параметры. Если условие не выполнено, значит у отрезков нет общей точки
        if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
        {
            var intersectionPoint = first.Start + s * d1;
            
            return intersectionPoint;
        }
        
        return null;
    }
}