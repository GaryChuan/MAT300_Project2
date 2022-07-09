using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "SplineColours", 
    menuName = "ScriptableObjects/SplineColours", 
    order = 1)]
public class SplineColours : ScriptableObject
{
    public List<Color> _colours;
}
