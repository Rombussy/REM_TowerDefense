using UnityEngine;

//Basically the math library specifically cubic for more dynamic movement for the entities, helpful to calculate the position where our child/main player 

[System.Serializable]
public struct BezierSegment
{
    public Vector3 p0;
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;

    public BezierSegment(Vector3 _startP, Vector3 _h1, Vector3 _h2, Vector3 _endP)
    {
        p0 = _startP;
        p1 = _h1;
        p2 = _h2;
        p3 = _endP;
    }
}

public static class BezierMathLibrary
{
    // We are tracking its position or pathing

    public static Vector3 Cubic(BezierSegment _segment, float t)
    {
        return Cubic(_segment.p0, _segment.p1, _segment.p2, _segment.p3, t);
    }

    public static Vector3 Cubic(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        //Math.fClamp01 ensures t its value never goes below 0 and 1 
        t = Mathf.Clamp01(t);
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;

        return (uu * u * p0) + (3f * uu * t * p1) + (3f * u * tt * p2) + (tt * t * p3);
    }

    //Method Overloading: it allows a class to have multiple methods with the same name but different parameter lists.
    public static Vector3 CubicTangent(BezierSegment _segment, float t)
    {
        return CubicTangent(_segment.p0, _segment.p1, _segment.p2, _segment.p3, t);
    }

    //responsible for Direction and Rotation of the sprite
    public static Vector3 CubicTangent(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float u = 1f - t;
        return (3f * u * u * (p1 - p0)) + (6f * u * t * (p2 - p1)) + (3f * t * t * (p3 - p2));
    }
}