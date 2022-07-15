using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    [SerializeField] Vector2 _position;
    [SerializeField] Vector2 _velocity;
    [SerializeField] Vector2 _acceleration;
    [SerializeField] float _mass = 1f;

    public Data()
    {
        _position = Vector2.zero;
        _velocity = Vector2.zero;
        _acceleration = Vector2.zero;
        _mass = 1;
    }

    public Data(Vector2 position, float mass)
    {
        _position = position;
        _mass = mass;
    }

    public Data(Data rhs)
    {
        _position = rhs._position;
        _velocity = rhs._velocity;
        _acceleration = rhs._acceleration;
        _mass = rhs._mass;
    }

    public Vector2 Position => _position;
    public Vector2 Velocity => _velocity;
    public Vector2 Acceleration => _acceleration;
    public float Mass => _mass;

    public void SetMass(float mass)
    {
        _mass = mass;
    }

    public Vector2 GetValue(int derivative)
    {
        switch (derivative)
        {
            case 0: return _position;
            case 1: return _velocity;
            case 2: return _acceleration;
        }

        return Vector2.zero;
    }

    public void SetValue(int derivative, Vector2 vec)
    {
        switch(derivative)
        {
            case 0: _position       = vec; break;
            case 1: _velocity       = vec; break;
            case 2: _acceleration   = vec; break;
            default: break; // do nothing
        }
    }

    public void SetPosition(Vector2 pos)
    {
        _position = pos;
    }

    public void SetVelocity(Vector2 vel)
    {
        _velocity = vel;
    }

    public void SetAcceleration(Vector2 accel)
    {
        _acceleration = accel;
    }
}
