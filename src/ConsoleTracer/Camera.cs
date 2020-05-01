namespace ConsoleTracer
{
    class Camera
    {

        private readonly Vector3 _lowerLeftCorner = new Vector3(-2, -1, -1);
        private readonly Vector3 _horizontal = new Vector3(4, 0, 0);
        private readonly Vector3 _vertical = new Vector3(0, 2, 0);
        private readonly Vector3 _origin = new Vector3(0, 0, 0);

        public Ray GetRay(double u, double v) => new Ray(_origin, _lowerLeftCorner + (u * _horizontal) + (v * _vertical) - _origin);

    }
}
