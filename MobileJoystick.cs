using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;
    [SerializeField, Range(0.1f, 1f)] private float handleRange = 0.65f;

    public Vector2 Direction { get; private set; }

    public void OnPointerDown(PointerEventData eventData) => OnDrag(eventData);

    public void OnDrag(PointerEventData eventData)
    {
        if (background == null || handle == null) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);

        Vector2 halfSize = background.rect.size * 0.5f;
        Vector2 normalized = new Vector2(
            halfSize.x > 0 ? localPoint.x / halfSize.x : 0,
            halfSize.y > 0 ? localPoint.y / halfSize.y : 0);

        Direction = Vector2.ClampMagnitude(normalized, 1f);
        handle.anchoredPosition = Direction * (halfSize * handleRange);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Direction = Vector2.zero;
        if (handle != null) handle.anchoredPosition = Vector2.zero;
    }
}
