using System.Collections;
using UnityEngine;

public class RandomlySounds : MonoBehaviour
{
    [Header("SFX Settings")]
    [SerializeField] private AudioClip[] _terrorSounds;
    [SerializeField] private float _minRandomDelay = 10f;
    [SerializeField] private float _maxRandomDelay = 30f;
    [SerializeField] private float _minVolume = 0.4f;
    [SerializeField] private float _maxVolume = 0.8f;

    private Coroutine _randomSoundCoroutine;

    void Start()
    {
        if (_terrorSounds != null && _terrorSounds.Length > 0)
        {
            _randomSoundCoroutine = StartCoroutine(PlayRandomSoundsCoroutine());
        }
    }

    public void PlayRandomTerrorSound()
    {
        if (_terrorSounds == null || _terrorSounds.Length == 0)
            return;

        AudioClip randomClip = _terrorSounds[Random.Range(0, _terrorSounds.Length)];
        float randomVolume = Random.Range(_minVolume, _maxVolume);
        Debug.Log($"🔊 Reproduciendo sonido: {randomClip.name} con volumen: {randomVolume}");
        AudioManager.Instance.PlaySFX(randomClip, randomVolume);
    }

    private IEnumerator PlayRandomSoundsCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minRandomDelay, _maxRandomDelay));
            PlayRandomTerrorSound();
        }
    }
}
