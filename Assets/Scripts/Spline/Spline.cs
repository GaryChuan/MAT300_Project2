using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{
    [SerializeField] SplineGenerator _splineGenerator = new SplineGenerator();
    [SerializeField] SplineVisual _splineVisual = new SplineVisual();
    [SerializeField] List<Curve> _curves;

    public List<Curve> Curves => _curves;
    public List<InputData> InputDataList => _splineGenerator.InputDataList;
    public List<MassPointData> DataList => _splineGenerator.DataList;

    public int NumOfPoints => DataList.Count;
    public const int MaxPoints = 20;

    void Awake()
    {
        _splineVisual.Initialize(this);
    }
    public void AddInputData(InputData _data)
    {
        _splineGenerator.AddInputData(_data);
    }

    public void Initialize()
    {
        _curves = _splineGenerator.GenerateSpline();
        _splineVisual.GenerateSplineVisuals(this);
        _splineVisual.GenerateDataVisuals(this);
    }

    public void AddData()
    {
        _splineGenerator.AddData();
        Initialize();
    }

    public void RemoveData(int index)
    {
        _splineGenerator.RemoveData(index);
        Initialize();
    }

    public void InitializeColours(SplineColours splineColours)
    {
        _splineVisual.InitializeColours(splineColours);
    }

    public int OnCollision(Collider2D collider)
    {
        return _splineVisual.OnCollision(collider);
    }

    public MassPointData GetData(int index)
    {
        return DataList[index];
    }

    public InputData GetInputData(int index)
    {
        return InputDataList[index];
    }

    public void SwapPoints(int index1, int index2)
    {
        _splineGenerator.SwapPoints(index1, index2);
        Initialize();
    }

    public bool ToggleVelocityArrow(int index)
    {
        return _splineVisual.ToggleVelocityArrow(index);
    }

    public bool ToggleAccelerationArrow(int index)
    {
        return _splineVisual.ToggleAccelerationArrow(index);
    }
}
