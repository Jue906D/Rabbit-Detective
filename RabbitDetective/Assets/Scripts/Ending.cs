using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    [Header("强制持续时间")] public float forceTime = 0.0f;
    private bool isClicked = false;
    [Header("是否强制等待")]
    public bool isForceWaiting = false;
    [Header("是否要求点击")]
    public bool isForceClick = false;

    void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { isClicked = true; });
    }
    public void Activate()
    {
        Debug.Log("激活跳转");
        isClicked = false;
        Coroutine c = StartCoroutine(Wait(forceTime,isForceWaiting));
    }
    
    public IEnumerator Wait(float time, bool force = false) {
        float t = 0.0f;
        while (t < time)
        {
            if (!force && isClicked)
            {
                break;
            }
            t += Time.deltaTime;
            yield return null;
        }

        while (isForceClick && !isClicked)
        {
            yield return null;
        }
        Debug.Log("退出");
        gameObject.SetActive(false);
        
            
    }
}
