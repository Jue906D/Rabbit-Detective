using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        GameManager.instance.ItemSlotList.Add(gameObject);
    }

    void OnDisable()
    {
        GameManager.instance.ItemSlotList.Remove(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
