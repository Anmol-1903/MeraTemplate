using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
namespace BlackOut{

public class VideoPlayerInteraction : Interaction
{
    public VideoClip videoClip; // Assign this in the Inspector
    private GameObject canvasObject;
    private Image fadeImage;
    private VideoPlayer videoPlayer;
    public Vector3 EndPosition;

    public override bool isInteractionCompleted { get; set; }

    public void StartTransition()
    {
        Debug.Log("StartedVideo");
        StartCoroutine(FadeToVideo());
    }

    private IEnumerator FadeToVideo()
    {
        CreateCanvas();

        // Fade to Black
        yield return StartCoroutine(Fade(1f, 1f));

        // Play Video
        yield return StartCoroutine(PlayVideo());

        //teleport
         GameObject player = GameObject.Find("XR Origin (XR Rig)");
        //yield return new WaitForSecondsRealtime(0.5f);
        player.transform.position = EndPosition;
        // Fade to Scene
        yield return StartCoroutine(Fade(0f, 1f));

        // Cleanup
        Destroy(canvasObject);
        Destroy(videoPlayer.gameObject);

        
        isInteractionCompleted = true;
        yield return new WaitForSecondsRealtime(1f);

        // Proceed to next interaction
        StepManager.instance?.MoveToNextInteraction();
    }

    private void CreateCanvas()
    {
        // Create Canvas
        canvasObject = new GameObject("TransitionCanvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 1000; // Ensure it's on top

        // Create Fade Image
        GameObject imageObject = new GameObject("FadeImage");
        imageObject.transform.SetParent(canvas.transform, false);
        fadeImage = imageObject.AddComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 0); // Start fully transparent

        // Stretch Image to Full Screen
        RectTransform rect = fadeImage.rectTransform;
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    private IEnumerator PlayVideo()
    {
        // Create a new GameObject for the video player
        GameObject videoObject = new GameObject("VideoPlayer");
        videoPlayer = videoObject.AddComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.clip = videoClip;

        // Optionally: Set video rendering mode as RenderTexture for more flexibility
        videoPlayer.renderMode = VideoRenderMode.RenderTexture;

        // Create a render texture to display the video
        RenderTexture renderTexture = new RenderTexture(1920, 1080, 16);
        videoPlayer.targetTexture = renderTexture;

        // Create a RawImage to display the video on the canvas
        GameObject videoDisplay = new GameObject("VideoDisplay");
        RawImage rawImage = videoDisplay.AddComponent<RawImage>();
        rawImage.texture = renderTexture;
        videoDisplay.transform.SetParent(canvasObject.transform, false);

        // Stretch the RawImage to full screen
        RectTransform rectTransform = rawImage.rectTransform;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        // Play the video
        videoPlayer.Play();

        // Wait for the video to finish playing
        yield return new WaitForSeconds((float)videoClip.length);
    }

    private IEnumerator Fade(float targetAlpha, float duration)
    {
        float startAlpha = fadeImage.color.a;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, targetAlpha);
    }

    public override void StartInteraction()
    {
        StartTransition();
    }

    public override void InteractionCompleted()
    {
        // Optionally handle anything after the interaction is completed
    }
}
}