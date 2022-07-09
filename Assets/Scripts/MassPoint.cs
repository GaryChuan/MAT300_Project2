using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MassPoint
{
    Vector2 _point;
    float _mass;

    public Vector2 Point => _point;
    public float Mass => _mass;

    public MassPoint(Vector2 point, float mass)
    {
        _mass = mass;
        _point = point; 
    }
}
