using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SplineGenerator
{
    [SerializeField] Data _firstData;
    [SerializeField] List<InputData> _inputData;
    List<Data> _dataList = new List<Data>();

    public List<Data> DataList => _dataList;

    public List<Curve> GenerateSpline()
    {
        RegenerateDataList();

        List<Curve> curveList = new List<Curve>();

        for (int i = 0; i < _dataList.Count - 1; ++i)
        {
            Data data1 = _dataList[i];
            Data data2 = _dataList[i + 1];

            MassPoint p0 = new MassPoint(data1.Position, data1.Mass);
            MassPoint p1 = new MassPoint(data2.Position, data2.Mass);

            // Generate first intermediate control point
            MassPoint c0 = 
                new MassPoint(
                    p0.Point + data1.Velocity / 3f, 
                    data1.Mass);

            // Generate second intermediate conrol point
            MassPoint c1 = 
                new MassPoint(
                    p0.Point + data1.Acceleration / 6f + 2 * data1.Velocity / 3f, 
                    data1.Mass);
            
            // Generate new curve and add to list
            curveList.Add(new Curve(p0, c0, c1, p1));

            // Generate second data velocity and acceleration
            Vector2 v = data2.Position - data1.Position;

            data2.SetVelocity(3 * v - data1.Acceleration / 2f - 2 * data1.Velocity);

            data2.SetAcceleration(6 * v - 2 * data1.Acceleration - 6 * data1.Velocity);
        }

        return curveList;
    }

    void RegenerateDataList()
    {
        _dataList.Clear();

        // Add first data
        _dataList.Add(_firstData);

        foreach(InputData inputData in _inputData)
        {
            Data newData = new Data(inputData.position, inputData.mass);
            _dataList.Add(newData);
        }
    }

}
