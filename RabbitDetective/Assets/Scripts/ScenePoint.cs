using UnityEngine;

public class ScenePoint : Point
{
    [Header("允许交互时间")]
    public float StartTime;
    public float EndTime;
    public bool EnableInteract;
    
    //[Header("正确物体")] public Item RightItem;
   // [Header("正确跳转")] public Ending RightEnding;

    //[Header("特殊物体")] public Item SpecialItem;
    //[Header("特殊跳转")] public Ending SpecialEnding;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void AttachItem(Item item)
    {
        base.AttachItem(item);
        // if (SpecialItem is not null && item == SpecialItem)
        // {
        //     SpecialEnding.gameObject.SetActive(true);
        //     SpecialEnding.Activate();
        // }
        // if (RightItem is not null && item == RightItem)
        // {
        //     RightEnding.gameObject.SetActive(true);
        //     RightEnding.Activate();
        // }
    }

    public void Update()
    {
        if (GameManager.instance.TimeNow >= StartTime && GameManager.instance.TimeNow <= EndTime)
        {
            EnableInteract = true;
        }
        else
        {
            EnableInteract = false;
        }
    }
}
