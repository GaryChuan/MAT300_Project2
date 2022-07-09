using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineEditor : MonoBehaviour
{
    [SerializeField] Spline _spline;
    [SerializeField] DataListItem _dataListItemPrefab;
    [SerializeField] GameObject _content;
    [SerializeField] SplineColours _splineColours;

    List<DataListItem> _dataListItems = new List<DataListItem>();
    List<int> _selectedListItems = new List<int>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
