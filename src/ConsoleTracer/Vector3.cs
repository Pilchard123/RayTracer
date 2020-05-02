using System;

namespace ConsoleTracer
{
    readonly struct Vector3
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double this[int i] => i == 0 ? X : (i == 1 ? Y : Z);
        public static Vector3 operator -(in Vector3 v) => new Vector3(-v.X, -v.Y, -v.Z);
        public static Vector3 operator -(in Vector3 v, in Vector3 other) => new Vector3(v.X - other.X, v.Y - other.Y, v.Z - other.Z);
        public static Vector3 operator +(in Vector3 v, in Vector3 other) => new Vector3(v.X + other.X, v.Y + other.Y, v.Z + other.Z);
        public static Vector3 operator *(in Vector3 v, double d) => new Vector3(v.X * d, v.Y * d, v.Z * d);
        public static Vector3 operator *(double d, in Vector3 v) => new Vector3(v.X * d, v.Y * d, v.Z * d);
        public static Vector3 operator /(in Vector3 v, double d) => v * (1d / d);

        public double LengthSquared => (X * X) + (Y * Y) + (Z * Z);
        public double Length => Math.Sqrt((X * X) + (Y * Y) + (Z * Z));

        public double Dot(in Vector3 other) => (X * other.X) + (Y * other.Y) + (Z * other.Z);
        public Vector3 Cross(in Vector3 other) => new Vector3(
                (Y * other.Z) - (Z * other.Y),
                (Z * other.X) - (X * other.Z),
                (X * other.Y) - (Y * other.X)
            );

        public Vector3 Normalize() => this / Length;

        private readonly static Random rng = new Random();
        public static Vector3 RandomUnitVector()
        {
            var a = rng.NextDouble(0, 2 * Math.PI);
            var z = rng.NextDouble(-1, 1);
            var r = Math.Sqrt(1 - (z * z));
            return new Vector3(
                r * Math.Cos(a),
                r * Math.Sin(a),
                z
            );
        }
    }
}
