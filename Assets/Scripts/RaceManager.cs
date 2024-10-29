using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager instance;

    [SerializeField] private List<float> records;
    [SerializeField] private Timer timer;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        instance.records = new List<float>();
    }

    private void OnEnable()
    {
        TimerEventManager.TimerStop += instance.TimerEventManagerOnTimerStop;
    }

    private void Start()
    {
    }

    private void TimerEventManagerOnTimerStop()
    {
        instance.records.Add(timer.GetLastRecord());
    }
}
