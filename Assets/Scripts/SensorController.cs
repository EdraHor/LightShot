using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SensorController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private Image _controlImage;
    [SerializeField] private Image _handleImage;
    [SerializeField] private float _radiusOffet = 325;
    public bool isInvertX = true;
    public bool isDebug = true;

    public Vector3 Value { get { return GetPositionOnControl(); } }
    public bool OnDown { get { return isDown; } }
    public bool isDown = false;
    private float _radius { get {return _radiusOffet + _controlImage.rectTransform.rect.width / 2; } }
    
    private Vector2 _center;

    private Vector2 inputPosition
    {
        get
        {
            Vector2 input = Vector2.zero;
            if (Input.touchCount == 0)
            {
                input = Input.mousePosition;
            }
            else
            {
                foreach (var touch in Input.touches) //TODO: ��������� ������� ����� �� fingerId
                {
                    if (Vector2.Distance(touch.rawPosition, _center) < _radius)
                    {
                        input = touch.position;
                    }
                }
            }
            return input;
        }
    }
    private bool isInputInRadius { 
        get 
        {
            if (Vector2.Distance(inputPosition, _center) < _radius)
            {
                return true;
            }
            return false;
        } 
    }


    void Start()
    {
        Input.multiTouchEnabled = true;
        if (!_controlImage || !_handleImage)
        {
            _controlImage = GetComponent<Image>();
            if (!_controlImage) Debug.LogError("You not set image component!");
        }

        _center = new Vector2(_controlImage.transform.position.x + _controlImage.rectTransform.rect.center.x,
                         _controlImage.transform.position.y + _controlImage.rectTransform.rect.center.y);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(_handleImage.transform.position, _radius);
    }

    public Vector2 GetPositionOnControl()
    {
        if (!isDown)
            return Vector2.zero;

        Vector2 position;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(_controlImage.rectTransform, inputPosition,
        //    _controlImage.canvas.worldCamera, out position);
        //position = new Vector2(Mathf.Abs(position.x), Mathf.Abs(position.y));

        //var normPos = new Vector2(Mathf.Clamp(((position.x / _controlImage.rectTransform.rect.width) - 0.5f) * 2, -1, 1),
        //                          Mathf.Clamp(((position.y / _controlImage.rectTransform.rect.height) - 0.5f) * 2, -1, 1));

        position = _handleImage.rectTransform.anchoredPosition / _radius;

        if (isInvertX)
            position.x *= -1;
        if (isDebug)
            Debug.Log($"{position}");

        return position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isInputInRadius)
        {
            _handleImage.transform.position = inputPosition;
            isDown = true;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDown)
        {
            _handleImage.rectTransform.anchoredPosition = Vector2.zero;
            isDown = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDown && isInputInRadius) //������ ����
        {
            _handleImage.transform.position = inputPosition;
        }
        else if(!isInputInRadius)
        {
            var heading = inputPosition - _center;
            var direction = heading / heading.magnitude; 
            _handleImage.transform.position = _center + direction * _radius;
        }
    }
}
