using System;
using System.Threading.Tasks;

namespace ConsoleTracer
{
    class Program
    {

        const int img_width = 400;
        const int img_height = 200;
        const int spp = 5;

        static async Task Main(string[] _)
        {
            var film = new Film(img_height, img_width, spp);
            var cam = new Camera();
            var rng = new Random();
            var world = new HittableList(new[]{
                new Sphere(new Vector3(0,0,-1), 0.5),
                new Sphere(new Vector3(0,-100.5,-1), 100),
            });

            for (var j = 0; j < img_height; j++)
            {
                Console.WriteLine($"Scanlines remaining: {(img_height - j).ToString()}");
                for (var i = 0; i < img_width; i++)
                {
                    for (var s = 0; s < spp; s++)
                    {
                        var u = (i + rng.NextDouble()) / img_width;
                        var v = (j + rng.NextDouble()) / img_height;

                        var ray = cam.GetRay(u, v);
                        film.AddSample(i, j, RayColor(ray, world, 50));
                    }
                }
            }
            Console.WriteLine("Writing film");
            await film.WriteToFile($@"C:\ExperimentalProjects\FileDrops\RaytracerOutput\img_{DateTime.UtcNow.Ticks}.ppm");
            Console.WriteLine("Done");
        }

        private static Vector3 RayColor(in Ray r, HittableList world, int depth)
        {
            if (depth <= 0)
            {
                return new Vector3(0, 0, 0);
            }

            if (world.Hit(r, 0.001, double.MaxValue, out var hitRecord))
            {
                var target = hitRecord.Point + hitRecord.Normal + Vector3.RandomUnitVector();
                return 0.5 * RayColor(new Ray(hitRecord.Point, target - hitRecord.Point), world, depth - 1);
            }

            var unitDirection = r.Direction.Normalize();
            var mappedY = 0.5 * (unitDirection.Y + 1.0);
            return ((1.0 - mappedY) * new Vector3(1, 1, 1)) + (mappedY * new Vector3(0.5, 0.7, 1));
        }
    }
}
