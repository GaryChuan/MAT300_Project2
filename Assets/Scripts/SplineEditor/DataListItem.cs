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
    public TMP_Text headerText;
    public Image headerImage;

    [Header("Content")]
    public Image contentImage;

    [Header("Input Fields")]
    [SerializeField] TMP_InputField[] positionText = new TMP_InputField[2];
    [SerializeField] TMP_InputField[] velocityText = new TMP_InputField[2];
    [SerializeField] TMP_InputField[] accelerationText = new TMP_InputField[2];

    public TMP_InputField massInputField;

    [Header("Panels")]
    [SerializeField] GameObject AddVelocityPanel;
    [SerializeField] GameObject VelocityPanel;

    [SerializeField] GameObject AddAccelerationPanel;
    [SerializeField] GameObject AccelerationPanel;

    [Header("Buttons")]
    [SerializeField] GameObject RemoveVelocityButton;
    [SerializeField] GameObject RemoveAccelerationButton;

    [Header("Texts")]
    [SerializeField] TMP_Text HideShowVelocityText;
    [SerializeField] TMP_Text HideShowAccelerationText;

    public Button dropDownButton;
    public Toggle toggleButton;
    public Button deleteButton;

    [HideInInspector] public List<TMP_InputField[]> inputFields;

    public UnityAction _onAddVelocity;
    public UnityAction _onRemoveVelocity;
    public UnityAction _onAddAcceleration;
    public UnityAction _onRemoveAcceleration;

    public Func<bool> _onHideShowVelocity;
    public Func<bool> _onHideShowAcceleration;

    SplineEditor _splineEditor;

    bool toggled = true;

    void Awake()
    {
        inputFields = new List<TMP_InputField[]>();

        inputFields.Add(positionText);
        inputFields.Add(velocityText);
        inputFields.Add(accelerationText);
    }

    public void Initialize(SplineEditor editor)
    {
        _splineEditor = editor;
    }

    public void InitializePanel(int derivative)
    {
        if (derivative == 1)
        {
            VelocityPanel.SetActive(true);
            AddVelocityPanel.SetActive(false);
        }
        else if (derivative == 2 && VelocityPanel.gameObject.activeInHierarchy)
        {
            AccelerationPanel.SetActive(true);
            AddAccelerationPanel.SetActive(false);
        }
    }

    public void FlipDropdownButton()
    {
        dropDownButton.transform.rotation *= Quaternion.Euler(0, 0, 180);
    }

    public void ActivatePanel(int derivative)
    {
        if (derivative == 1)
        {
            VelocityPanel.SetActive(true);
            AddVelocityPanel.SetActive(false);
            _onAddVelocity?.Invoke();
        }
        else if (derivative == 2 && VelocityPanel.gameObject.activeInHierarchy)
        {
            AccelerationPanel.SetActive(true);
            AddAccelerationPanel.SetActive(false);
            _onAddAcceleration?.Invoke();
        }
    }

    public void DeactivatePanel(int derivative)
    {
        if (derivative == 1)
        {
            if (AccelerationPanel.gameObject.activeInHierarchy)
            {
                AccelerationPanel.SetActive(false);
                AddAccelerationPanel.SetActive(true);
                _onRemoveAcceleration?.Invoke();
            }

            VelocityPanel.SetActive(false);
            AddVelocityPanel.SetActive(true);
            _onRemoveVelocity?.Invoke();
        }
        else if (derivative == 2 && VelocityPanel.gameObject.activeInHierarchy)
        {
            AccelerationPanel.SetActive(false);
            AddAccelerationPanel.SetActive(true);
            _onRemoveAcceleration?.Invoke();
        }
    }

    public void DeactivateAllPanels()
    {
        VelocityPanel.SetActive(false);
        AddVelocityPanel.SetActive(true);
        AccelerationPanel.SetActive(false);
        AddAccelerationPanel.SetActive(true);
    }

    public void ShowVelocity()
    {
        HideShowVelocityText.text = "Hide";
    }

    public void ShowAcceleration()
    {
        HideShowAccelerationText.text = "Hide";
    }

    public void HideShowVelocityArrow()
    {
        bool show = (bool)_onHideShowVelocity?.Invoke();

        HideShowVelocityText.text = show ? "Hide" : "Show";
    }

    public void HideShowAccelerationArrow()
    {
        bool show = (bool)_onHideShowAcceleration?.Invoke();

        HideShowAccelerationText.text = show ? "Hide" : "Show";
    }
    public void OnToggle()
    {
        toggled = !toggled;
        // _dataPanel.SetActive(toggled);
    }
}
