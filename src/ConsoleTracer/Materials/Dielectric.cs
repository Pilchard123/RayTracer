using System;
using ConsoleTracer.Core;

namespace ConsoleTracer.Materials
{
    class Dielectric : Material
    {
        public double RefractiveIndex { get; }
        public Dielectric(double ior) : base(new Vector3(1, 1, 1))
        {
            RefractiveIndex = ior;
        }

        public override bool Scatter(in Ray incomingRay, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered)
        {
            attenuation = Albedo;
            var etaiOverEtat = hitRecord.HitFrontFace ? 1.0 / RefractiveIndex : RefractiveIndex;


            var refractedDirection = Refract(incomingRay.Direction.Normalize(), hitRecord.Normal, etaiOverEtat);
            scattered = new Ray(hitRecord.Point, refractedDirection);
            return true;
        }

        private static Vector3 Refract(in Vector3 incomingDirection, in Vector3 normal, double etaiOverEtat)
        {
            var cosTheta = -incomingDirection.Dot(normal);
            var rOutParallel = etaiOverEtat * (incomingDirection + (cosTheta * normal));
            var rOutPerpendicular = -Math.Sqrt(1.0 - rOutParallel.LengthSquared) * normal;

            return rOutParallel + rOutPerpendicular;
        }
    }
}
