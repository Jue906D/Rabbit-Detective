using UnityEngine;

public class Point : MonoBehaviour
{
    [Header("脚本，碰撞体，名称要是同一个Gameobject的")]
    [Header("当前挂载物体")]
    public Item CurItem;
    [Header("挂载位置指示对象")]
    public GameObject PointObject;
    
    void OnEnable()
    {
        //GameManager.instance.PointDict.Add(gameObject.name,this);
    }

    void OnDisable()
    {
        //GameManager.instance.PointDict.Remove(gameObject.name);
    }
    public virtual void AttachItem(Item item)
    {
        if (CurItem is not null)
        {
            DetachItemToUI(CurItem);
        }
        CurItem = item;
        item.transform.SetParent(PointObject.transform);
        item.transform.localPosition = Vector3.zero;
    }

    public virtual void DetachItem(Item item)
    {
        Debug.Log($"物体{item.gameObject.name}从{this.gameObject.name}离开");
        //item.BackToPack();
        CurItem = null;
    }
    
    public virtual void DetachItemToUI(Item item)
    {
        Debug.Log($"物体{item.gameObject.name}从{this.gameObject.name}离开");
        item.BackToPack();
        CurItem = null;
    }
}
