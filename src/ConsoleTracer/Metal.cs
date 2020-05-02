namespace ConsoleTracer
{
    class Metal : Material
    {

        public Metal(in Vector3 albedo) : base(albedo) { }

        private static Vector3 Reflect(in Vector3 inVector, in Vector3 normal) => inVector - (2 * inVector.Dot(normal) * normal);
        public override bool Scatter(in Ray incomingRay, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered)
        {
            var reflectedDirection = Reflect(incomingRay.Direction.Normalize(), hitRecord.Normal);
            scattered = new Ray(hitRecord.Point, reflectedDirection);
            attenuation = Albedo;
            return reflectedDirection.Dot(hitRecord.Normal) > 0;
        }
    }
}
