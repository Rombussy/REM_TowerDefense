using UnityEngine;
using System.Collections;

public class EntityMovementManager : MonoBehaviour
{
    [System.Serializable]
    public struct PathSegmentSetup
    {
        public Transform p0;
        public Transform p1;
        public Transform p2;
        public Transform p3;
    }

    public float _entitySpeed = 3f;

    public PathSegmentSetup[] pathSetup;
    private BezierSegment[] pathSegments;
    private int segmentIndex = 0;

    private void Start()
    {
        if (pathSetup != null && pathSetup.Length > 0)
        {
            // Read position coordinates directly from your dragged GameObjects
            pathSegments = new BezierSegment[pathSetup.Length];
            for (int i = 0; i < pathSetup.Length; i++)
            {
                pathSegments[i] = new BezierSegment(
                    pathSetup[i].p0.position,
                    pathSetup[i].p1.position,
                    pathSetup[i].p2.position,
                    pathSetup[i].p3.position
                );
            }

            Initialize(pathSegments);
        }
    }

    public void Initialize(BezierSegment[] segments)
    {
        pathSegments = segments;

        if (pathSegments != null && pathSegments.Length > 0)
        {
            transform.position = pathSegments[0].p0;
            StartCoroutine(FollowPath(segments));
        }
    }

    private IEnumerator FollowPath(BezierSegment[] pathSegments)
    {
        while (segmentIndex < pathSegments.Length)
        {
            BezierSegment currentSegment = pathSegments[segmentIndex];

            float segmentLength = Vector3.Distance(currentSegment.p0, currentSegment.p3);
            float duration = segmentLength / _entitySpeed;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                transform.position = BezierMathLibrary.Cubic(currentSegment, t);

                yield return null;
            }
            segmentIndex++;
        }
        EndPoint();
    }

    private void EndPoint()
    {
        gameObject.SetActive(true);
    }
}