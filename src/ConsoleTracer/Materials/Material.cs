using ConsoleTracer.Core;

namespace ConsoleTracer.Materials
{
    abstract class Material
    {
        public Vector3 Albedo { get; }
        public Material(in Vector3 albedo)
        {
            Albedo = albedo;
        }
        public abstract bool Scatter(in Ray incomingRay, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered);
    }
}
