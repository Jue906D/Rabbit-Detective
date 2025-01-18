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
    public Level lastLevel;
    public bool hasChanged = false;

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
        hasChanged = false;
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
            var point = GameManager.instance.TryGetPoint(item.SpawnPoint.gameObject.name);
            if (point is not null)
            {
                point.AttachItem(item);
            }
            item.gameObject.SetActive(true);
        }
    }

    public Level TryChangeLevel()
    {
        //test
        //return GameManager.instance.TryGetLevel("Level2_Scene01");
        if (LevelObject.gameObject.name == "Level1")
        {
            Levelckpt.instance.GetLv1CKPT(CheckPointsList);
            if (Levelckpt.instance.Level1CKPTResultHE())
            {
                Debug.Log("level 1通过，进2");
                return GameManager.instance.TryGetLevel("Level2");
            }
            else
            {
                return GameManager.instance.TryGetLevel("DefaultBadEnding_1");
            }
        }
        else if (LevelObject.gameObject.name == "Level2")
        {
            //ckpt.GetLv2CKPT(CheckPointsList);
            if (Levelckpt.instance.Leve2CKPTResultHE())
            {
                return GameManager.instance.TryGetLevel("Level3");
            }
            else
            {
                return GameManager.instance.TryGetLevel("DefaultBadEnding");
            }
        }
        else
        {
            return GameManager.instance.TryGetLevel("DefaultBadEnding");
        }
    }
    
    public void Update()
    {
        if (GameManager.instance.CurLevel != this)
        {
            return;
        }

        if (isSceneLevel && !hasChanged && GameManager.instance.TimeNow >= GameManager.instance.TimeAll)
        {
            hasChanged = true;
            GameManager.instance.ChangeLevel(TryChangeLevel());
        }
        foreach (var cp in CheckPointsList)
        {
            if (!cp.isChecked && GameManager.instance.TimeNow >= cp.CheckTime)
            {
                bool isPass = cp.Check();
                if (isPass)
                {
                    cp.isChecked = true;
                    // if (GameManager.instance.isPaused == true)
                    // {
                    //     GameManager.instance.isPaused = false;
                    // }
                    //Debug.Log($"通过Check{cp.gameObject.name}_{cp.CheckTime}");
                    cp.ActivateChildrenInOrder(cp.imgFBGroup_01);
                }
                else
                {
                    // if (GameManager.instance.TimeNow > cp.CheckTime || GameManager.instance.isPaused == false)
                    // {
                    //     GameManager.instance.TimeNow = cp.CheckTime;
                    //     Debug.Log($"已回到检查点{cp.gameObject.name}_{cp.CheckTime}");
                    // }
                    // if (GameManager.instance.isPaused == false)
                    // {
                    //     Debug.Log($"未通过Check{cp.gameObject.name}_{cp.CheckTime}，已暂停");
                    //     GameManager.instance.isPaused = true;
                    // }
                    //Debug.Log($"未通过Check{cp.gameObject.name}_{cp.CheckTime}");
                }
            }
        }
    }
    
}
