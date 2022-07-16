using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SplineGenerator
{
    [SerializeField] List<InputData> _inputDataList = new List<InputData>();
    [SerializeField] List<MassPointData> _dataList = new List<MassPointData>();

    public List<MassPointData> DataList => _dataList;
    public List<InputData> InputDataList => _inputDataList;

    public List<Curve> GenerateSpline()
    {
        _dataList.Clear();

        // Build mass point data
        for (int i = 0; i < _inputDataList.Count; ++i)
        {
            InputData data = _inputDataList[i];

            MassPointData mpData = new MassPointData(
                new MassPoint(data.Position, data.Mass),
                new MassPoint(data.Velocity, 0),
                new MassPoint(data.Acceleration, 0));

            _dataList.Add(mpData);
        }

        // Generate velocity and acceleration data
        for(int i = 0; i < _dataList.Count - 1; ++i)
        {
            MassPointData data1 = _dataList[i];
            MassPointData data2 = _dataList[i + 1];

            MassPoint p0p1 = data2.position - data1.position;
            MassPoint v0 = data1.velocity * (Mathf.Abs(data1.velocity.mass) > 0 ? 1 : data1.position.mass);
            MassPoint a0 = data1.acceleration * (Mathf.Abs(data1.acceleration.mass) > 0 ? 1 : data1.position.mass);

            data2.velocity = 3f * p0p1 - 2f * v0 - 0.5f * a0;
            data2.acceleration = 6f * p0p1 - 6f * v0 - 2 * a0;

            _inputDataList[i + 1].SetVelocity(data2.velocity.point);
            _inputDataList[i + 1].SetAcceleration(data2.acceleration.point);
        }

        List<Curve> curveList = new List<Curve>();

        for(int i = 0; i < _dataList.Count - 1; ++i)
        {
            MassPointData data1 = _dataList[i];
            MassPointData data2 = _dataList[i + 1];

            MassPoint p0 = data1.position;
            MassPoint p1 = data2.position;
            MassPoint v0 = data1.velocity * (Mathf.Abs(data1.velocity.mass) > 0 ? 1 : data1.position.mass);
            MassPoint a0 = data1.acceleration * (Mathf.Abs(data1.acceleration.mass) > 0 ? 1 : data1.position.mass);

            MassPoint c0 =  p0 + v0 * 1 / 3f;
            MassPoint c1 = p0 + 2f * v0 * 1 / 3f + a0 * 1 / 6f;

            curveList.Add(new Curve(p0, c0, c1, p1));
        }
        
        return curveList;
    }

    public void AddData()
    {
        if(_inputDataList.Count < 10)
        {
           _inputDataList.Add(new InputData());
        }
    }

    public void RemoveData(int index)
    {
        _inputDataList.RemoveAt(index);
    }

    public void SwapPoints(int index1, int index2)
    {
        InputData temp = _inputDataList[index1];
        Vector2 velocity = temp.Velocity;
        Vector2 acceleration = temp.Acceleration;

        _inputDataList[index1] = _inputDataList[index2];
        _inputDataList[index2] = temp;

        _inputDataList[index1].SetVelocity(velocity);
        _inputDataList[index1].SetAcceleration(acceleration);
    }
}
