using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    public event EventHandler OnStartDay;

    [SerializeField] private GameObject _nightSprite;

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
            _isDay = false;
            StartNight();

            if (_time > _dayLength + _nightLength)
            {
                _isDay = true;
                StartDay();
            }
        }
    }

    private void StartDay()
    {
        OnStartDay?.Invoke(this, EventArgs.Empty);

        _nightSprite.SetActive(false);
        _time = 0f;
        _dayCount++;
    }

    private void StartNight()
    {
        _nightSprite.SetActive(true);
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
