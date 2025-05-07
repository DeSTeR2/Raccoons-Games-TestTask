using Animations;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Slider")] [SerializeField] private Slider _soundSlider;

    [SerializeField] private Slider _musicSlider;

    [Header("Mute")] [SerializeField] private Toggle _soundToggle;

    [SerializeField] private Toggle _musicToggle;

    [Space] [SerializeField] private AnimationSequence _animationSequence;

    [SerializeField] private Button closeBtn;

    private void Start()
    {
        _soundSlider.value = SoundManager.GetSoundVolume();
        _musicSlider.value = SoundManager.GetMusicVolume();

        _soundSlider.onValueChanged.AddListener(SoundChange);
        _musicSlider.onValueChanged.AddListener(MusicChange);

        _soundToggle.onValueChanged.AddListener(MuteSound);
        _musicToggle.onValueChanged.AddListener(MuteMusic);

        closeBtn.onClick.AddListener(Close);

        SetMuteToggles();
    }

    public void Open()
    {
        gameObject.SetActive(true);
        _animationSequence.StartAnimation();
    }

    private void SetMuteToggles()
    {
        var soundMute = SoundManager.GetMuteSound();
        var musicMute = SoundManager.GetMuteMusic();

        _soundToggle.SetIsOnWithoutNotify(!soundMute);
        _musicToggle.SetIsOnWithoutNotify(musicMute);
    }

    private void SoundChange(float volume)
    {
        SoundManager.instance.ChangeSoundVolume(volume);
    }

    private void MusicChange(float volume)
    {
        SoundManager.instance.ChangeMusicVolume(volume);
    }

    private void MuteSound(bool value)
    {
        SoundManager.instance.ChangeMuteSound(!value);
    }

    private void MuteMusic(bool value)
    {
        SoundManager.instance.ChangeMuteMusic(value);
    }

    private void Close()
    {
        _animationSequence.StartReverceAnimation();
        _animationSequence.SetCallback(() => { gameObject.SetActive(false); });
    }
}