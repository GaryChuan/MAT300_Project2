using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DerivativeVisual : MonoBehaviour
{
    [SerializeField] GameObject _arrow;
    [SerializeField] GameObject _line;

    Vector3 _initialScale;

    void Awake()
    {
        _initialScale = _line.transform.localScale;
    }

    public void AdjustArrow(Vector3 v)
    {
        Vector3 v1 = Vector3.Cross(Vector3.right, v);
        float Angle = Vector3.Angle(v, Vector3.right);

        Angle *= v1.z < 0 ? -1 : 1;

        Quaternion rotation = Quaternion.Euler(0, 0, Angle);

        _arrow.transform.localPosition = new Vector3(
            _initialScale.x * v.magnitude, 0, -2f
        );

        transform.rotation = rotation;
        _line.transform.localScale = new Vector3(
            _initialScale.x * v.magnitude, _initialScale.y, 1f
        );
    }

    public bool ToggleArrow()
    {
        _arrow.SetActive(!_arrow.activeSelf);
        _line.SetActive(!_line.activeSelf);

        return _arrow.activeSelf;
    }

    public void ShowArrow()
    {
        _arrow.SetActive(true);
        _line.SetActive(true);
    }
}
