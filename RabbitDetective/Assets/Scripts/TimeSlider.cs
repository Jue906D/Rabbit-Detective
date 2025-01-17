using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
 
[RequireComponent(typeof(Slider))]
public class TimeSlider : Slider, IDragHandler 
{
    public override void OnDrag(PointerEventData eventData)
    {
        
        base.OnDrag(eventData);

        GameManager.instance.OnSliderClicked();
        
        //Debug.Log("Slider is being dragged.");
    }

    public void OnMouseUpAsButton()
    {
        GameManager.instance.OnSliderClicked();
        GameManager.instance.isPaused = false;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        GameManager.instance.OnSliderClicked();
        //Debug.Log("Pressed");
    }
    
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        GameManager.instance.isPaused = false;
        //Debug.Log("Pressed");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.OnSliderClicked();
        //Debug.Log("Slider is being dragged.");
    }
}