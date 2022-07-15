using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SplineEditor : MonoBehaviour
{
    [SerializeField] Spline _spline;
    [SerializeField] DataListItem _dataListItemPrefab;
    [SerializeField] SplineColours _splineColours;
    
    [Header("Panels")]
    [SerializeField] GameObject _content;
    [SerializeField] GameObject _dataPanel;

    [Header("Buttons")]
    [SerializeField] Button _addDataButton;
    [SerializeField] Button _swapDataButton;

    List<DataListItem> _dataListItems = new List<DataListItem>();
    List<int> _selectedListItems = new List<int>();

    int _dragIndex = -1;
    float _holdTimer = 0f;

    void Awake()
    {
        for (int i = 0; i < 10; ++i)
        {
            int index = i; // Caching index for lambdas

            DataListItem dataListItem = 
                Instantiate(_dataListItemPrefab, _content.transform);

            dataListItem.gameObject.SetActive(false);
            dataListItem.Initialize(this);

            // Only set first data list item's velocity and acceleration panel
            // to be interactable
            if(i == 0)
            {
                dataListItem.SetVelocityPanelInteractable(true);
                dataListItem.SetAccelerationPanelInteractable(true);
            }

            // Setting header text
            dataListItem._headerText.text = "P" + Convert.ToString(index);
            dataListItem._headerImage.color = _splineColours._colours[index];

            // Set colour of data list item background
            Color contentColor = _splineColours._colours[index];
            
            contentColor.a = 0.5f;
            dataListItem._contentImage.color = contentColor;

            // Set action for when toggling
            // velocity and acceleration arrows visibility
            dataListItem._onToggleVelocity = 
                () => { return ToggleVelocityArrow(index); };
            dataListItem._onToggleAcceleration = 
                () => { return ToggleAccelerationArrow(index); };

            // Clear and add on toggle button callback
            dataListItem._toggleButton.onValueChanged.RemoveAllListeners();
            dataListItem._toggleButton.onValueChanged.AddListener(
                (bool selected) => { OnDateListItemSelectionToggled(selected, index); }
            );

            // Clear and add on delete button callback
            dataListItem._deleteButton.onClick.RemoveAllListeners();
            dataListItem._deleteButton.onClick.AddListener(
                () => { RemoveData(index); });

            // Add to list of data items
            _dataListItems.Add(dataListItem);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        string filePath = Application.dataPath + "/sample.txt";

        _spline.InitializeColours(_splineColours);

        if (File.Exists(filePath))
        {
            // LoadFromFile(filePath);
        }

        _spline.Initialize();

        Initialize();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                _dragIndex = _spline.OnCollision(hit.collider);
            }
            else
            {
                _dragIndex = -1;
            }
        }

        if (Input.GetMouseButton(0) && _dragIndex != -1)
        {
            _holdTimer += Time.deltaTime;

            if (_holdTimer >= 0.05f)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                DataListItem dataListItem = _dataListItems[_dragIndex];

                dataListItem._inputFields[0][0].text = mousePos.x.ToString("F1");
                dataListItem._inputFields[0][1].text = mousePos.y.ToString("F1");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _dragIndex = -1;
            _holdTimer = 0f;
        }
    }

    void OnDateListItemSelectionToggled(bool selected, int index)
    {
        if (selected)
        {
            _selectedListItems.Add(index);

            if (_selectedListItems.Count == 2)
            {
                for (int i = 0; i < 10; ++i)
                {
                    if (i == index || _selectedListItems.Contains(i))
                    {
                        continue;
                    }

                    _dataListItems[i]._toggleButton.interactable = false;
                }
            }
        }
        else
        {
            _selectedListItems.Remove(index);

            for (int i = 0; i < 10; ++i)
            {
                _dataListItems[i]._toggleButton.interactable = true;
            }
        }

        _swapDataButton.interactable = _selectedListItems.Count == 2;
    }

    //    void LoadFromFile(string filePath)
    //    {
    //        StreamReader inputStream = File.OpenText(filePath);

    //        var vertexCountInfo = inputStream.ReadLine();
    //        int vertexCount = Convert.ToInt32(vertexCountInfo);

    //        for (int i = 0; i < vertexCount; ++i)
    //        {
    //            var info = inputStream.ReadLine()?.Trim(' ').Split(' ');

    //#nullable enable
    //            InputData? inputData = null;
    //#nullable disable

    //            // Position
    //            if (info.Length >= 2)
    //            {
    //                inputData = new InputData();

    //                inputData._dataList.Add(new Vector2(
    //                    (float)Convert.ToDouble(info[0]),
    //                    (float)Convert.ToDouble(info[1])
    //                ));
    //            }

    //            // Velocity
    //            if (info.Length >= 4)
    //            {
    //                inputData._dataList.Add(new Vector2(
    //                    (float)Convert.ToDouble(info[2]),
    //                    (float)Convert.ToDouble(info[3])
    //                ));
    //            }

    //            // Acceleration
    //            if (info.Length >= 6)
    //            {
    //                inputData._dataList.Add(new Vector2(
    //                    (float)Convert.ToDouble(info[4]),
    //                    (float)Convert.ToDouble(info[5])
    //                ));
    //            }

    //            if (inputData != null && Convert.ToBoolean(info.Length % 2))
    //            {
    //                inputData.mass = (float)Convert.ToDouble(info[info.Length - 1]);
    //            }

    //            if (inputData != null)
    //            {
    //                _spline.AddInputData(inputData);
    //            }
    //        }
    //    }

    void Initialize()
    {
        for (int i = 0; i < _spline.NumOfPoints; ++i)
        {
            DataListItem dataListItem = _dataListItems[i];
            Data data = _spline.GetData(i);

            dataListItem.transform.gameObject.SetActive(true);

            for(int j = 0; j < 3; ++j)
            {
                UpdateDataListItem(dataListItem._inputFields[j], data, j);
            }

            dataListItem.massInputField.text = Convert.ToString(data.Mass);
            dataListItem.massInputField.onValueChanged.RemoveAllListeners();
            dataListItem.massInputField.onValueChanged.AddListener(
                (string s) => {
                    try
                    {
                        float newMass = (float)Convert.ToDouble(s);

                        if (newMass == 0) return;

                        data.SetMass(Mathf.Clamp(newMass, -1f, 1f));
                        dataListItem.massInputField.text = data.Mass.ToString("F1");
                        _spline.Initialize();
                    }
                    catch (System.Exception)
                    {
                    }
                });

            dataListItem.massInputField.onSubmit.RemoveAllListeners();
            dataListItem.massInputField.onSubmit.AddListener(
                (string s) => {
                    try
                    {
                        float newMass = (float)Convert.ToDouble(s);

                        if (Mathf.Abs(newMass) == 0)
                        {
                            dataListItem.massInputField.text = data.Mass.ToString("F1");
                        }
                    }
                    catch (System.Exception)
                    {
                    }
                });
        }

        for (int i = _spline.NumOfPoints; i < 10; ++i)
        {
            _dataListItems[i].transform.gameObject.SetActive(false);
        }
    }

    void UpdateDataListItem(TMP_InputField[] inputFields, Data data, int derivative)
    {
        for (int i = 0; i < inputFields.Length; ++i)
        {
            int index = i; // Cache index for lambda

            float value = data.GetValue(derivative)[index];

            inputFields[index].onValueChanged.RemoveAllListeners();
            inputFields[index].text = value.ToString("F1");
            inputFields[index].onValueChanged.AddListener(
                (string s) =>
                {
                    float min = derivative == 0 ? -10f : -5f;
                    float max = derivative == 0 ? 10f : 5f;
                    Vector2 vec2 = data.GetValue(derivative);

                    try
                    {
                        float newValue = (float)Convert.ToDouble(s);

                        if (newValue < min || newValue > max)
                        {
                            newValue = Mathf.Clamp(newValue, min, max);
                            inputFields[index].text = newValue.ToString("F1");
                        }

                        vec2[index] = newValue;
                        data.SetValue(derivative, vec2);
                        _spline.Initialize();
                        // Update panels
                        Initialize();
                    }
                    catch (System.Exception)
                    {
                    }
                });
        }
    }

    public void SwapPoints()
    {
        if (_selectedListItems.Count != 2) return;

        _spline.SwapPoints(_selectedListItems[0], _selectedListItems[1]);

        Initialize();
    }

    public void AddData()
    {
        _spline.AddData();
        _addDataButton.interactable = _spline.DataList.Count != 10;
        Initialize();
    }

    public void RemoveData(int index)
    {
        _spline.RemoveData(index);
        _addDataButton.interactable = _spline.DataList.Count != 10;

        if(_selectedListItems.Contains(index))
        {
            _dataListItems[index]._toggleButton.isOn = false;
            _selectedListItems.Remove(index);
        }

        Initialize();
    }

    public void MinimizeEditorPanel()
    {
        _dataPanel.gameObject.SetActive(!_dataPanel.gameObject.activeSelf);
    }

    public bool ToggleVelocityArrow(int index)
    {
        return _spline.ToggleVelocityArrow(index);
    }

    public bool ToggleAccelerationArrow(int index)
    {
        return _spline.ToggleAccelerationArrow(index);
    }
}
