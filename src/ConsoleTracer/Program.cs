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
            var world = new HittableList(new[]{
                new Sphere(new Vector3(0,0,-1), 0.5),
                new Sphere(new Vector3(0,-100.5,-1), 100),
            });

            for (var j = img_height - 1; j >= 0; --j)
            {
                Console.WriteLine($"Scanlines remaining: {j.ToString()}");
                for (var i = 0; i < img_width; ++i)
                {
                    var u = ((double)i) / img_width;
                    var v = ((double)j) / img_height;

                    var ray = new Ray(origin, lowerLeftCorner + (u * horizontal) + (v * vertical));
                    var col = RayColor(ray, world).AsColour();
                    sw.WriteLine($"{col.R} {col.G} {col.B}");
                }
            }
            Console.WriteLine("Done");
        }

        private static Vector3 RayColor(in Ray r, HittableList world)
        {
            if (world.Hit(r, 0, double.MaxValue, out var hitRecord))
            {
                return 0.5 * (hitRecord.Normal + new Vector3(1, 1, 1));
            }

            var unitDirection = r.Direction.Normalize();
            var mappedY = 0.5 * (unitDirection.Y + 1.0);
            return ((1.0 - mappedY) * new Vector3(1, 1, 1)) + (mappedY * new Vector3(0.5, 0.7, 1));
        }
    }
}
