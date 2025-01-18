using UnityEngine;

public class SnowflakeFlickerController : MonoBehaviour
{
    public float flickerSpeed = 1.0f;
    public float flickerStrength = 0.5f;
    public Texture noiseTexture;  // ѩ����������

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
        // ��̬������˸�ٶȺ�ǿ��
        material.SetFloat("_FlickerSpeed", flickerSpeed);
        material.SetFloat("_FlickerStrength", flickerStrength);
    }
}
