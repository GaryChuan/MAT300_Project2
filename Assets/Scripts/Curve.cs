using UnityEngine;

[System.Serializable]
public class Curve 
{
    MassPoint[] _massPoints;
    
    public Curve(MassPoint pt1, MassPoint pt2, MassPoint pt3, MassPoint pt4)
    {
        _massPoints = new MassPoint[4];

        _massPoints[0] = pt1;
        _massPoints[1] = pt2;
        _massPoints[2] = pt3;
        _massPoints[3] = pt4;
    }

    public Vector2 GeneratePoint(float t)
    {
        Vector2 p01 = (1 - t) * _massPoints[0].Point + t * _massPoints[1].Point;
        Vector2 p12 = (1 - t) * _massPoints[1].Point + t * _massPoints[2].Point;
        Vector2 p23 = (1 - t) * _massPoints[2].Point + t * _massPoints[3].Point;

        Vector2 p012 = (1 - t) * p01 + t * p12;
        Vector2 p123 = (1 - t) * p12 + t * p23;

        return (1 - t) * p012 + t * p123;
    }
}
