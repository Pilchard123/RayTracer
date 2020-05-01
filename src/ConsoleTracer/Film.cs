using System.IO;
using System.Threading.Tasks;

namespace ConsoleTracer
{
    class Film
    {

        private readonly Vector3[][] _pixelArray;
        public int Height { get; }
        public int Width { get; }
        public int SamplesPerPixel { get; }

        public Film(int height, int width, int spp)
        {
            Height = height;
            Width = width;
            SamplesPerPixel = spp;

            _pixelArray = new Vector3[height][];
            for (var i = 0; i < height; i++)
            {
                _pixelArray[i] = new Vector3[width];
            }

        }

        public void AddSample(int x, int y, in Vector3 sample) => _pixelArray[Height - y - 1][Width - x - 1] += sample;

        public async Task WriteToFile(string filename)
        {
            using var fs = new FileStream(filename, FileMode.CreateNew);
            using var sw = new StreamWriter(fs);

            await sw.WriteLineAsync("P3");
            await sw.WriteLineAsync(Width.ToString());
            await sw.WriteLineAsync(Height.ToString());
            await sw.WriteLineAsync("255");

            foreach (var row in _pixelArray)
            {
                foreach (var pixel in row)
                {
                    var scaledPixel = (pixel / SamplesPerPixel).AsColour();

                    await sw.WriteLineAsync($"{scaledPixel.X} {scaledPixel.Y} {scaledPixel.Z}");
                }
            }
        }

    }
}
