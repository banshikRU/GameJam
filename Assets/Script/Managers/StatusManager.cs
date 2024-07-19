using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StatusManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    public float countdownTime ; 
    private float currentTime;
    public static StatusManager Instance;
    void Start()
    {
        Instance = this;
        currentTime = countdownTime;
    }
    public void SetStatus(string statusText)
    {
        _textMeshPro.text = statusText;
    }
    public void SetStatus(string statusText,float timeToEnd)
    {
        _textMeshPro.text = statusText;
        StartTimer(3f);
    }
    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            SetStatus(".....");
        }
    }
    public void StartTimer(float countdownTime)
    {
        currentTime = countdownTime;
        InvokeRepeating("UpdateTimer", 0f, 1f); 
    }
    private void UpdateTimer()
    {
        if (currentTime > 0)
        {
            currentTime -= 1f;
        }
        else
        {
            StopTimer();
        }
    }
    public void StopTimer()
    {
        CancelInvoke("UpdateTimer");
    }
}


