using System.Collections.Generic;
using InnovateLabs;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class WrenchTool : Tool<object>
{
    public XRKnob knob;
    XRGrabInteractable grabInteractable;
    public WrenchState state;

    public Transform visual;

    public List<XRSocketInteractor> socketsInteracting = new();

    void Awake()
    {
        knob = GetComponentInChildren<XRKnob>(true);
        grabInteractable = GetComponent<XRGrabInteractable>();
        visual = transform.GetChild(0);


        //knob.onValueChange.AddListener(RotateKnob);
    }
    // private void RotateKnob(float value)
    // {
    //     knob.value = value;
    // }
    void OnEnable()
    {
        toolType = ToolType.Wrench;
    }

    private Dictionary<XRSocketInteractor, SocketTool> cachedSockets = new Dictionary<XRSocketInteractor, SocketTool>();
    public SocketTool SocketTool;
    void Update()
    {
        socketsInteracting.Clear();
        foreach (var interactor in grabInteractable.interactorsSelecting)
        {
            XRSocketInteractor socketInteractor = interactor as XRSocketInteractor;
            if (socketInteractor != null)
            {
                socketsInteracting.Add(socketInteractor);
            }
        }

        if (socketsInteracting.Count > 0)
        {
            XRSocketInteractor firstSocket = socketsInteracting[0];
            if (!cachedSockets.TryGetValue(firstSocket, out SocketTool socket))
            {
                socket = firstSocket.GetComponent<SocketTool>();
                cachedSockets[firstSocket] = socket;
                SocketTool = socket;
            }

            if (socket != null)
            {
                SetWrenchState(socket.Static);
            }
        }
        else
        {
            SetWrenchState(true);
        }
    }

    private void SetWrenchState(bool isStatic)
    {
        if (isStatic)
        {
            state = WrenchState.Detached;
            baseInteractable = grabInteractable;
            knob.enabled = false;
            knob.gameObject.SetActive(false);
            visual.SetParent(transform);
            // knob.onValueChange.RemoveAllListeners();
        }
        else
        {
            state = WrenchState.Attached;
            baseInteractable = knob;
            knob.enabled = true;
            knob.gameObject.SetActive(true);
            visual.SetParent(knob.transform.GetChild(0).transform);
        }
    }
}

public enum WrenchState
{
    Detached,
    Attached
}
