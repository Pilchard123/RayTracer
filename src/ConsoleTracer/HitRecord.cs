namespace ConsoleTracer
{
    class HitRecord
    {
        public Vector3 Point { get; }
        public Vector3 Normal { get; }
        public double Parameter { get; }
        public bool HitFrontFace { get; }
        public Material Material { get; }

        public HitRecord(in Vector3 rayDirection, in Vector3 point, double param, in Vector3 outwardNormal, Material material)
        {
            Point = point;
            Parameter = param;
            HitFrontFace = rayDirection.Dot(outwardNormal) < 0;
            Normal = HitFrontFace ? outwardNormal : -outwardNormal;
            Material = material;
        }
    }
}
