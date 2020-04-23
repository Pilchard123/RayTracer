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
            using var fs = new FileStream($@"C:\ExperimentalProjects\FileDrops\RaytracerOutput\img_{DateTime.UtcNow.Ticks}.ppm", FileMode.CreateNew);
            using var sw = new StreamWriter(fs);

            sw.WriteLine("P3");
            sw.WriteLine(img_width);
            sw.WriteLine(img_height);
            sw.WriteLine("255");

            for (var j = img_height - 1; j >= 0; --j)
            {
                Console.WriteLine($"Scanlines remaining: {j.ToString()}");
                for (var i = 0; i < img_width; ++i)
                {
                    var r = (double)i / img_width;
                    var g = (double)j / img_height;
                    var b = 0.2;
                    var ir = (int)(255.999 * r);
                    var ig = (int)(255.999 * g);
                    var ib = (int)(255.999 * b);
                    sw.WriteLine($"{ir} {ig} {ib}");
                }
            }
            Console.WriteLine("Done");
        }
    }
}
