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
        float oneMinusT = 1 - t;

        Vector2 p01 = oneMinusT * _massPoints[0].point + t * _massPoints[1].point;
        Vector2 p12 = oneMinusT * _massPoints[1].point + t * _massPoints[2].point;
        Vector2 p23 = oneMinusT * _massPoints[2].point + t * _massPoints[3].point;

        Vector2 p012 = oneMinusT * p01 + t * p12;
        Vector2 p123 = oneMinusT * p12 + t * p23;

        return oneMinusT * p012 + t * p123;
    }
}
