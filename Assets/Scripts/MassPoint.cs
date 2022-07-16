using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MassPoint
{
    [SerializeField] Vector2 _point;
    [SerializeField] float _mass;

    public Vector2 point => _point;

    public float mass => _mass;
    Vector2 value => _mass == 0 ? _point : _point * _mass;

    public MassPoint(Vector2 point, float mass)
    {
        _mass = mass;
        _point = point; 
    }

    public float this[int index]
    {
        get { return _point[index]; }
        set { _point[index] = value; }
    }

    public static MassPoint operator + (MassPoint p1, MassPoint p0)
    {
        MassPoint result = new MassPoint();

        result._mass = p1.mass + p0.mass;
        result._point = p1.value + p0.value;

        if (Mathf.Abs(result.mass) > 0)
        {
            result._point /= result.mass;
        }

        return result;
    }

    public static MassPoint operator - (MassPoint p1, MassPoint p0)
    {
        MassPoint result = new MassPoint();

        result._mass = p1.mass - p0.mass;
        result._point = p1.value - p0.value;

        if(Mathf.Abs(result.mass) > 0)
        {
            result._point /= result.mass;
        }

        return result;
    }

    public static MassPoint operator * (MassPoint mp, float f)
    {
        MassPoint result = new MassPoint();

        result._mass = mp.mass * f;
        result._point = mp.value * f;
        
        if (Mathf.Abs(result.mass) > 0)
        {
            result._point /= result.mass;
        }

        return result;
    }

    public static MassPoint operator * (float f, MassPoint mp)
    {
        return mp * f;
    }

    public static MassPoint operator / (MassPoint mp, float f)
    {
        MassPoint result = new MassPoint();

        result._mass = mp.mass / f;
        result._point = mp.value / f;
        
        if (Mathf.Abs(result.mass) > 0)
        {
            result._point /= result.mass;
        }

        return result;
    }
}
