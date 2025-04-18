using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class HapticSettings
{
    public bool active;
    [Range(0f, 1f)]
    public float intensity;
    public float duration;
}

public class XRUIHapticFeedback : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public HapticSettings OnHoverEnter;
    public HapticSettings OnHoverExit;
    public HapticSettings OnSelectEnter;
    public HapticSettings OnSelectExit;

    private string audioClipPath = "AudioClips/buttonClick";
    AudioSource audioSource;
    private XRUIInputModule InputModule => EventSystem.current.currentInputModule as XRUIInputModule;

    void Start()
    {
        AudioClip buttonClickClip = Resources.Load<AudioClip>(audioClipPath);
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.clip = buttonClickClip;
        OnSelectEnter.active = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnHoverEnter.active && GetComponent<Button>().enabled)
        {
            TriggerHaptic(eventData, OnHoverEnter);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnHoverExit.active)
        {
            //TriggerHaptic(eventData, OnHoverExit);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnSelectEnter.active && GetComponent<Button>().enabled)
        {
            audioSource.Play();
            //TriggerHaptic(eventData, OnSelectEnter);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnSelectExit.active)
        {
            //TriggerHaptic(eventData, OnSelectExit);
        }
    }

    private void TriggerHaptic(PointerEventData eventData, HapticSettings hapticSettings)
    {
        UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor interactor = InputModule.GetInteractor(eventData.pointerId) as UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor;

        if (!interactor) { return; }

        interactor.SendHapticImpulse(hapticSettings.intensity, hapticSettings.duration);

    }
}
