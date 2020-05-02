namespace ConsoleTracer.Core
{
    interface IHittable
    {
        bool Hit(in Ray r, double t_min, double t_max, out HitRecord hitRecord);
    }
}
