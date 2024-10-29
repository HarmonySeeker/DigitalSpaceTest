using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    private TMP_Text _timerText;

    [SerializeField] private float currentTime;

    private bool _isRunning;

    private void Awake()
    {
        _timerText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (!_isRunning) return;

        currentTime += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        _timerText.text = timeSpan.ToString(@"mm\:ss\:ff");
    }

    private void OnEnable()
    {
        TimerEventManager.TimerStart += TimerEventManagerOnTimerStart;
        TimerEventManager.TimerStop += TimerEventManagerOnTimerStop;
        TimerEventManager.TimerUpdate += TimerEventManagerOnTimerUpdate;
    }

    private void OnDisable()
    {
        TimerEventManager.TimerStart -= TimerEventManagerOnTimerStart;
        TimerEventManager.TimerStop -= TimerEventManagerOnTimerStop;
        TimerEventManager.TimerUpdate -= TimerEventManagerOnTimerUpdate;
    }

    private void TimerEventManagerOnTimerUpdate(float value) => currentTime = value;

    private void TimerEventManagerOnTimerStop()
    {
        _isRunning = false;
        _timerText.text = "";
        currentTime = 0;
    }

    private void TimerEventManagerOnTimerStart() => _isRunning = true;

    public float GetCurrentTime()
    {
        return currentTime;
    }
}
