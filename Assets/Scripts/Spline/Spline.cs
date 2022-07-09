using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{
    [SerializeField] SplineGenerator _splineGenerator = new SplineGenerator();
    [SerializeField] SplineVisual _splineVisual = new SplineVisual();
    [SerializeField] List<Curve> _curves;

    public List<Curve> Curves => _curves;
    public List<Data> DataList => _splineGenerator.DataList;

    void Awake()
    {
        _splineVisual.Initialize(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _curves = _splineGenerator.GenerateSpline();
        _splineVisual.GenerateSplineVisuals(this);
        _splineVisual.GenerateDataVisuals(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Initialize()
    {
        
    }
}
