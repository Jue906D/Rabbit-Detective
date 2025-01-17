using System;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("场景总时长")]
    public float LevelTime;
    [Header("场景预制初始对象")]
    public GameObject LevelObject;
    [Header("场景Point列表")]
    public List<Point> PointList;
    [Header("场景Item列表")]
    public List<Item> ItemList;
    [Header("是否是关卡场景")]
    public bool isSceneLevel;
    [Header("检查点")]
    public List<CheckPoint> CheckPointsList;
    //[Header("场景Ending列表")]
    //public List<Ending> EndingList;
    public void StartLevel()
    {
        //time
        if (isSceneLevel)
        {
            GameManager.instance.TimeAll = LevelTime;
            GameManager.instance.TimeNow = 0.0f;
            GameManager.instance.isPaused = false;
        }
        //Obj
        LevelObject.SetActive(true);
        SpawnItems();
    }
    public void ResetLevel()
    {
        //time
        if (isSceneLevel)
        {
            GameManager.instance.TimeAll = LevelTime;
            GameManager.instance.TimeNow = 0.0f;
            GameManager.instance.isPaused = false;
        }
        foreach (var item in ItemList)
        {
            if (item.CurPoint is not null)
            {
                item.CurPoint.DetachItem(item);
            }
            item.gameObject.SetActive(false);
        }
        SpawnItems();
    }
    public void LeaveLevel()
    {
        LevelObject.SetActive(false);
    }
    public void SpawnItems()
    {
        foreach (var item in ItemList)
        {
            var point = GameManager.instance.TryGetPoint(item.SpawnPoint);
            if (point is not null)
            {
                point.AttachItem(item);
            }
            item.gameObject.SetActive(true);
        }
    }

    public void Update()
    {
        if (GameManager.instance.CurLevel != this)
        {
            return;
        }
        foreach (var cp in CheckPointsList)
        {
            if (!cp.isChecked && GameManager.instance.TimeNow >= cp.CheckTime)
            {
                bool isPass = cp.Check();
                if (isPass)
                {
                    cp.isChecked = true;
                    if (GameManager.instance.isPaused == true)
                    {
                        GameManager.instance.isPaused = false;
                    }
                    Debug.Log($"通过Check{cp.gameObject.name}_{cp.CheckTime}");
                }
                else
                {
                    if (GameManager.instance.TimeNow > cp.CheckTime || GameManager.instance.isPaused == false)
                    {
                        GameManager.instance.TimeNow = cp.CheckTime;
                        Debug.Log($"已回到检查点{cp.gameObject.name}_{cp.CheckTime}");
                    }
                    if (GameManager.instance.isPaused == false)
                    {
                        Debug.Log($"未通过Check{cp.gameObject.name}_{cp.CheckTime}，已暂停");
                        GameManager.instance.isPaused = true;
                    }
                }
            }
        }
    }
    
}
