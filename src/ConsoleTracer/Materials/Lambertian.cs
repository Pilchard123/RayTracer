using ConsoleTracer.Core;

namespace ConsoleTracer.Materials
{
    class Lambertian : Material
    {
        public Lambertian(in Vector3 albedo) : base(albedo) { }
        public override bool Scatter(in Ray incomingRay, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered)
        {
            var scatterDirection = hitRecord.Normal + Vector3.RandomUnitVector();
            scattered = new Ray(hitRecord.Point, scatterDirection);
            attenuation = Albedo;
            return true;
        }

    }
}
