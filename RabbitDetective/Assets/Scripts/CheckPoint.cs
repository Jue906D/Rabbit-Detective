using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]  
public class CheckPointLine 
{  
    public ScenePoint spoint;
    public Item ritem;  
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

    [Header("ckpt关联反馈")]
    public GameObject imgFBGroup_01;
    public GameObject imgFBGroup_02;
    public float FBTime;
    private List<GameObject> imgFBGroup;

    public bool CheckItem(CheckPointLine cpl)
    {
        if (cpl.ritem == null)
        {
            return cpl.spoint.CurItem is null;
        }
        else
        {
            return cpl.spoint.CurItem == cpl.ritem;
        }
    }
    public bool Check()
    {
        bool check = false;
        bool acheck = false;

        if (CheckPointLines.Count == 0)
        {
            return true;
        }
        
        foreach (var checkPointLine in CheckPointLines)
        {
            var p = checkPointLine.spoint;
            switch (checkPointLine.cType)
            {
                case checkType.And:
                    if (!CheckItem(checkPointLine))
                    {
                        return false;
                    }
                    else
                    {
                        acheck = true;
                    }
                    break;
                case checkType.Or:
                    if (CheckItem(checkPointLine))
                    {
                        check = true;
                    }
                    break;
                case checkType.Single:
                    if (CheckItem(checkPointLine))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
        }
        return acheck || check; 
    }


    public void CkptFB(bool isCkptTrue)
    {
        if (isCkptTrue)
        {
            if (imgFBGroup_01 != null)
            {
                ActivateChildrenInOrder(imgFBGroup_01);
            }
            
        }
        
    }

    public void ActivateChildrenInOrder(GameObject parentObject)
    {


        // 设置为暂停状态
        GameManager.instance.isPaused = true;
        StartCoroutine(ActivateChildrenCoroutine(parentObject, FBTime));
    }
    private IEnumerator ActivateChildrenCoroutine(GameObject parentObject,float seconds)
    {
        // 获取所有子对象
        int childCount = parentObject.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            // 获取当前子对象
            Transform child = parentObject.transform.GetChild(i);

            // 激活当前子对象
            child.gameObject.SetActive(true);
            Debug.Log($"Activated: {child.gameObject.name}");

            // 等待 2 秒
            yield return new WaitForSeconds(seconds);

            // 关闭当前子对象
            child.gameObject.SetActive(false);
            Debug.Log($"Deactivated: {child.gameObject.name}");
        }
        // 恢复非暂停状态
    GameManager.instance.isPaused = false;
    }
}
