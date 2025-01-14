using System;
using FluffyUnderware.Curvy.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("控制参数")]
    public bool isPaused;
    [Header("时间参数")]
    public float TimeAll;
    public float TimeNow;
    public float TimeSpeed;
    [Header("对象挂载")]
    public GameObject StartScreen;
    public TimeSlider Slider;
    public TextMeshProUGUI TimeNowText;
    public TextMeshProUGUI TimeAllText;
    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        TimeNowText.text = TimeNow.ToString("0.00");
        TimeAllText.text = TimeAll.ToString("0.00");
        Slider.value = TimeNow / TimeAll;
    }

    private void LateUpdate()
    {
        if (!isPaused)
        {
            TimeNow += TimeSpeed * Time.deltaTime;
        }
        
    }

    public void StartGame()
    {
        StartScreen.SetActive(false);
        isPaused = false;
    }

    public void BackToMainMenu()
    {
        StartScreen.SetActive(true);
    }

    public void OnSliderClicked()
    {
        isPaused = true;
        TimeNow = Slider.value * TimeAll;
        TimeNowText.text = TimeNow.ToString("0.00");
    }
}
