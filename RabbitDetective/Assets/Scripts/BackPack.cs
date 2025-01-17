using System.Collections.Generic;
using UnityEngine;

public class BackPack : MonoBehaviour
{
    private int pointNum = 0;
    public List<UIPoint> UIPointList;
    public GameObject BackPackUI;

    public GameObject UIPointPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        UIPointList = new List<UIPoint>(10);
    }

    void Start()
    {
        GameManager.instance.backPack = this;
    }

    public UIPoint AddUIPoint()
    {
        UIPoint nPoint = Instantiate(UIPointPrefab, BackPackUI.transform, false).GetComponent<UIPoint>();
        nPoint.gameObject.SetActive(false);
        nPoint.gameObject.name = $"{nPoint.gameObject.name}_{pointNum++}";
        nPoint.gameObject.SetActive(true);
        UIPointList.Add(nPoint);
        return nPoint;
    }
    public UIPoint GetFreeUIPoint()
    {
        foreach (var uiPoint in UIPointList)
        {
            if (uiPoint.CurItem is null)
            {
                return uiPoint;
            }
        }
        return AddUIPoint();
    }
    
    
}
