using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputData
{
    [SerializeField] List<Vector2> _infoList 
        = new List<Vector2> { Vector2.zero, Vector2.zero, Vector2.zero };
    [SerializeField] float _mass = 1f;

    public InputData()
    {
        // Add and initialise position, velocity, and acceleration at (0, 0)_infoList 
        _infoList = new List<Vector2> { Vector2.zero, Vector2.zero, Vector2.zero };
        _mass = 1;
    }

    public InputData(Vector2 position, float mass)
    {
        _infoList = new List<Vector2> { Vector2.zero, Vector2.zero, Vector2.zero };
        _infoList[0] = position;
        _mass = mass;
    }

    public InputData(InputData rhs)
    {
        _infoList = new List<Vector2>(rhs._infoList);
        _mass = rhs._mass;
    }

    public Vector2 Position => _infoList[0];
    public Vector2 Velocity => _infoList[1];
    public Vector2 Acceleration => _infoList[2];
    public float Mass => _mass;

    public void SetMass(float mass)
    {
        _mass = mass;
    }

    public Vector2 GetValue(int derivative)
    {
        return _infoList[derivative];
    }

    public void SetValue(int derivative, Vector2 vec)
    {
        _infoList[derivative] = vec;
    }

    public void SetPosition(Vector2 pos)
    {
        _infoList[0] = pos;
    }

    public void SetVelocity(Vector2 vel)
    {
        _infoList[1] = vel;
    }

    public void SetAcceleration(Vector2 accel)
    {
        _infoList[2] = accel;
    }
}
