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
        protected static Vector3 Reflect(in Vector3 inVector, in Vector3 normal) => inVector - (2 * inVector.Dot(normal) * normal);

        public abstract bool Scatter(in Ray incomingRay, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered);
    }
}
