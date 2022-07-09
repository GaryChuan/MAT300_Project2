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

    public Data(Vector2 position, float mass)
    {
        _position = position;
        _mass = mass;
    }

    public Vector2 Position => _position;
    public Vector2 Velocity => _velocity;
    public Vector2 Acceleration => _acceleration;
    public float Mass => _mass;

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
