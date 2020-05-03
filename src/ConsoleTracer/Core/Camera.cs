using System;

namespace ConsoleTracer.Core
{
    class Camera
    {

        private readonly Vector3 _lowerLeftCorner;
        private readonly Vector3 _horizontal;
        private readonly Vector3 _vertical;
        private readonly Vector3 _origin;



        public Camera(double vfovDegrees, double aspect)
        {
            var vfovRadians = vfovDegrees * Math.PI / 180;
            var halfHeight = Math.Tan(vfovRadians / 2);
            var halfWidth = aspect * halfHeight;

            _origin = new Vector3(0, 0, 0);
            _lowerLeftCorner = new Vector3(-halfWidth, -halfHeight, -1);
            _horizontal = new Vector3(2 * halfWidth, 0, 0);
            _vertical = new Vector3(0, 2 * halfHeight, 0);

        }

        public Ray GetRay(double u, double v) => new Ray(_origin, _lowerLeftCorner + (u * _horizontal) + (v * _vertical) - _origin);

    }
}
