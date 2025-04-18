using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MyAudioManager : MonoBehaviour
{
    public static MyAudioManager instance;

    [Header("Audio Sources")]
    public AudioSource SfxSource, GuidanceSource, EnvironmentSource;

    [Header("Volume Sliders")]
    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider guidanceSlider;
    public Slider environmentSlider;
    private int masterMute, sfxMute, guidanceMute, environmentMute;

    [Header("Mute Buttons")]
    public Button masterButton;
    public Button sfxButton;
    public Button guidanceButton;
    public Button environmentButton;

    [Header("Mute Sprites")]
    public Sprite muted;
    public Sprite unmuted;

    [Header("Audio Mixer")]
    public AudioMixer masterMixer;

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

        if (masterSlider != null) masterSlider.onValueChanged.AddListener(delegate { SetMasterVolume(masterSlider.value); });
        if (sfxSlider != null) sfxSlider.onValueChanged.AddListener(delegate { SetSfxVolume(sfxSlider.value); });
        if (guidanceSlider != null) guidanceSlider.onValueChanged.AddListener(delegate { SetGuidanceVolume(guidanceSlider.value); });
        if (environmentSlider != null) environmentSlider.onValueChanged.AddListener(delegate { SetEnvironmentVolume(environmentSlider.value); });

        if (masterButton != null) masterButton.onClick.AddListener(MasterButtonClicked);
        if (sfxButton != null) sfxButton.onClick.AddListener(SfxButtonClicked);
        if (guidanceButton != null) guidanceButton.onClick.AddListener(GuidanceButtonClicked);
        if (environmentButton != null) environmentButton.onClick.AddListener(EnvironmentButtonClicked);
    }
    void OnEnable()
    {
        if (masterSlider != null)
        {
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", .7f);
        }
        if (sfxSlider != null)
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", .7f);
        }
        if (guidanceSlider != null)
        {
            guidanceSlider.value = PlayerPrefs.GetFloat("GuidanceVolume", .7f);
        }
        if (environmentSlider != null)
        {
            environmentSlider.value = PlayerPrefs.GetFloat("EnvironmentVolume", .7f);
        }

        // Load mute states from PlayerPrefs
        masterMute = PlayerPrefs.GetInt("MasterMute", 1);
        sfxMute = PlayerPrefs.GetInt("SfxMute", 1);
        guidanceMute = PlayerPrefs.GetInt("GuidanceMute", 1);
        environmentMute = PlayerPrefs.GetInt("EnvironmentMute", 1);

        // Apply saved settings directly to the AudioMixer
        SetMasterVolume(masterMute == 1 ? masterSlider?.value ?? 1 : 0);
        SetSfxVolume(sfxMute == 1 ? sfxSlider?.value ?? 1 : 0);
        SetGuidanceVolume(guidanceMute == 1 ? guidanceSlider?.value ?? 1 : 0);
        SetEnvironmentVolume(environmentMute == 1 ? environmentSlider?.value ?? 1 : 0);
    }
    public void SetMasterVolume(float vol)
    {
        if (masterMixer != null)
        {
            masterMixer.SetFloat("Master", vol == 0 ? -80 : Mathf.Log10(vol) * 20);
            PlayerPrefs.SetFloat("MasterVolume", vol);
        }
    }

    public void SetSfxVolume(float vol)
    {
        if (masterMixer != null)
        {
            masterMixer.SetFloat("sfx", vol == 0 ? -80 : Mathf.Log10(vol) * 20);
            PlayerPrefs.SetFloat("SfxVolume", vol);
        }
    }

    public void SetGuidanceVolume(float vol)
    {
        if (masterMixer != null)
        {
            masterMixer.SetFloat("dia", vol == 0 ? -80 : Mathf.Log10(vol) * 20);
            PlayerPrefs.SetFloat("GuidanceVolume", vol);
        }
    }

    public void SetEnvironmentVolume(float vol)
    {
        if (masterMixer != null)
        {
            masterMixer.SetFloat("env", vol == 0 ? -80 : Mathf.Log10(vol) * 20);
            PlayerPrefs.SetFloat("EnvironmentVolume", vol);
        }
    }

    private void MasterButtonClicked()
    {
        masterMute = 1 - masterMute; // Toggle mute state
        if (masterButton != null) masterButton.image.sprite = masterMute == 1 ? unmuted : muted;
        SetMasterVolume(masterSlider?.value * masterMute ?? 0);
        PlayerPrefs.SetInt("MasterMute", masterMute);
    }

    private void SfxButtonClicked()
    {
        sfxMute = 1 - sfxMute; // Toggle mute state
        if (sfxButton != null) sfxButton.image.sprite = sfxMute == 1 ? unmuted : muted;
        SetSfxVolume(sfxSlider?.value * sfxMute ?? 0);
        PlayerPrefs.SetInt("SfxMute", sfxMute);
    }

    private void GuidanceButtonClicked()
    {
        guidanceMute = 1 - guidanceMute; // Toggle mute state
        if (guidanceButton != null) guidanceButton.image.sprite = guidanceMute == 1 ? unmuted : muted;
        SetGuidanceVolume(guidanceSlider?.value * guidanceMute ?? 0);
        PlayerPrefs.SetInt("GuidanceMute", guidanceMute);
    }

    private void EnvironmentButtonClicked()
    {
        environmentMute = 1 - environmentMute; // Toggle mute state
        if (environmentButton != null) environmentButton.image.sprite = environmentMute == 1 ? unmuted : muted;
        SetEnvironmentVolume(environmentSlider?.value * environmentMute ?? 0);
        PlayerPrefs.SetInt("EnvironmentMute", environmentMute);
    }
    public void PlaySfxOneShot(AudioClip clip)
    {
        SfxSource.PlayOneShot(clip);
    }
    public void PlaySfx(AudioClip clip)
    {
        SfxSource.clip = clip;
        SfxSource.Play();
    }

    public void PlayGuidance(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.Log($"<color=red>MyAudioManager </color>- Audio clip is null");
            return;
        }
        GuidanceSource.clip = clip;
        GuidanceSource.Play();
    }

    public void StopGuidance()
    {
        GuidanceSource.clip = null;
        GuidanceSource.Stop();
    }
    public void PlayEnvironment(string clipName)
    {

        AudioClip clip = Resources.Load<AudioClip>($"Audio/AudioClips/GeneratedAudio/Environment/{clipName}");
        EnvironmentSource.loop = true;
        EnvironmentSource.clip = clip;
        EnvironmentSource.Play();
    }

    public void PlayEnvironment(AudioClip clipName)
    {
        EnvironmentSource.loop = true;
        EnvironmentSource.clip = clipName;
        EnvironmentSource.Play();
    }

    public void StopEnvironment()
    {
        EnvironmentSource.Stop();
    }
}