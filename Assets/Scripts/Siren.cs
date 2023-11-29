using System.Collections;
using UnityEngine;

public class Siren : MonoBehaviour
{
    [SerializeField] private ThiefDetector _thiefDetector;
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _soundChangeSpeed;

    private float _maxVolume = 1.0f;
    private float _minVolume = 0f;
    private Coroutine _routine;

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
        StartCoroutine(IncreaseVolume());
    }

    private void OnThiefGone()
    {
        StartCoroutine(DecreaseVolume());
    }

    private IEnumerator IncreaseVolume()
    {
        WaitForSeconds delay = new WaitForSeconds(0.3f);

        if (_routine != null)
            StopCoroutine(_routine);

        while (_sound.volume < _maxVolume)
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, _maxVolume, _soundChangeSpeed);

            yield return delay;
        }
    }

    private IEnumerator DecreaseVolume()
    {
        WaitForSeconds delay = new WaitForSeconds(0.3f);

        if (_routine != null)
            StopCoroutine(_routine);

        while (_sound.volume > _minVolume)
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, _minVolume, _soundChangeSpeed);

            yield return delay;
        }
    }
}
