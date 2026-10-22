using UnityEngine;

public class PathManager : MonoBehaviour
{
    [System.Serializable]
    public class PathSegments
    {
        public Transform p0;
        public Transform p1;
        public Transform p2;
        public Transform p3;
    }

    public PathSegments[] pathSetup;
    public BezierSegment[] cachedPath { get; private set; }

    private void Awake()
    {
        CachePath();
    }

    private void CachePath()
    {
        if (pathSetup == null || pathSetup.Length == 0)
            return;

        cachedPath = new BezierSegment[pathSetup.Length];
        for (int i = 0; i < pathSetup.Length; i++)
        {
            cachedPath[i] = new BezierSegment(
                pathSetup[i].p0.position,
                pathSetup[i].p1.position,
                pathSetup[i].p2.position,
                pathSetup[i].p3.position
            );
        }
    }
}