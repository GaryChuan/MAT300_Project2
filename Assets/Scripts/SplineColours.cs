using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "SplineColours", 
    menuName = "ScriptableObjects/SplineColours", 
    order = 1)]
public class SplineColours : ScriptableObject
{
    [SerializeField] List<Color> _colours;
    public int Count => _colours.Count;
    public Color this[int index]
    {
        get { return _colours[index]; }
    }
}
