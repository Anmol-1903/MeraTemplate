using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class Scroller : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform content;
    public Button prevButton, nextButton;
    public float scrollSpeed = 0.1f; 
    public float snapSpeed = 10f; 
    public Vector2 scrollDistance = Vector2.zero;

    public Vector2 minClamp, maxClamp;

    public bool horizontal, vertical;

    private Vector2 initialPosition;
    private Vector2 targetPosition; 
    private Vector2 contentSize; 

    async void Awake() {
        if(prevButton != null){
            prevButton.onClick.AddListener(OnPrevButtonClick);
        }
        if(nextButton != null){
            nextButton.onClick.AddListener(OnNextButtonClick);
        }

        await Task.Delay(1000);

        contentSize = new Vector2(content.rect.width, content.rect.height);
    }
    private void Start()
    {
        initialPosition = content.anchoredPosition;
        targetPosition = content.anchoredPosition;
    }

    private void Update()
    {
        if(!horizontal){
            targetPosition.x = initialPosition.x;
        }
        if(!vertical){
            targetPosition.y = initialPosition.y;
        }

        targetPosition = ClampPosition(targetPosition);

        content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPosition, Time.deltaTime * snapSpeed);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        targetPosition = content.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        targetPosition += eventData.delta * scrollSpeed;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        targetPosition = SnapToNearest();
    }
    private Vector2 ClampPosition(Vector2 position)
    {
        position.x = Mathf.Clamp(position.x, minClamp.x, maxClamp.x);
        position.y = Mathf.Clamp(position.y, minClamp.y, maxClamp.y);
        return position;
    }

    private float RoundToNearest(float value, float k)

    {
        return Mathf.Round(value / k) * k;
    }
    private Vector2 SnapToNearest()
    {
        if(scrollDistance.x == 0 && scrollDistance.y == 0){
            return targetPosition;
        }
        else
        {
            Vector2 snapPosition = targetPosition;
            if(horizontal && scrollDistance.x != 0){
                snapPosition.x = RoundToNearest(targetPosition.x, scrollDistance.x);
            }
            if(vertical && scrollDistance.y != 0){
                snapPosition.y = RoundToNearest(targetPosition.y, scrollDistance.y);
            }
            return snapPosition;
        }
    }

    private void OnPrevButtonClick(){
        targetPosition += new Vector2(scrollDistance.x, -scrollDistance.y);
    }
    private void OnNextButtonClick(){
        targetPosition += new Vector2(-scrollDistance.x, scrollDistance.y);
    }
}
