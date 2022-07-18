using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SplineVisual
{
    [Header("Line")]
    [SerializeField] LineRenderer _lineRendererPrefab;
    List<LineRenderer> _lineRenderers = new List<LineRenderer>();

    uint _asymptoteLineCount = 0;
    List<LineRenderer> _asymptoteLineRenderers = new List<LineRenderer>();

    [Header("Points")]
    [SerializeField] DataVisual _dataVisualPrefab;
    List<DataVisual> _dataVisuals = new List<DataVisual>();

    SplineColours _splineColours;

    public void Initialize(Spline spline)
    {
        // Instantiate line renderers and data visuals
        for (int i = 0; i < Spline.MaxPoints; ++i)
        {
            LineRenderer lineRenderer =
                Object.Instantiate(_lineRendererPrefab, spline.transform);

            lineRenderer.gameObject.SetActive(false);
            lineRenderer.startWidth = 0.15f;
            lineRenderer.endWidth = 0.15f;
            _lineRenderers.Add(lineRenderer);

            DataVisual dataVisual =
                Object.Instantiate(_dataVisualPrefab, spline.transform);

            if(i == 0)
            {
                dataVisual.ShowAccelerationArrow();
                dataVisual.ShowVelocityArrow();
            }

            dataVisual.SetPointLabel("P" + i.ToString());
            dataVisual.gameObject.SetActive(false);
            _dataVisuals.Add(dataVisual);
        }
    }

    public void InitializeColours(SplineColours splineColours)
    {
        _splineColours = splineColours;

        for(int i = 0; i < Spline.MaxPoints; ++i)
        {
            _dataVisuals[i].SetPointColour(splineColours[i]);

            if(i < Spline.MaxPoints - 1)
            {
                _lineRenderers[i].startColor = splineColours[i];
                _lineRenderers[i].endColor = splineColours[(i + 1) % Spline.MaxPoints];
            }
        }
    }

    public void GenerateSplineVisuals(Spline spline)
    {
        _asymptoteLineCount = 0;

        foreach (LineRenderer lineRenderer in _lineRenderers)
        {
            lineRenderer.gameObject.SetActive(false);
        }

        foreach (LineRenderer asymptoteLR in _asymptoteLineRenderers)
        {
            asymptoteLR.gameObject.SetActive(false);
        }

        const float threshold = -0.95f;

        for(int i = 0; i < spline.Curves.Count; ++i)
        {
            Curve curve = spline.Curves[i];

            List<Vector3> points = new List<Vector3>();
            bool hasAsymptote = false;
            uint counter = 0;

            for (float t = 0f; t < 1; t += 0.001f)
            {
                Vector3 point = curve.GeneratePoint(t);

                // To add first two points
                if (counter == 0 || counter == 1)
                {
                    points.Add(point); 
                    
                    if(hasAsymptote)
                    {
                        Vector3 p = points[points.Count - 1];
                        Vector3 v = (points[points.Count - 2] - p).normalized * 1000f;
                        points.Insert(0, v + p);
                    }
                }
                else
                {
                    // Check for asymptotes
                    Vector3 v1 = (points[points.Count - 1] - points[points.Count - 2]).normalized;
                    Vector3 v2 = (point - points[points.Count - 1]).normalized;

                    if (Vector3.Dot(v1, v2) < threshold)
                    {
                        points.Add(v1 * 1000f + points[points.Count - 1]);
                        EnableAsymptoteLineRenderer(spline, points, t, i, (int)_asymptoteLineCount);

                        ++_asymptoteLineCount;

                        points.Clear();
                        points.Add(point);
                        counter = 0;

                        hasAsymptote = true;
                    }
                    else
                    {
                        points.Add(point);
                    }
                }

                ++counter;
            }

            points.Add(curve.GeneratePoint(1));

            if(hasAsymptote)
            {
                Vector3 p = points[points.Count - 1];
                Vector3 v = (points[points.Count - 2] - p).normalized * 1000f;
                points.Insert(0, v + p);
            }
            
            EnableLineRenderer(points, i);
        }
    }
    void AddAsymptoteLineRenderer(Spline spline)
    {
        LineRenderer lineRenderer =
            Object.Instantiate(_lineRendererPrefab, spline.transform);

        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;

        _asymptoteLineRenderers.Add(lineRenderer);
    }

    void EnableAsymptoteLineRenderer(Spline spline, List<Vector3> points, float t, int nodeIndex, int index)
    {
        if (index >= _asymptoteLineRenderers.Count)
        {
            AddAsymptoteLineRenderer(spline);
        }

        LineRenderer lr = _asymptoteLineRenderers[index];

        lr.gameObject.SetActive(true);
        lr.positionCount = points.Count;

        lr.startColor = Color.Lerp(
                            _splineColours[nodeIndex % _splineColours.Count],
                            _splineColours[(nodeIndex + 1) % _splineColours.Count],
                            t);
        lr.endColor = _splineColours[(nodeIndex + 1) % _splineColours.Count];

        lr.SetPositions(points.ToArray());
    }

    void EnableLineRenderer(List<Vector3> points, int index)
    {
        LineRenderer lr = _lineRenderers[index];

        lr.gameObject.SetActive(true);
        lr.positionCount = points.Count;
        lr.SetPositions(points.ToArray());
    }

    public void GenerateDataVisuals(Spline spline)
    {
        for(int i = 0; i < spline.DataList.Count; ++i)
        {
            MassPointData data = spline.DataList[i];
            DataVisual dataVisual = _dataVisuals[i];

            dataVisual.gameObject.SetActive(true);
            dataVisual.gameObject.transform.position = data.position.point;

            dataVisual.UpdateVelocityArrow(data.velocity.point);
            dataVisual.UpdateAccelerationArrow(data.acceleration.point);
        }

        for(int i = spline.DataList.Count; i < Spline.MaxPoints; ++i)
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
