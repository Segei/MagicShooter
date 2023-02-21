using UnityEngine;

namespace Script.Platforms
{
    public static class Bezier
    {
        public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return
                (Mathf.Pow(oneMinusT, 3) * p0) +
                (3f * Mathf.Pow(oneMinusT, 2) * t * p1) +
                (3f * oneMinusT * t * t * p2) +
                (t * t * t * p3);
        }

        public static Vector3 GetDirection(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return
                (3f * Mathf.Pow(oneMinusT, 2) * (p1 - p0)) +
                (6f * oneMinusT * t * (p2 - p1)) +
                (3f * t * t * (p3 - p2));
        }

        public static Vector3 GetPoint(Segment segment, float t)
        {
            return GetPoint(
                segment.Start.position,
                segment.Start.AuxiliaryPointEnd,
                segment.End.AuxiliaryPointStart,
                segment.End.position,
                t
            );
        }

        public static Vector3 GetDirection(Segment segment, float t)
        {
            return GetDirection(
                segment.Start.position,
                segment.Start.AuxiliaryPointEnd,
                segment.End.AuxiliaryPointStart,
                segment.End.position,
                t
            );
        }
    }
}