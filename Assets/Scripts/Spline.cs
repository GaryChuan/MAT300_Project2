using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{
    [SerializeField] SplineGenerator _splineGenerator = new SplineGenerator();
    [SerializeField] List<Curve> _curves;


    [SerializeField] LineRenderer _lineRendererPrefab;
    List<LineRenderer> _lineRenderers = new List<LineRenderer>();

    // Start is called before the first frame update
    void Start()
    {
        _curves = _splineGenerator.GenerateSpline();

        // Instantiate line renderers
        for(int i = 0; i < 10; ++i)
        {
            LineRenderer lr = Instantiate(_lineRendererPrefab, transform);
            _lineRenderers.Add(lr);
        }

        for(int i = 0; i < _curves.Count; ++i)
        {
            List<Vector3> points = new List<Vector3>();
            Curve curve = _curves[i];

            for(float t = 0; t < 1; t += 0.01f)
            {
                points.Add(curve.GeneratePoint(t));
            }

            points.Add(curve.GeneratePoint(1));

            _lineRenderers[i].positionCount = points.Count;
            _lineRenderers[i].SetPositions(points.ToArray());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Initialize()
    {
        
    }
}
