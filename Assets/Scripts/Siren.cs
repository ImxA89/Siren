using System.Collections;
using UnityEngine;

public class Siren : MonoBehaviour
{
    [SerializeField] private ThiefDetector _thiefDetector;
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _soundChangeSpeed;

    private float _maxVolume = 1.0f;
    private float _minVolume = 0f;
    private float _delayTime = 0.3f;
    private Coroutine _routineChangeVolume;

    private void OnValidate()
    {
        if (_soundChangeSpeed < 0)
            _soundChangeSpeed = -_soundChangeSpeed;
    }

    private void OnEnable()
    {
        _thiefDetector.Detected += OnThiefDetected;
        _thiefDetector.ThiefGone += OnThiefGone;
    }

    private void Start()
    {
        _sound.volume = _minVolume;
        _sound.Play();
    }

    private void OnDisable()
    {
        _thiefDetector.Detected -= OnThiefDetected;
        _thiefDetector.ThiefGone -= OnThiefGone;
    }

    private void OnThiefDetected()
    {
        IncreaseVolume();
    }

    private void OnThiefGone()
    {
        DecreaseVolume();
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        WaitForSeconds delay = new WaitForSeconds(_delayTime);

        if (_routineChangeVolume != null)
            StopCoroutine(_routineChangeVolume);

        do
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, targetVolume, _soundChangeSpeed);

            yield return delay;
        }
        while (_sound.volume < _maxVolume && _sound.volume > _minVolume);
    }

    private void IncreaseVolume()
    {

        _routineChangeVolume = StartCoroutine(ChangeVolume(_maxVolume));
    }

    private void DecreaseVolume()
    {
        _routineChangeVolume = StartCoroutine(ChangeVolume(_minVolume));
    }
}
