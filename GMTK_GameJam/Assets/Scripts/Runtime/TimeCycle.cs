using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    public event EventHandler OnStartDay;
    public event EventHandler OnStartNight;

    private static readonly int IsNight = Animator.StringToHash("IsNight");

    [SerializeField] private Animator _cycle;
    [SerializeField] private LumberjackBehaviour _lumber;

    private float _time;
    private float _dayLength = 10f;
    private float _nightLength = 5f;

    private int _dayCount = 0;

    private bool _isDay;

    void Start()
    {
        _isDay = true;
        StartDay();
    }

    void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _dayLength)
        {
            StartNight();

            if (_time > _dayLength + _nightLength)
            {
                StartDay();
            }
        }
    }

    private void StartDay()
    {
        OnStartDay?.Invoke(this, EventArgs.Empty);

        gameObject.GetComponent<Animator>().SetBool(IsNight, false);
        _isDay = true;

        _time = 0f;
        _dayCount++;
    }

    private void StartNight()
    {
        OnStartNight?.Invoke(this, EventArgs.Empty);

        _isDay = false;
        gameObject.GetComponent<Animator>().SetBool(IsNight, true);
    }

    public int GetDayCount()
    {
        return _dayCount;
    }

    public bool GetIsDay()
    {
        return _isDay;
    }
}
