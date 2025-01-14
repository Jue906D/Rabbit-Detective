using FluffyUnderware.Curvy.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

public class MoveController : MonoBehaviour
{
    public SplineController sc;
    [SerializeField]
    public float StartMoveTime;
    public float EndMoveTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        sc = GetComponent<SplineController>();
    }
    void OnEnable()
    {
        GameManager.instance.MoveList.Add(gameObject);
    }

    void OnDisable()
    {
        GameManager.instance.MoveList.Remove(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        float time = GameManager.instance.TimeNow;
        if (time >= StartMoveTime && time <= EndMoveTime)
        {
            float rate = (time - StartMoveTime) / (EndMoveTime - StartMoveTime);
            sc.Position = rate;
        }
    }
}
