using UnityEngine;

public class SceneRenderer : MonoBehaviour
{
    [Header("场景存在时间")]
    public float StartTime;
    public float EndTime;
    [Header("场景实际渲染前、当前、后物体")]
    public GameObject SceneObject;
    public GameObject LastSceneObject;
    public GameObject NextSceneObject;
    // Update is called once per frame
    void Update()
    {
        float time = GameManager.instance.TimeNow;
        if (time >= StartTime && time <= EndTime)
        {
            SceneObject.SetActive(true);
        }
        else if(time > EndTime)
        {
            if (NextSceneObject != null)
            {
                NextSceneObject.SetActive(true);
            }
            SceneObject.SetActive(false);
        }
        else if (time < StartTime)
        {
            if (LastSceneObject != null)
            {
                LastSceneObject.SetActive(true);
            }
            SceneObject.SetActive(false);
        }
    }
}
