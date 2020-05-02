using ConsoleTracer.Core;

namespace ConsoleTracer.Materials
{
    class Metal : Material
    {

        public double Fuzziness { get; }

        public Metal(in Vector3 albedo, double fuzz) : base(albedo)
        {
            Fuzziness = fuzz < 1 ? fuzz : 1;
        }
        public Metal(in Vector3 albedo) : this(albedo, 0) { }

        public override bool Scatter(in Ray incomingRay, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered)
        {
            var reflectedDirection = Reflect(incomingRay.Direction.Normalize(), hitRecord.Normal);

            scattered = new Ray(hitRecord.Point, reflectedDirection + (Fuzziness * Vector3.RandomUnitVector()));
            attenuation = Albedo;
            return reflectedDirection.Dot(hitRecord.Normal) > 0;
        }
    }
}
