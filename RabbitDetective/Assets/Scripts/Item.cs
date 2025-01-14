using FluffyUnderware.DevTools.Extensions;
using UnityEngine;

public class Item : MonoBehaviour
{
    private bool isDragging = false;
    private Vector2 mouseReference = Vector2.zero;
    private Vector2 offset = Vector2.zero;

    public string ItemSlotScene;
    public string ItemSlotUI;
    
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
        
    }

    void OnEnable()
    {
        GameManager.instance.ItemList.Add(gameObject);
    }

    void OnDisable()
    {
        GameManager.instance.ItemList.Remove(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider = Physics2D.OverlapCircle(mousePos, 0.5f, 1 << LayerMask.NameToLayer("Default"));
            if (collider != null)
            {
                isDragging = true;
                mouseReference = Input.mousePosition;
                offset = collider.transform.position.ToVector2() - mousePos;
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
            Collider2D collider = Physics2D.OverlapCircle(mousePos, 0.5f, 1 << LayerMask.NameToLayer("Default"));
            if (collider != null)
            {
                Debug.Log(collider.name);
                if (collider.gameObject.name == ItemSlotScene)
                {
                    state = ItemState.OnScene;
                    gameObject.transform.position = collider.gameObject.transform.position;
                    gameObject.transform.SetParent(collider.gameObject.transform,true);
                    Debug.Log($"{gameObject.name}吸附到{collider.gameObject.name}");
                }
                else if (collider.gameObject.name == ItemSlotUI)
                {
                    state = ItemState.InPack;
                    gameObject.transform.position = collider.gameObject.transform.position;
                    gameObject.transform.SetParent(collider.gameObject.transform,true);
                    Debug.Log($"{gameObject.name}仍在槽{collider.gameObject.name}");

                }
                else
                {
                    state = ItemState.OnDrag;
                    foreach (var slot in GameManager.instance.ItemSlotList)
                    {
                        if (slot.gameObject.name == ItemSlotUI)
                        {   
                            gameObject.transform.position = slot.transform.position;
                            gameObject.transform.SetParent(slot.transform,true);
                            Debug.Log($"{gameObject.name}回到槽{slot.gameObject.name}");
                        }
                    }
                    
                }
                    
            }else
            {
                state = ItemState.InPack;
                foreach (var slot in GameManager.instance.ItemSlotList)
                {
                    if (slot.gameObject.name == ItemSlotUI)
                    {   
                        gameObject.transform.position = slot.transform.position;
                        gameObject.transform.SetParent(slot.transform,true);
                        Debug.Log($"{gameObject.name}回到槽{slot.gameObject.name}");
                    }
                }
                    
            }
        }
    }
}