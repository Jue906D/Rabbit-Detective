using System;
using FluffyUnderware.DevTools.Extensions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private bool isDragging = false;
    private Vector2 mouseReference = Vector2.zero;
    private Vector2 offset = Vector2.zero;
    
    [Header("初始位置")] 
    public Point SpawnPoint;
    //[Header("从属位置")]
    //public string RightPoint;
    [Header("当前位置")]
    public Point CurPoint;
    public Vector2 rawSizeData;
    public enum ItemState
    {
        OnDrag,
        OnScene,
        InPack,
    }
    public ItemState state = ItemState.InPack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rawSizeData = GetComponent<RectTransform>().sizeDelta;
    }
    
    void Start()
    {
        CurPoint = SpawnPoint;
    }

    void OnEnable()
    {
        GameManager.instance.ItemDict.Add(gameObject.name,this);
    }

    void OnDisable()
    {
        GameManager.instance.ItemDict.Remove(gameObject.name);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider = Physics2D.OverlapCircle(mousePos, 0.5f, 1 << LayerMask.NameToLayer("Default"));
            if (collider != null && collider.gameObject.name == gameObject.name)
            {
                isDragging = true;
                mouseReference = Input.mousePosition;
                offset = collider.transform.position.ToVector2() - mousePos;
                Debug.Log($"抓到了{collider.name}");
                OnDragDown();
            }
        }
 
        if (isDragging && Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 newPosition = new Vector2(mousePos.x + offset.x, mousePos.y + offset.y);
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
 
        if (isDragging && Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] collider = Physics2D.OverlapCircleAll(mousePos, 0.5f, 1 << LayerMask.NameToLayer("Default"));
            if (collider.Length>1)
            {
                bool flag =false;
                foreach (Collider2D collider2 in collider)
                {
                    if (collider2.gameObject.GetComponent<Item>() is not null)
                    {
                        Debug.Log($"射线跳过物体{collider2.name}");
                        continue;
                    }
                    else
                    {
                        Debug.Log($"放下到{collider2.name}");
                        OnDragOver(collider2);
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    state = ItemState.InPack;
                    UIPoint uiPoint = GameManager.instance.backPack.GetFreeUIPoint();
                    AttachToPoint(uiPoint);
                    ScaleAutoFit(true);
                    Debug.Log($"{gameObject.name}无碰撞回到背包{uiPoint.gameObject.name}");
                }
            }
            else
            {
                state = ItemState.InPack;
                UIPoint uiPoint = GameManager.instance.backPack.GetFreeUIPoint();
                AttachToPoint(uiPoint);
                ScaleAutoFit(true);
                Debug.Log($"{gameObject.name}无碰撞回到背包{uiPoint.gameObject.name}");
            }
            
        }
    }

    public void AttachToPoint(Point point)
    {
        CurPoint = point;
        point.AttachItem(this);
    }

    public void DetachFromPoint()
    {
        if (CurPoint != null)
        {
            Debug.Log($"从当前{CurPoint.gameObject.name}离开");
            CurPoint.DetachItem(this);
            CurPoint = null;
        }
    }

    public void OnDragDown()
    {
       DetachFromPoint();
       ScaleAutoFit(true);
    }

    public void ScaleAutoFit(bool autoFit)
    {
        var rf = GetComponent<RectTransform>();
        if (autoFit && rf != null && isDragging)
        {
            if (rf.sizeDelta.x > GameManager.instance.sizeDataLimit.x
                || rf.sizeDelta.y > GameManager.instance.sizeDataLimit.y)
            {
                rf.sizeDelta = GameManager.instance.sizeDataLimit / Mathf.Max(rf.localScale.x, rf.localScale.y);
            }
            return;
        }

        if (!autoFit)
        {
            rf.sizeDelta = rawSizeData;
        }
    }
    
    public void OnDragOver(Collider2D collider)
    {
        //Debug.Log($"检测到{collider.gameObject.name}");
        string name = collider.gameObject.name;
        ScenePoint sp = collider.GetComponent<ScenePoint>();
         if (sp != null && sp.EnableInteract == true)
         {
             state = ItemState.OnScene;
             var rightPoint = collider.gameObject.GetComponent<Point>();
             AttachToPoint(rightPoint);
             ScaleAutoFit(false);
             Debug.Log($"{gameObject.name}吸附到允许交互的从属位置{rightPoint.gameObject.name}");
         }
        if(CurPoint is not null && name == CurPoint.gameObject.name)
        {
            state = ItemState.InPack;
            Debug.Log($"{gameObject.name}仍然在{collider.gameObject.name}");
        }
        else
        {
            state = ItemState.InPack;
            UIPoint uiPoint = GameManager.instance.backPack.GetFreeUIPoint();
            AttachToPoint(uiPoint);
            Debug.Log($"{gameObject.name}不允许交互回到背包{uiPoint.gameObject.name}");
        }
    }

    public void BackToPack()
    {
        state = ItemState.InPack;
        UIPoint uiPoint = GameManager.instance.backPack.GetFreeUIPoint();
        AttachToPoint(uiPoint);
        Debug.Log($"{gameObject.name}被挤回到背包{uiPoint.gameObject.name}");
    }
}