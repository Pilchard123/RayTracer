using System.Collections.Generic;

namespace ConsoleTracer.Core
{
    class HittableList : IHittable
    {
        private readonly IEnumerable<IHittable> _items;
        public HittableList(IEnumerable<IHittable> items)
        {
            _items = items;
        }

        public bool Hit(in Ray r, double t_min, double t_max, out HitRecord hitRecord)
        {
            hitRecord = null;
            var closestSoFar = t_max;

            foreach (var item in _items)
            {
                if (item.Hit(in r, t_min, closestSoFar, out var tempHitRecord))
                {
                    closestSoFar = tempHitRecord.Parameter;
                    hitRecord = tempHitRecord;
                }
            }
            return hitRecord is object;
        }
    }
}
