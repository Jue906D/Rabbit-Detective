using UnityEngine;

public class SceneRenderer : MonoBehaviour
{
    [Header("场景存在时间")]
    public float StartTime;
    public float EndTime;
    [Header("场景实际渲染物体")]
    public GameObject SceneObject;
    // Update is called once per frame
    void Update()
    {
        float time = GameManager.instance.TimeNow;
        if (time >= StartTime && time <= EndTime)
        {
            SceneObject.SetActive(true);
        }
        else
        {
            SceneObject.SetActive(false);
        }
    }
}
