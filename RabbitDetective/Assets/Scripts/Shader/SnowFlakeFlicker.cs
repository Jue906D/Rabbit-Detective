using UnityEngine;

public class SnowflakeFlickerController : MonoBehaviour
{
    public float flickerSpeed = 1.0f;
    public float flickerStrength = 0.5f;
    public Texture noiseTexture;  // 雪花噪声纹理

    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        if (noiseTexture != null)
        {
            material.SetTexture("_NoiseTex", noiseTexture);
        }
    }

    void Update()
    {
        // 动态调整闪烁速度和强度
        material.SetFloat("_FlickerSpeed", flickerSpeed);
        material.SetFloat("_FlickerStrength", flickerStrength);
    }
}
