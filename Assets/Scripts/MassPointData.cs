using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MassPointData
{
    [SerializeField] List<MassPoint> _infoList = new List<MassPoint>();

    public MassPointData(MassPoint pos, MassPoint vel, MassPoint accel)
    {
        _infoList.Add(pos);
        _infoList.Add(vel);
        _infoList.Add(accel);
    }

    public MassPoint position
    {
        get { return _infoList[0]; }
        set { _infoList[0] = value; }
    }

    public MassPoint velocity
    {
        get { return _infoList[1]; }
        set { _infoList[1] = value; }
    }

    public MassPoint acceleration
    {
        get { return _infoList[2]; }
        set { _infoList[2] = value; }
    }
};
