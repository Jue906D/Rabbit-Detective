using System;
using System.Collections.Generic;
using UnityEngine;

public class EndingSets : MonoBehaviour
{
    public List<GameObject> endingSets;
    public List<float> endingSetsTimes;
    public float timeNow;

    public void OnEnable()
    {
        timeNow = 0.0f;
    }

    private void Update()
    {
        timeNow += Time.deltaTime;
        for (var index = 0; index < endingSetsTimes.Count-1; index++)
        {
            if (endingSetsTimes[index] <= timeNow && endingSetsTimes[index+1] >= timeNow)
            {
                endingSets[index].SetActive(true);
                if (index > 0)
                {
                    endingSets[index - 1].SetActive(false);
                }
            }
        }
    }
}
