using System;
using System.Threading.Tasks;
using ConsoleTracer.Core;
using ConsoleTracer.Geometry;
using ConsoleTracer.Materials;

namespace ConsoleTracer
{
    class Program
    {

        static async Task Main(string[] args)
        {

            var img_width = int.Parse(args[0]);
            var img_height = int.Parse(args[1]);
            var spp = int.Parse(args[2]);
            var destination_folder = args[3];

            var film = new Film(img_height, img_width, spp);

            var lookFrom = new Vector3(3, 3, 2);
            var lookAt = new Vector3(0, 0, -1);
            var vup = new Vector3(0, 1, 0);

            var cam = new Camera(
                lookFrom,
                lookAt,
                vup,
                20,
                ((double)img_width) / img_height,
                1,
                (lookFrom - lookAt).Length
            );

            var rng = new Random(1);

            var world = new HittableList(new[]{
                new Sphere(new Vector3(0,0,-1),0.5, new Lambertian(new Vector3(0.1,0.2,0.5))),
                new Sphere(new Vector3(0,-100.5,-1), 100, new Lambertian(new Vector3(0.8,0.8,0))),
                new Sphere(new Vector3(1,0,-1),0.5, new Metal(new Vector3(0.8,0.6,0.2), 0)),
                new Sphere(new Vector3(-1,0,-1), 0.5, new Dielectric(1.5)),
                new Sphere(new Vector3(-1,0,-1), -0.45, new Dielectric(1.5)),
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
            await film.WriteToFile($@"{destination_folder}\img_{DateTime.UtcNow.Ticks}.ppm");
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
                return hitRecord.Material.Scatter(r, hitRecord, out var attenuation, out var scattered)
                    ? attenuation * RayColor(scattered, world, depth - 1)
                    : new Vector3(0, 0, 0);
            }

            var unitDirection = r.Direction.Normalize();
            var mappedY = 0.5 * (unitDirection.Y + 1.0);
            return ((1.0 - mappedY) * new Vector3(1, 1, 1)) + (mappedY * new Vector3(0.5, 0.7, 1));
        }
    }
}
