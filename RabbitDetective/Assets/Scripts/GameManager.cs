using System;
using System.Collections.Generic;
using FluffyUnderware.Curvy.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("控制参数")]
    public bool isPaused;
    [Header("时间参数")]
    public float TimeAll;
    public float TimeNow;
    public float TimeSpeed;
    [Header("对象挂载")]
    public GameObject StartScreen;
    public TimeSlider Slider;
    public TextMeshProUGUI TimeNowText;
    public TextMeshProUGUI TimeAllText;
    public Button StartGameButton;
    public Button ResetButton;
    public BackPack backPack;
    [Header("场景移动对象列表")]
    public List<GameObject> MoveList;
    [Header("UI槽列表")]
    public List<ItemSlot> ItemSlotList;
    
    [Header("交互槽列表")]
    public Dictionary<string,Point> PointDict;
    [Header("Item字典")]
    public Dictionary<string,Item> ItemDict;
    [Header("Level序列")]
    public Level CurLevel;
    public List<Level> LevelList;

    public Vector2 sizeDataLimit;
    

    void Awake()
    {
        //.Log("init");
        ItemSlotList = new List<ItemSlot>(20);
        PointDict = new Dictionary<string, Point>(40);
        MoveList = new List<GameObject>(20);
        ItemDict = new Dictionary<string, Item>(40);
        instance = this;
        foreach (var level in LevelList)
        {
            level.LevelObject.SetActive(false);
        }
        StartGameButton.onClick.AddListener(ChangeLevel);
        ResetButton.onClick.AddListener(ResetLevel);
        ChangeLevel();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        TimeNowText.text = TimeNow.ToString("0.00");
        TimeAllText.text = TimeAll.ToString("0.00");
        Slider.value = TimeNow / TimeAll;
    }

    private void LateUpdate()
    {
        if (!isPaused)
        {
            TimeNow += TimeSpeed * Time.deltaTime;
        }
        
    }

    public void StartGame()
    {
        StartScreen.SetActive(false);
        isPaused = false;
        //SpawnItemInitScene();
        ChangeLevel();
    }

    public void ChangeLevel()
    {
        if (CurLevel is not null)
        {
            CurLevel.LeaveLevel();
            for (int i = 0; i < LevelList.Count; i++)
            {
                if (LevelList[i] == CurLevel)
                {
                    CurLevel = LevelList[(i+1)%LevelList.Count];
                    CurLevel.lastLevel = LevelList[i];
                    break;
                }
            }
        }
        else
        {
            CurLevel = LevelList[0];
        }
        CurLevel.StartLevel();
    }

    public void ChangeLevel(Level newLevel)
    {
        Level tmp = null;
        tmp = CurLevel;
        
        CurLevel.LeaveLevel();
        Debug.Log($"上一场景{tmp.gameObject.name}，下一场景{newLevel.gameObject.name}");
        CurLevel = newLevel;
        CurLevel.lastLevel = tmp;
        CurLevel.StartLevel();
    }

    public void ResetLevel()
    {
        if (CurLevel is not null)
        {
            if (CurLevel.isSceneLevel)
            {
                CurLevel.ResetLevel();
            }
            else 
            {
                Debug.Log($"回到上一个场景{CurLevel.lastLevel.gameObject.name}");
                ChangeLevel(CurLevel.lastLevel);
            }
            
        }
    }

    public void PauseGame()
    {
        isPaused = true;
    }
    public void BackToMainMenu()
    {
        StartScreen.SetActive(true);
    }
    public void OnSliderClicked()
    {
        isPaused = true;
        TimeNow = Slider.value * TimeAll;
        TimeNowText.text = TimeNow.ToString("0.00");
        float time;
    }
    public Item TryGetItem(string itemName)
    {
        if (ItemDict.TryGetValue(itemName, out Item item))
        {
            return item;
        }
        else
        {
            Debug.LogError($"尝试获取未注册的Item对象：{itemName}");
            return null;
        }
    }
    public Point TryGetPoint(string pointName)
    {
        if (PointDict.TryGetValue(pointName, out Point point))
        {
            return point;
        }
        else
        {
            Debug.LogError($"尝试获取未注册的Point对象：{pointName}");
            return null;
        }
    }
    
    public Level TryGetLevel(string levelName)
    {
        foreach (var level in LevelList)
        {
            if (level.LevelObject.gameObject.name == levelName)
            {
                return level;    
            }
        }
        return null;    
    }
}
