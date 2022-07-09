using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBackground : MonoBehaviour
{
    [SerializeField] LineRenderer _horizontalLine;
    [SerializeField] LineRenderer _verticalLine;

    void Awake()
    {
        for(int i = -15; i <= 15; ++i)
        {
            var horizontalLine = Instantiate(_horizontalLine, transform);
            var verticalLine = Instantiate(_verticalLine, transform);
        
            horizontalLine.SetPosition(0, new Vector3(-15, i, 1));
            horizontalLine.SetPosition(1, new Vector3(15, i, 1));

            verticalLine.SetPosition(0, new Vector3(i, -15, 1));
            verticalLine.SetPosition(1, new Vector3(i, 15, 1));
        }
    }
}
