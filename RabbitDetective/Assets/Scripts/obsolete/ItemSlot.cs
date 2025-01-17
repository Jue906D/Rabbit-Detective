using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public GameObject CurItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        GameManager.instance.ItemSlotList.Add(this);
    }

    void OnDisable()
    {
        GameManager.instance.ItemSlotList.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
