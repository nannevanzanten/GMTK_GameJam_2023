using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    [SerializeField] private AnimationClip _nightSprite;
    private Animation _anim;

    private float _time;
    private float _dayLength = 100f;
    private float _nightLength = 5f;

    private int _dayCount = 0;

    private bool _isDay;

    void Start()
    {
        _anim.clip = _nightSprite;
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
        _dayCount++;

        _time = 0f;
    }

    private void StartNight()
    {
        _anim.Play();
    }
}
