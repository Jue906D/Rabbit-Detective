using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class OldFilmEffect : MonoBehaviour
{
    public float grainIntensity = 0.1f;          // 颗粒强度
    public float flickerIntensity = 0.05f;       // 闪烁强度
    public Texture grainTexture;                 // 颗粒纹理
    public float flickerSpeed = 1.0f;            // 闪烁速度

    private Material material;
    private Image image;

    void Start()
    {
        // 获取 Image 组件
        image = GetComponent<Image>();

        // 创建新的 Material，应用到 Image 组件
        material = new Material(Shader.Find("Custom/OldFilmEffect"));
        image.material = material;

        // 如果有颗粒纹理，应用它
        if (grainTexture != null)
        {
            material.SetTexture("_GrainTexture", grainTexture);
        }
    }

    void Update()
    {
        // 使用时间动态改变颗粒强度（例如，随时间变化）
        material.SetFloat("_GrainIntensity", Mathf.PerlinNoise(Time.time * 0.5f, 0) * grainIntensity);

        // 动态更新闪烁强度（使用正弦波模拟闪烁效果）
        float flicker = Mathf.Sin(Time.time * flickerSpeed) * flickerIntensity;
        material.SetFloat("_FlickerIntensity", flicker);
    }
}
