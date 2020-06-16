using System;

namespace ConsoleTracer.Core
{
    class Camera
    {

        private readonly Vector3 _lowerLeftCorner;
        private readonly Vector3 _horizontal;
        private readonly Vector3 _vertical;
        private readonly Vector3 _origin;



        public Camera(Vector3 lookfrom, Vector3 lookat, Vector3 vup, double vfovDegrees, double aspect)
        {
            var vfovRadians = vfovDegrees * Math.PI / 180;
            var viewportHeight = 2 * Math.Tan(vfovRadians / 2);
            var viewportWidth = aspect * viewportHeight;

            var w = (lookfrom - lookat).Normalize();
            var u = vup.Cross(w).Normalize();
            var v = w.Cross(u);

            _origin = lookfrom;
            _horizontal = viewportWidth * u;
            _vertical = viewportHeight * v;
            _lowerLeftCorner = _origin - (_horizontal / 2) - (_vertical / 2) - w;
        }

        public Ray GetRay(double u, double v) => new Ray(_origin, _lowerLeftCorner + (u * _horizontal) + (v * _vertical) - _origin);

    }
}
