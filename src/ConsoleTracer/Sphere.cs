using System;

namespace ConsoleTracer
{
    class Sphere : IHittable
    {

        public Vector3 Center { get; set; }
        public double Radius { get; set; }

        public Sphere(in Vector3 center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public bool Hit(in Ray r, double t_min, double t_max, out HitRecord hitRecord)
        {
            var oc = r.Origin - Center;
            var a = r.Direction.LengthSquared;
            var half_b = oc.Dot(r.Direction);
            var c = oc.LengthSquared - (Radius * Radius);
            var discriminant = (half_b * half_b) - (a * c);
            if (discriminant > 0)
            {

                var root = Math.Sqrt(discriminant);
                var temp = (-half_b - root) / a;
                if (temp < t_max && temp > t_min)
                {
                    var hitPoint = r.PointAtParameter(temp);
                    var outwardNormal = (hitPoint - Center) / Radius;

                    hitRecord = new HitRecord(r.Direction, hitPoint, temp, outwardNormal);

                    return true;
                }
                temp = (-half_b + root) / a;
                if (temp < t_max && temp > t_min)
                {
                    var hitPoint = r.PointAtParameter(temp);
                    var outwardNormal = (hitPoint - Center) / Radius;

                    hitRecord = new HitRecord(r.Direction, hitPoint, temp, outwardNormal);

                    return true;
                }
            }
            hitRecord = null;
            return false;
        }
    }
}
