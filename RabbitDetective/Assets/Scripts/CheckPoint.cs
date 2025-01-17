using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]  
public class CheckPointLine 
{  
    public ScenePoint spoint;  
    public checkType cType;  
} 
public enum checkType{
    And,
    Or,
    Single
}



public class CheckPoint : MonoBehaviour
{

    [Header("检查时刻")] public float CheckTime;
    [SerializeField]
    [Header("检查条件列表")]
    public List<CheckPointLine> CheckPointLines = new List<CheckPointLine>();
    [Header("是否已经通过")] public bool isChecked = false;

    public bool Check()
    {
        bool check = false;
        bool acheck = true;
        foreach (var checkPointLine in CheckPointLines)
        {
            var p = checkPointLine.spoint;
            switch (checkPointLine.cType)
            {
                case checkType.And:
                    if (p.CurItem != p.RightItem)
                    {
                        return false;
                    }
                    else
                    {
                        acheck = true;
                    }
                    break;
                case checkType.Or:
                    if (p.CurItem == p.RightItem)
                    {
                        check = true;
                    }
                    break;
                case checkType.Single:
                    if (p.CurItem.gameObject.name == p.RightItem.name)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
        }
        return acheck ;
    }
}
