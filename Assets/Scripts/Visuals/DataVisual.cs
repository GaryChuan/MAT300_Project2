using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataVisual : MonoBehaviour
{
    [Header("Derivatives")]
    [SerializeField] DerivativeVisual _velocityArrow;
    [SerializeField] DerivativeVisual _accelerationArrow;

    [Header("Point")]
    [SerializeField] SpriteRenderer _pointRenderer;
    [SerializeField] TMP_Text _pointLabel;

    public void SetPointLabel(string label)
    {

    }

    public void SetVelocityArrowActive(bool active)
    {
        _velocityArrow.gameObject.SetActive(active);
    }

    public void SetAccelerationArrowActive(bool active)
    {
        _accelerationArrow.gameObject.SetActive(active);
    }

    public void SetPointActive(bool active)
    {
        _pointRenderer.gameObject.SetActive(active);
        _pointLabel.gameObject.SetActive(active);
    }

    public void UpdateVelocityArrow(Vector3 velocity)
    {
        UpdateArrow(_velocityArrow, velocity);
    }

    public void UpdateAccelerationArrow(Vector3 acceleration)
    {
        UpdateArrow(_accelerationArrow, acceleration);
    }

    void UpdateArrow(DerivativeVisual arrow, Vector3 vec)
    {
        arrow.AdjustArrow(vec);
    }

    public bool ToggleVelocityArrow()
    {
        return _velocityArrow.ToggleArrow();
    }

    public bool ToggleAccelerationArrow()
    {
        return _accelerationArrow.ToggleArrow();
    }

    public void ShowVelocityArrow()
    {
        _velocityArrow.ShowArrow();
    }

    public void ShowAccelerationArrow()
    {
        _accelerationArrow.ShowArrow();
    }
}
