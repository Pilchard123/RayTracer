using System;
using System.IO;

namespace ConsoleTracer
{
    class Program
    {

        const int img_width = 200;
        const int img_height = 100;

        static void Main(string[] _)
        {
            var filename = $@"C:\ExperimentalProjects\FileDrops\RaytracerOutput\img_{DateTime.UtcNow.Ticks}.ppm";
            using var fs = new FileStream(filename, FileMode.CreateNew);
            using var sw = new StreamWriter(fs);

            sw.WriteLine("P3");
            sw.WriteLine(img_width);
            sw.WriteLine(img_height);
            sw.WriteLine("255");

            var origin = new Vector3(0, 0, 0);
            var horizontal = new Vector3(4, 0, 0);
            var vertical = new Vector3(0, 2, 0);
            var lowerLeftCorner = new Vector3(-2, -1, -1);

            for (var j = img_height - 1; j >= 0; --j)
            {
                Console.WriteLine($"Scanlines remaining: {j.ToString()}");
                for (var i = 0; i < img_width; ++i)
                {
                    var u = ((double)i) / img_width;
                    var v = ((double)j) / img_height;

                    var ray = new Ray(origin, lowerLeftCorner + (u * horizontal) + (v * vertical));
                    var col = RayColor(ray).AsColour();
                    sw.WriteLine($"{col.R} {col.G} {col.B}");
                }
            }
            Console.WriteLine("Done");
        }

        private static Vector3 RayColor(in Ray r)
        {
            var t = HitSphere(new Vector3(0, 0, -1), 0.5, r);
            if (t > 0)
            {
                var surfaceNormal = (r.PointAtParameter(t) - new Vector3(0, 0, -1)).Normalize();
                return 0.5 * new Vector3(surfaceNormal.X + 1, surfaceNormal.Y + 1, surfaceNormal.Z + 1);
            }

            var unitDirection = r.Direction.Normalize();
            var mappedY = 0.5 * (unitDirection.Y + 1.0);
            return ((1.0 - mappedY) * new Vector3(1, 1, 1)) + (mappedY * new Vector3(0.5, 0.7, 1));
        }

        private static double HitSphere(in Vector3 center, double radius, in Ray r)
        {
            var oc = r.Origin - center;
            var a = r.Direction.LengthSquared;
            var half_b = oc.Dot(r.Direction);
            var c = oc.LengthSquared - (radius * radius);
            var discriminant = (half_b * half_b) - (a * c);
            return discriminant < 0 ? -1 : ((-half_b - Math.Sqrt(discriminant)) / a);
        }
    }
}
