﻿﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

//***************************************
//  Modified for data purposes by Ryuta
//***************************************
public class TimeManager : MonoBehaviour
{ 
    public bool IsFinite { get; private set; }
    public float TotalTime { get; private set; }
    public float TimeRemaining { get; private set; }
    public bool IsOver { get; private set; }

    public static float timeTaken;

    private bool raceStarted;

    public static Action<float> OnAdjustTime;
    public static Action<int, bool, GameMode> OnSetTime;

    private void Awake()
    {
        timeTaken = 0f;
        IsFinite = false;
        TimeRemaining = TotalTime;
    }


    void OnEnable()
    {
        OnAdjustTime += AdjustTime;
        OnSetTime += SetTime;
    }

    private void OnDisable()
    {
        OnAdjustTime -= AdjustTime;
        OnSetTime -= SetTime;
    }

    private void AdjustTime(float delta)
    {
        TimeRemaining += delta;
    }

    private void SetTime(int time, bool isFinite, GameMode gameMode)
    {
        TotalTime = time;
        IsFinite = isFinite;
        TimeRemaining = TotalTime;
    }

    void Update()
    {
        if (!raceStarted) return;
        
        if (IsFinite && !IsOver)
        {
            TimeRemaining -= Time.deltaTime;
            timeTaken += Time.deltaTime;
            if (TimeRemaining <= 0)
            {
                TimeRemaining = 0;
                IsOver = true;
            }
        }
    }

    public void StartRace()
    {
        raceStarted = true;
    }

    public void StopRace() {
        raceStarted = false;
    }
}

