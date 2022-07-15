using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DataListItem : MonoBehaviour
{
    [Header("Header")]
    public TMP_Text _headerText;
    public Image _headerImage;

    [Header("Content")]
    public Image _contentImage;

    [Header("Input Fields")]
    [SerializeField] TMP_InputField[] _positionText = new TMP_InputField[2];
    [SerializeField] TMP_InputField[] _velocityText = new TMP_InputField[2];
    [SerializeField] TMP_InputField[] _accelerationText = new TMP_InputField[2];

    public TMP_InputField massInputField;

    [Header("Panels")]
    [SerializeField] CanvasGroup _velocityPanel;
    [SerializeField] CanvasGroup _accelerationPanel;

    [Header("Texts")]
    [SerializeField] TMP_Text _hideShowVelocityText;
    [SerializeField] TMP_Text _hideShowAccelerationText;

    public Button _dropDownButton;
    public Toggle _toggleButton;
    public Button _deleteButton;

    [HideInInspector] public List<TMP_InputField[]> _inputFields;

    public Func<bool> _onToggleVelocity;
    public Func<bool> _onToggleAcceleration;

    SplineEditor _splineEditor;

    bool toggled = true;

    void Awake()
    {
        _inputFields = new List<TMP_InputField[]>();

        _inputFields.Add(_positionText);
        _inputFields.Add(_velocityText);
        _inputFields.Add(_accelerationText);
    }

    public void Initialize(SplineEditor editor)
    {
        _splineEditor = editor;
    }

    public void FlipDropdownButton()
    {
        _dropDownButton.transform.rotation *= Quaternion.Euler(0, 0, 180);
    }

    public void ShowVelocity()
    {
        _hideShowVelocityText.text = "Hide";
    }

    public void ShowAcceleration()
    {
        _hideShowAccelerationText.text = "Hide";
    }

    public void HideShowVelocityArrow()
    {
        bool show = (bool)_onToggleVelocity?.Invoke();

        _hideShowVelocityText.text = show ? "Hide" : "Show";
    }

    public void HideShowAccelerationArrow()
    {
        bool show = (bool)_onToggleAcceleration?.Invoke();

        _hideShowAccelerationText.text = show ? "Hide" : "Show";
    }
    public void OnToggle()
    {
        toggled = !toggled;
        // _dataPanel.SetActive(toggled);
    }

    public void SetVelocityPanelInteractable(bool interactable)
    {
        _velocityPanel.interactable = interactable;
    }

    public void SetAccelerationPanelInteractable(bool interactable)
    {
        _accelerationPanel.interactable = interactable;
    }
}
