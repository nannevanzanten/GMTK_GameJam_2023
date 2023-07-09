using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    [SerializeField] private GameObject _nightSprite;

    private float _time;
    private float _dayLength = 100f;
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
        _time += Time.time;

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
        _nightSprite.SetActive(false);
        _dayCount++;
    }

    private void StartNight()
    {
        _nightSprite.SetActive(true);
        _time = 0f;
    }

    IEnumerator FadeSprite()
    {
        var b = _nightSprite.GetComponent<SpriteRenderer>().color.a;
        b += Time.time;

        yield return new WaitForSeconds(1f);
    }
}
