using System.Collections;
using UnityEngine;
using UnityEngine.XR.Management;

public class VRInitializer : MonoBehaviour
{
#if UNITY_EDITOR
    private void Start()
    {
        EnableXR();
    }
    private void OnApplicationQuit()
    {
        DisableXR();
    }
    public void EnableXR()
    {
        StartCoroutine(StartXRCoroutine());
    }
    public void DisableXR()
    {
        // Debug.Log("Stopping XR...");
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        // Debug.Log("XR stopped completely.");
    }

    public IEnumerator StartXRCoroutine()
    {
        // Debug.Log("Initializing XR...");
        yield return new WaitForSeconds(.2f);
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance?.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
        }
        else
        {
            // Debug.Log("Starting XR...");
            XRGeneralSettings.Instance.Manager.StartSubsystems();
        }
    }

#endif
}