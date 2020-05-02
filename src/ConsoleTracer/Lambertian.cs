namespace ConsoleTracer
{
    class Lambertian : IMaterial
    {

        public Vector3 Albedo { get; }
        public Lambertian(in Vector3 albedo)
        {
            Albedo = albedo;
        }

        public bool Scatter(in Ray incomingRay, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered)
        {
            var scatterDirection = hitRecord.Normal + Vector3.RandomUnitVector();
            scattered = new Ray(hitRecord.Point, scatterDirection);
            attenuation = Albedo;
            return true;
        }

    }
}
