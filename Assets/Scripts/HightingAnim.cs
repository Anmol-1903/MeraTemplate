using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HightingAnim : MonoBehaviour
{
    public static bool isAnimating = true;
    public static HightingAnim instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this);
        }
    }

    public void HighlightedAnim(GameObject obj, Quaternion startQuaternion, Quaternion endQuaternion, Vector3 startPos, Vector3 endPos, float duration)
    {
        isAnimating = true;
        Animate = StartCoroutine(AnimateObject(obj, startQuaternion, endQuaternion, startPos, endPos, duration));
    }

    public void HighlightedAnim(GameObject obj, Quaternion startQuaternion, Quaternion endQuaternion, float duration)
    {
        isAnimating = true;
        Animate = StartCoroutine(AnimateObject(obj, startQuaternion, endQuaternion, duration));
    }
    public Coroutine Animate;
    public void HighlightedAnim(GameObject obj, Vector3 startPos, Vector3 endPos, float duration)
    {
        isAnimating = true;
        Animate = StartCoroutine(AnimateObject(obj, startPos, endPos, duration));
    }
    public void StopAnimating(){
        StopCoroutine(Animate);
    }
    private IEnumerator AnimateObject(GameObject obj, Quaternion startQuaternion, Quaternion endQuaternion, Vector3 startPos, Vector3 endPos, float duration)
    {
        while (isAnimating)
        {
            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                t = EaseInOut(t); // Apply easing function
                obj.transform.localPosition = Vector3.Lerp(startPos, endPos, t);
                
                Vector3 startEuler = startQuaternion.eulerAngles;
                Vector3 endEuler = endQuaternion.eulerAngles;
        
                Vector3 euler = Vector3.Lerp(startEuler, endEuler, t);
                obj.transform.localRotation = Quaternion.Euler(euler);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            obj.transform.localPosition = endPos;
            obj.transform.localRotation = endQuaternion;
        }
    }

    private IEnumerator AnimateObject(GameObject obj, Vector3 startPos, Vector3 endPos, float duration)
    {
        while (isAnimating)
        {
            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                obj.transform.localPosition = Vector3.Lerp(startPos, endPos, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            obj.transform.position = endPos;
        }
    }

    private IEnumerator AnimateObject(GameObject obj, Quaternion startQuaternion, Quaternion endQuaternion, float duration)
    {
        while (isAnimating)
        {
            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                t = EaseInOut(t); // Apply easing function
                obj.transform.localRotation = Quaternion.Slerp(startQuaternion, endQuaternion, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            obj.transform.localRotation = endQuaternion;
        }
    }

    public void StopAnimation()
    {
        isAnimating = false;
    }

    // Easing function for Ease In Out
    private float EaseInOut(float t)
    {
        return t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;
    }
}
