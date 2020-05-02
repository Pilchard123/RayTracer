using System;
using ConsoleTracer.Core;

namespace ConsoleTracer.Materials
{
    class Dielectric : Material
    {

        private static readonly Random rng = new Random();

        public double RefractiveIndex { get; }
        public Dielectric(double ior) : base(new Vector3(1, 1, 1))
        {
            RefractiveIndex = ior;
        }

        public override bool Scatter(in Ray incomingRay, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered)
        {
            attenuation = Albedo;
            var refractiveIndexRatio = hitRecord.HitFrontFace ? 1.0 / RefractiveIndex : RefractiveIndex;
            var unitIncomingDirection = incomingRay.Direction.Normalize();
            var cosTheta = Math.Min(-unitIncomingDirection.Dot(hitRecord.Normal), 1.0);
            var sinTheta = Math.Sqrt(1.0 - (cosTheta * cosTheta));
            if (refractiveIndexRatio * sinTheta > 1.0 || rng.NextDouble() < Schlick(cosTheta, refractiveIndexRatio))
            {
                var reflectedDirection = Reflect(unitIncomingDirection, hitRecord.Normal);
                scattered = new Ray(hitRecord.Point, reflectedDirection);
                return true;
            }


            var refractedDirection = Refract(unitIncomingDirection, hitRecord.Normal, refractiveIndexRatio);
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

        private static double Schlick(double cosTheta, double refractiveIndexRatio)
        {
            var r0 = (1 - refractiveIndexRatio) / (1 + refractiveIndexRatio);
            r0 = r0 * r0;
            return r0 + ((1 - r0) * Math.Pow(1 - cosTheta, 5));
        }
    }
}
