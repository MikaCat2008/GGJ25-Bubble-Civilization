﻿using TMPro;
using System;
using BubbleApi;
using UnityEngine;
using UnityEngine.UI;


public class TimePanel : MonoBehaviour
{
    [SerializeField] public TMP_Text pauseButtonText;
    [SerializeField] public TMP_Text speed1ButtonText;
    [SerializeField] public TMP_Text speed2ButtonText;
    [SerializeField] public TMP_Text speed3ButtonText;

    [SerializeField] public TMP_Text dayText;
    [SerializeField] public TMP_Text monthText;
    [SerializeField] public TMP_Text yearText;

    private DateTime dateTime;
    private TMP_Text currentActiveButtonText;

    private static string[] MONTHS = new string[]
    {
        "січня", "лютого", "березня",
        "квітня", "травня", "червня",
        "липня", "серпня", "вересня",
        "жовтня", "листопада", "грудня"
    };

    private void Start()
    {
        this.dateTime = new DateTime(1312, 1, 1);
        this.currentActiveButtonText = this.pauseButtonText;
    }

    private void FixedUpdate()
    {
        int speed = GlobalStorage.storage.timer.speed,
            ticks = GlobalStorage.storage.timer.ticks;

        if (speed == 0)
            return;

        if (ticks % 60 < speed)
            this.UpdateDate();
    }

    private void UpdateDate()
    {
        this.dateTime = this.dateTime.AddDays(1);

        this.dayText.text = this.dateTime.Day.ToString();
        this.monthText.text = this.GetMonthString();
        this.yearText.text = $"{this.dateTime.Year} р.";
    }

    private string GetMonthString()
    {
        return TimePanel.MONTHS[this.dateTime.Month - 1];
    }

    private void SetActiveButtonText(TMP_Text buttonText)
    {
        if (buttonText == this.currentActiveButtonText)
            return;

        buttonText.color = new Color(255, 0, 0);
        this.currentActiveButtonText.color = new Color(255, 255, 255);
        this.currentActiveButtonText = buttonText;
    }

    public void Timer_Pause()
    {
        GlobalStorage.storage.timer.speed = 0;

        this.SetActiveButtonText(this.pauseButtonText);
    }

    public void Timer_Speed1()
    {
        GlobalStorage.storage.timer.speed = 1;
    
        this.SetActiveButtonText(this.speed1ButtonText);
    }

    public void Timer_Speed2()
    {
        GlobalStorage.storage.timer.speed = 2;
    
        this.SetActiveButtonText(this.speed2ButtonText);
    }

    public void Timer_Speed3()
    {
        GlobalStorage.storage.timer.speed = 99;
    
        this.SetActiveButtonText(this.speed3ButtonText);
    }
}
