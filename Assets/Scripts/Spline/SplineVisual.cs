using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SplineVisual
{
    [Header("Line")]
    [SerializeField] LineRenderer _lineRendererPrefab;
    List<LineRenderer> _lineRenderers = new List<LineRenderer>();

    [Header("Points")]
    [SerializeField] DataVisual _dataVisualPrefab;
    List<DataVisual> _dataVisuals = new List<DataVisual>();

    public void Initialize(Spline spline)
    {
        // Instantiate line renderers and data visuals
        for (int i = 0; i < 10; ++i)
        {
            LineRenderer lineRenderer =
                Object.Instantiate(_lineRendererPrefab, spline.transform);
            
            lineRenderer.gameObject.SetActive(false);
            _lineRenderers.Add(lineRenderer);

            DataVisual dataVisual =
                Object.Instantiate(_dataVisualPrefab, spline.transform);

            dataVisual.SetPointLabel("P" + i.ToString());
            dataVisual.gameObject.SetActive(false);
            _dataVisuals.Add(dataVisual);
        }
    }

    public void GenerateSplineVisuals(Spline spline)
    {
        for (int i = 0; i < spline.Curves.Count; ++i)
        {
            List<Vector3> points = new List<Vector3>();
            Curve curve = spline.Curves[i];

            for (float t = 0; t < 1; t += 0.01f)
            {
                points.Add(curve.GeneratePoint(t));
            }

            points.Add(curve.GeneratePoint(1));

            LineRenderer lr = _lineRenderers[i];

            lr.gameObject.SetActive(true);
            lr.positionCount = points.Count;
            lr.SetPositions(points.ToArray());
        }

        for(int i = spline.Curves.Count; i < 10; ++i)
        {
            _lineRenderers[i].gameObject.SetActive(false);
        }
    }

    public void GenerateDataVisuals(Spline spline)
    {
        for(int i = 0; i < spline.DataList.Count; ++i)
        {
            Data data = spline.DataList[i];
            DataVisual dataVisual = _dataVisuals[i];

            dataVisual.gameObject.SetActive(true);
            dataVisual.gameObject.transform.position = data.Position;

            dataVisual.UpdateVelocityArrow(data.Velocity);
            dataVisual.UpdateAccelerationArrow(data.Acceleration);
        }

        for(int i = spline.DataList.Count; i < 10; ++i)
        {
            _dataVisuals[i].gameObject.SetActive(false);
        }
    }

    public bool ToggleVelocityArrow(int index)
    {
        return _dataVisuals[index].ToggleVelocityArrow();
    }

    public bool ToggleAccelerationArrow(int index)
    {
        return _dataVisuals[index].ToggleAccelerationArrow();
    }

    public int OnCollision(Collider2D collider)
    {
        int result = -1;

        for(int i = 0; i < _dataVisuals.Count; ++i)
        {
            DataVisual dataVisual = _dataVisuals[i];
            
            if(dataVisual.CheckCollision(collider))
            {
                result = i;
                break;
            }
        }

        return result;
    }
}
