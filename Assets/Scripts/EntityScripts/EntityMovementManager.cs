using UnityEngine;
using System.Collections;

public class EntityMovementManager : MonoBehaviour
{
    public int damageToPlayer = 10;
    public float _entitySpeed = 3f;
    private BezierSegment[] pathSegments;
    private int segmentIndex = 0;
    

    public void Initialize(BezierSegment[] segments)
    {
        StopAllCoroutines();
        pathSegments = segments;
        segmentIndex = 0;

        if (pathSegments != null && pathSegments.Length > 0)
        {
            transform.position = pathSegments[0].p0;
            StartCoroutine(FollowPath());
        }
    }

    private IEnumerator FollowPath()
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
        if (PlayerStat.Instance != null)
        {
            PlayerStat.Instance.TakeDamage(damageToPlayer);
        }
        gameObject.SetActive(false);
    }
}   