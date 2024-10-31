using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;
using Unity.VisualScripting;

public class Timer : MonoBehaviour
{
    private TMP_Text _timerText;

    [SerializeField] private float currentTime;

    private bool _isRunning;

    private void Awake()
    {
        _timerText = GetComponent<TMP_Text>();
        _timerText.text = "";
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
        TimerEventManager.OnRecordUpdate(currentTime);
        currentTime = 0;
    }

    private void TimerEventManagerOnTimerStart() => _isRunning = true;
}
