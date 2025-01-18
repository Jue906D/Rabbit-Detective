using System.Collections;
using UnityEngine;

public class AvatarControl : MonoBehaviour
{

    public GameObject avatar0;
    public float time0;//״̬0����
    public GameObject avatar1;
    public float time1;//״̬1����
    public GameObject avatar2;
    public float time2;//״̬2����
    public GameObject avatar3;
    public float time3;//״̬3����

    public float fadeDuration = 1f; // ���뵭����ʱ��

    private float curTime;

    

    private void FixedUpdate()
    {
        curTime = Mathf.Floor( GameManager.instance.TimeNow);
        if (curTime<time0)
        {
            avatar0.GetComponent<CanvasGroup>().alpha =1;
            avatar1.GetComponent<CanvasGroup>().alpha = 0;
            avatar2.GetComponent<CanvasGroup>().alpha = 0;
            avatar3.GetComponent<CanvasGroup>().alpha = 0;
        }
        else if(curTime == time0)
        {
            
            FadeOut(avatar0);
            FadeIn(avatar1);
            FadeOut(avatar2);
            FadeOut(avatar3);
        }
        else if(curTime == time1)
        {
            FadeOut(avatar0);
            FadeOut(avatar1);
            FadeIn(avatar2);
            FadeOut(avatar3);
        }
        else if (curTime == time2)
        {
            FadeOut(avatar0);
            FadeOut(avatar1);
            FadeOut(avatar2);
            FadeIn(avatar3);
        }
        else if (curTime == time3)
        {
            FadeOut(avatar0);
            FadeOut(avatar1);
            FadeOut(avatar2);
            FadeOut(avatar3);
        }
        else if (curTime > time3)
        {
            avatar0.GetComponent<CanvasGroup>().alpha = 0;
            avatar1.GetComponent<CanvasGroup>().alpha = 0;
            avatar2.GetComponent<CanvasGroup>().alpha = 0;
            avatar3.GetComponent<CanvasGroup>().alpha = 1;
        }
    }


    //����
    public void FadeIn(GameObject target)
    {
        Debug.Log($"{target.name}����");
        StartCoroutine(FadeTo(target,1f));
    }

    //����
    public void FadeOut(GameObject target)
    {
        Debug.Log($"{target.name}����");
        StartCoroutine(FadeTo(target,0f));
    }

    // ִ�е���򵭳�
    private IEnumerator FadeTo( GameObject target,float targetAlpha)
    {
        CanvasGroup canvasGroup = target.GetComponent<CanvasGroup>();
        float startAlpha = canvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha; // ȷ������ֵ׼ȷ
    }


}
