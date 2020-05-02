namespace ConsoleTracer.Core
{
    readonly struct Ray
    {
        public Vector3 Origin { get; }
        public Vector3 Direction { get; }


        public Ray(in Vector3 origin, in Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Vector3 PointAtParameter(double t) => Origin + (Direction * t);
    }
}
