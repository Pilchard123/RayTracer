using System;

namespace ConsoleTracer
{
    static class RandomExtension
    {
        public static double NextDouble(this Random rng, double min, double max) => min + ((max - min) * rng.NextDouble());
    }
}
