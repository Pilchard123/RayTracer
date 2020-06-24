using System;

namespace ConsoleTracer.Core
{
    class Camera
    {

        private readonly Vector3 _lowerLeftCorner;
        private readonly Vector3 _horizontal;
        private readonly Vector3 _vertical;
        private readonly Vector3 _origin;

        private readonly Vector3 u;
        private readonly Vector3 v;

        private readonly double lensRadius;



        public Camera(Vector3 lookfrom, Vector3 lookat,
            Vector3 vup, double vfovDegrees, double aspect,
            double aperture, double focusDistance)
        {
            var vfovRadians = vfovDegrees * Math.PI / 180;
            var viewportHeight = 2 * Math.Tan(vfovRadians / 2);
            var viewportWidth = aspect * viewportHeight;

            var w = (lookfrom - lookat).Normalize();
            u = vup.Cross(w).Normalize();
            v = w.Cross(u);

            _origin = lookfrom;
            _horizontal = focusDistance * viewportWidth * u;
            _vertical = focusDistance * viewportHeight * v;
            _lowerLeftCorner = _origin - (_horizontal / 2) - (_vertical / 2) - (focusDistance * w);

            lensRadius = aperture / 2;
        }

        public Ray GetRay(double s, double t)
        {
            var rd = lensRadius * Vector3.RandomVectorInDisc();
            var offset = (u * rd.X) + (v * rd.Y);

            return new Ray(_origin + offset,
                 _lowerLeftCorner + (s * _horizontal) + (t * _vertical) - _origin - offset);
        }

    }
}
