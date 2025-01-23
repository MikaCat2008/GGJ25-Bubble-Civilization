using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using BubbleApi;



public class GameplayTimeManager : MonoBehaviour
{

    public class Month
    {
        private int _number;
        private int _numberOfDays;
        private string _name;

        public int Number { get => _number; set => _number = value; }
        public int NumberOfDays { get => _numberOfDays; set => _numberOfDays = value; }
        public string Name { get => _name; set => _name = value; }

        public Month(int Number, int NumberOfDays, string Name)
        {
            _number = Number;
            _numberOfDays = NumberOfDays;
            _name = Name;
        }
    }
    public class Calendar
    {
        private int _currentDay = 1;
        private int _currentMonth = 2;
        private int _currentYear = 1312;

        Month[] _months = { new Month(1, 31, "січень"),
                           new Month(2, 28, "лютий"),
                           new Month(3, 31, "березень"),
                           new Month(4, 30, "квітень"),
                           new Month(5, 31, "травень"),
                           new Month(6, 30, "червень"),
                           new Month(7, 31, "липень"),
                           new Month(8, 31, "серпень"),
                           new Month(9, 30, "вересень"),
                           new Month(10, 31, "жовтень"),
                           new Month(11, 30, "листопад"),
                           new Month(12, 31, "грудень")};

        public int GetCurrentYear()
        {
            return _currentYear;
        }
        public string GetCurrentMonthName()
        {
            return _months[_currentMonth - 1].Name;
        }
        public int GetCurrentMonthDays()
        {
            return _months[_currentMonth - 1].NumberOfDays;
        }

        public float GetCurrentMonthProgressPercent()
        {
            return ((float)_currentDay / (float)GetCurrentMonthDays());
        }

        public void AdvanceDay()
        {
            _currentDay++;
            if (_currentDay > GetCurrentMonthDays())
            {
                _currentDay = 1;
                AdvanceMonth();
            }
        }

        private void AdvanceMonth()
        {
            _currentMonth++;
            if (_currentMonth > 12)
            {
                _currentMonth = 1;
                _currentYear++;
            }
        }

    }


    [SerializeField] public Slider timePassedSlider;
    [SerializeField] public TMP_Text currentMonthText;
    [SerializeField] public TMP_Text currentYearhText;


    private bool isPaused = true;
    private bool isRunning = false;
    private Calendar calendar;
    private WaitForSeconds currentDayDelay;

    private WaitForSeconds DayDelaySpeedOne;
    private WaitForSeconds DayDelaySpeedTwo;
    private WaitForSeconds DayDelaySpeedThree;

    private const float SpeedOne = 0.5f; //(1 day will take 0.5 seconds)
    private const float SpeedTwo = 0.25f; //(1 day will take 0.25 seconds)
    private const float SpeedThree = 0.125f; //(1 day will take 0.125 seconds)



    public void Awake()
    {
        calendar = new Calendar();

        DayDelaySpeedOne = new WaitForSeconds(SpeedOne);
        DayDelaySpeedTwo = new WaitForSeconds(SpeedTwo);
        DayDelaySpeedThree = new WaitForSeconds(SpeedThree);
        currentDayDelay = DayDelaySpeedOne;
    }

    public void Start()
    {
        updateTimeOnUI();
    }

    public void Pause()
    {
        isPaused = true;
        isRunning = false;
     
        GlobalStorage.storage.timer.speed = 0;
    }

    public void Resume()
    {
        if (!isRunning)
        {
            isPaused = false;
            isRunning = true;
            StartCoroutine(AdvanceTime());
        }
    }

    public IEnumerator AdvanceTime()
    {
        while (!isPaused)
        {
            yield return currentDayDelay;

            if (!isPaused)
            {
                calendar.AdvanceDay();
                updateTimeOnUI();
            }
        }

        yield break;
    }

    private void updateTimeOnUI()
    {
        timePassedSlider.SetValueWithoutNotify(calendar.GetCurrentMonthProgressPercent());

        currentMonthText.text = calendar.GetCurrentMonthName();
        currentYearhText.text = calendar.GetCurrentYear().ToString();
    }

    public void SetSpeedOne()
    {
        currentDayDelay = DayDelaySpeedOne;
        Resume();
     
        GlobalStorage.storage.timer.speed = 1;
    }

    public void SetSpeedTwo()
    {
        currentDayDelay = DayDelaySpeedTwo;
        Resume();
     
        GlobalStorage.storage.timer.speed = 2;
    }

    public void SetSpeedThree()
    {
        currentDayDelay = DayDelaySpeedThree;
        Resume();
        
        GlobalStorage.storage.timer.speed = 4;
    }
}
