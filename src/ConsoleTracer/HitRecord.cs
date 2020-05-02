namespace ConsoleTracer
{
    class HitRecord
    {
        public Vector3 Point { get; }
        public Vector3 Normal { get; }
        public double Parameter { get; }
        public bool HitFrontFace { get; }
        public IMaterial Material { get; }

        public HitRecord(in Vector3 rayDirection, in Vector3 point, double param, in Vector3 outwardNormal, IMaterial material)
        {
            Point = point;
            Parameter = param;
            HitFrontFace = rayDirection.Dot(outwardNormal) < 0;
            Normal = HitFrontFace ? outwardNormal : -outwardNormal;
            Material = material;
        }
    }
}
