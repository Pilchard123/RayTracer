namespace ConsoleTracer
{
    interface IMaterial
    {
        bool Scatter(in Ray incomingRay, HitRecord hitRecord, out Vector3 attenuation, out Ray scattered);
    }
}
