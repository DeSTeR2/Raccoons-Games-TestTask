using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private static readonly string _saveSound = "Sound";
    private static readonly string _saveMusic = "Music";
    private static readonly string _saveMuteSound = "MuteSound";
    private static readonly string _saveMuteMusic = "MuteMusic";
    [SerializeField] private Sound[] sounds;

    [Header("Audio sourses")] [SerializeField]
    private AudioSource _sound;

    [SerializeField] private AudioSource _music;

    [Space] [SerializeField] private float _playSoundIn;

    private readonly Dictionary<SoundType, Sound> _audios = new();
    private readonly Dictionary<SoundType, float> _timer = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        foreach (var sound in sounds) _audios.Add(sound.type, sound);

        SetMute();
    }

    public static float GetSoundVolume()
    {
        return PlayerPrefs.GetFloat(_saveSound, 1f);
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(_saveMusic, 1f);
    }

    public static bool GetMuteSound()
    {
        return PlayerPrefs.GetInt(_saveMuteSound, 1) == 1;
    }

    public static bool GetMuteMusic()
    {
        return PlayerPrefs.GetInt(_saveMuteMusic, 1) == 1;
    }

    public void PlaySound(SoundType type)
    {
        if (_audios.ContainsKey(type) == false) return;

        if (_timer.ContainsKey(type))
        {
            var time = _timer[type];

            if (_audios[type].audio == null)
            {
                Debug.LogError($"Non audio. Type {type.ToString()}");
                return;
            }

            if (time + _audios[type].timeToPlayAgain <= Time.time)
            {
                _sound.PlayOneShot(_audios[type].audio);
                _timer[type] = Time.time;
            }
        }
        else
        {
            _sound.PlayOneShot(_audios[type].audio);
            _timer.Add(type, Time.time);
        }
    }

    public void ChangeSoundVolume(float volume)
    {
        _sound.volume = volume;
        PlayerPrefs.SetFloat(_saveSound, volume);
        PlayerPrefs.Save();
    }

    public void ChangeMusicVolume(float volume)
    {
        _music.volume = volume;
        PlayerPrefs.SetFloat(_saveMusic, volume);
        PlayerPrefs.Save();
    }

    public void ChangeMuteSound(bool value)
    {
        _sound.mute = value;
        PlayerPrefs.SetInt(_saveMuteSound, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ChangeMuteMusic(bool value)
    {
        _music.mute = value;
        PlayerPrefs.SetInt(_saveMuteMusic, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void SetMute()
    {
        var soundValue = GetMuteSound();
        var musicValue = GetMuteMusic();

        ChangeMuteSound(soundValue);
        ChangeMuteMusic(musicValue);
    }
}