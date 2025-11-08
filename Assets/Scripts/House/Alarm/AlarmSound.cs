using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AlarmSound : MonoBehaviour
{
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AlarmTrigger _trigger;

    private float _timeForIncrease = 5f;
    private float _timeForReduction = 5f;
    private float _minVolume = 0f;

    private Coroutine _activeCoroutine = null;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.volume = _minVolume;
    }

    private void OnEnable()
    {
        if (_trigger != null)
        {
            _trigger.onEnter += RequestFadeIn;
            _trigger.onExit += RequestFadeOut;
        }
    }

    private void OnDisable()
    {
        if (_trigger != null)
        {
            _trigger.onEnter -= RequestFadeIn;
            _trigger.onExit -= RequestFadeOut;
        }

        if (_activeCoroutine != null)
            StopCoroutine(_activeCoroutine);
    }

    private void RequestFadeIn()
    {
        StopCurrentCoroutine();
        _activeCoroutine = StartCoroutine(StartAlarm());
    }

    private void RequestFadeOut()
    {
        StopCurrentCoroutine();
        _activeCoroutine = StartCoroutine(StopAlarm());
    }

    private void StopCurrentCoroutine()
    {
        if (_activeCoroutine != null)
        {
            StopCoroutine(_activeCoroutine);
            _activeCoroutine = null;
        }
    }

    private IEnumerator StartAlarm()
    {
        float startVolume = _audioSource.volume;
        float elapsedTime = 0f;

        if (!_audioSource.isPlaying)
        {
            _audioSource.loop = true;
            _audioSource.Play();
        }

        while (elapsedTime < _timeForIncrease)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _timeForIncrease);
            _audioSource.volume = Mathf.Lerp(startVolume, _maxVolume, t);
            yield return null;
        }

        _audioSource.volume = _maxVolume;
    }

    private IEnumerator StopAlarm()
    {
        float startVolume = _audioSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < _timeForReduction)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / _timeForReduction);
            _audioSource.volume = Mathf.Lerp(startVolume, _minVolume, t);
            yield return null;
        }

        _audioSource.volume = _minVolume;
        _audioSource.Stop();
    }
}
