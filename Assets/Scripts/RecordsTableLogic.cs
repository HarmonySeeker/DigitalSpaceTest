using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timingsTableLogic : MonoBehaviour
{
    [SerializeField] private GameObject index; 
    [SerializeField] private GameObject timing;

    [SerializeField] private GameObject indexParent;
    [SerializeField] private GameObject timingParent;

    [SerializeField] private List<float> timings = new List<float>();
    [SerializeField] private Timer timer;

    private TMP_Text indexText;
    private TMP_Text timingText;

    private void Awake()
    {
        indexText = index.GetComponent<TMP_Text>();
        timingText = timing.GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        TimerEventManager.RecordUpdate += TimerEventManagerOnRecordUpdate;
    }

    private void OnDisable()
    {
        TimerEventManager.RecordUpdate -= TimerEventManagerOnRecordUpdate;
    }


    private void TimerEventManagerOnRecordUpdate(float newEntry)
    {
        if (timer.IsRaceGoing())
        {
            timings.Add(newEntry);

            indexText.text = timings.Count.ToString();

            TimeSpan timeSpan = TimeSpan.FromSeconds(newEntry);
            timingText.text = timeSpan.ToString(@"mm\:ss\:ff");

            Instantiate(index, indexParent.transform);
            Instantiate(timing, timingParent.transform);
        }
    }
}
