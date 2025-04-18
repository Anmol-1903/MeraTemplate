using UnityEngine;

public class HighlightingMesh : MonoBehaviour
{
    public static HighlightingMesh Instance { get; private set; }
    public HighlightingType highlightingType;
    public HighlightingShape highlightingShape;
    public Vector3 startSize = Vector3.one;
    public Vector3 endSize = Vector3.one * 2;
    public float speed = 1f; // interval for Blink type

    public bool Highlight { get; private set; }
    private Renderer selectedRenderer;
    private float currentTime = 0f;

    MeshRenderer[] meshRenderers;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        selectedRenderer = meshRenderers[0];
        selectedRenderer.enabled = false;
    }
    void Update()
    {
        if (!Highlight) return;


        switch (highlightingType)
        {
            case HighlightingType.Blink:
                BlinkAnimation();
                break;
            case HighlightingType.Constant:
                ConstantAnimation();
                break;
            case HighlightingType.Pulse:
                PulseAnimation();
                break;
            case HighlightingType.Breathe:
                BreatheAnimation();
                break;
        }

        switch (highlightingShape)
        {
            case HighlightingShape.Sphere:
                SetShape(0);
                break;
            case HighlightingShape.Cube:
                SetShape(1);
                break;
        }
    }

    void SetShape(int j)
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            if (i == j)
            {
                selectedRenderer = meshRenderers[i];
                selectedRenderer.enabled = true;
            }
            else
            {
                meshRenderers[i].enabled = false;
            }
        }
    }
    void BlinkAnimation()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= speed)
        {
            currentTime = 0f;
            selectedRenderer.enabled = !selectedRenderer.enabled;
            transform.localScale = endSize;
        }
    }
    void ConstantAnimation()
    {
        transform.localScale = endSize;
    }
    void PulseAnimation()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= speed)
        {
            currentTime = 0f;
        }

        float t = currentTime / speed;
        t = t * t * (3f - 2f * t); // ease in-out curve
        transform.localScale = Vector3.Lerp(startSize, endSize, t);
    }
    void BreatheAnimation()
    {
        currentTime += Time.deltaTime;
        float t = Mathf.Repeat(currentTime / speed, 1f);
        if (t < 0.5f)
        {
            t = t * 2f; // map t to 0 to 1 range
            t = t * t * (3f - 2f * t); // ease in-out curve
            transform.localScale = Vector3.Lerp(startSize, endSize, t);
        }
        else
        {
            t = (t - 0.5f) * 2f; // map t to 0 to 1 range
            t = t * t * (3f - 2f * t); // ease in-out curve
            transform.localScale = Vector3.Lerp(endSize, startSize, t);
        }
    }
    public void HighlightObject(Transform target, Vector3 startSize, Vector3 endSize, float speed, HighlightingType highlightingType, HighlightingShape highlightingShape = HighlightingShape.Sphere)
    {
        Highlight = true;
        selectedRenderer.enabled = true;
        transform.position = target.position;
        this.startSize = startSize;
        this.endSize = endSize;
        this.speed = speed;
        this.highlightingType = highlightingType;
        this.highlightingShape = highlightingShape;
    }

    public void StopHighlighting()
    {
        Highlight = false;
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].enabled = false;
        }
    }
}

public enum HighlightingType
{
    Constant,
    Blink,
    Pulse,
    Breathe
}

public enum HighlightingShape
{
    Sphere,
    Cube
}