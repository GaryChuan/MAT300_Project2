using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SplineGenerator
{
    [SerializeField] Data _firstData;
    [SerializeField] List<InputData> _inputData;
    List<Data> _dataList = new List<Data>();

    public List<Curve> GenerateSpline()
    {
        RegenerateDataList();

        List<Curve> curveList = new List<Curve>();

        for (int i = 0; i < _dataList.Count - 1; ++i)
        {
            Data firstData = _dataList[i];
            Data secondData = _dataList[i + 1];

            MassPoint p0 = new MassPoint(firstData._position, firstData._mass);
            MassPoint p1 = new MassPoint(secondData._position, secondData._mass);

            // Generate first intermediate control point
            MassPoint c0 = 
                new MassPoint(
                    p0.Point + _firstData._velocity / 3f, 
                    firstData._mass);

            // Generate second intermediate conrol point
            MassPoint c1 = 
                new MassPoint(
                    p0.Point + _firstData._acceleration / 6f + 2 * _firstData._velocity / 3f, 
                    firstData._mass);
            
            // Generate new curve and add to list
            curveList.Add(new Curve(p0, c0, c1, p1));

            // Generate second data velocity and acceleration
            Vector2 v = secondData._position - firstData._position;

            secondData._velocity =
                3 * v - _firstData._acceleration / 2f - 2 * _firstData._velocity;

            secondData._acceleration =
                6 * v - 2 * _firstData._acceleration - 6 * firstData._velocity;
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
            Data newData = new Data();

            newData._mass = inputData.mass;
            newData._position = inputData.position;

            _dataList.Add(newData);
        }
    }

}
