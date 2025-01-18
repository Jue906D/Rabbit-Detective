using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class OldFilmEffect : MonoBehaviour
{
    public float grainIntensity = 0.1f;          // ����ǿ��
    public float flickerIntensity = 0.05f;       // ��˸ǿ��
    public Texture grainTexture;                 // ��������
    public float flickerSpeed = 1.0f;            // ��˸�ٶ�

    private Material material;
    private Image image;

    void Start()
    {
        // ��ȡ Image ���
        image = GetComponent<Image>();

        // �����µ� Material��Ӧ�õ� Image ���
        material = new Material(Shader.Find("Custom/OldFilmEffect"));
        image.material = material;

        // ����п�������Ӧ����
        if (grainTexture != null)
        {
            material.SetTexture("_GrainTexture", grainTexture);
        }
    }

    void Update()
    {
        // ʹ��ʱ�䶯̬�ı����ǿ�ȣ����磬��ʱ��仯��
        material.SetFloat("_GrainIntensity", Mathf.PerlinNoise(Time.time * 0.5f, 0) * grainIntensity);

        // ��̬������˸ǿ�ȣ�ʹ�����Ҳ�ģ����˸Ч����
        float flicker = Mathf.Sin(Time.time * flickerSpeed) * flickerIntensity;
        material.SetFloat("_FlickerIntensity", flicker);
    }
}
