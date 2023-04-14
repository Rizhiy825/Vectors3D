namespace Vectors3D;

    public class Vector3D
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }
        public double Length
        {
            get
            {
                var powX = X * X;
                var powY = Y * Y;
                var powZ = Z * Z;

                return Math.Sqrt(powX + powY + powZ);
            }
        }

        public Vector3D(double x, double y, double z)
        {
            X = x; Y = y; Z = z;
        }

        public static double Scalar(Vector3D vector1, Vector3D vector2)
        {
            return (vector1.X * vector2.X)
                   + (vector1.Y * vector2.Y)
                   + (vector1.Z * vector2.Z);
        }

        public static Vector3D Cross(Vector3D vector1, Vector3D vector2)
        {
            return new Vector3D(
                (vector1.Y * vector2.Z) - (vector1.Z * vector2.Y),
                (vector1.Z * vector2.X) - (vector1.X * vector2.Z),
                (vector1.X * vector2.Y) - (vector1.Y * vector2.X)
            );
        }

        public static Vector3D operator +(Vector3D left, Vector3D right)
        {
            return new Vector3D(
                left.X + right.X,
                left.Y + right.Y,
                left.Z + right.Z
            );
        }
        
        public static Vector3D operator -(Vector3D left, Vector3D right)
        {
            return new Vector3D(
                left.X - right.X,
                left.Y - right.Y,
                left.Z - right.Z
            );
        }

        public static Vector3D operator *(double left, Vector3D right)
        {
            return new Vector3D(right.X * left, right.Y * left, right.Z * left);
        }
    }
