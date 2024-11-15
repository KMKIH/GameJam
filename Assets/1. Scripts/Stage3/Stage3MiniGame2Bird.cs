using System.Collections;
using UnityEngine;

enum Direction
{
    Left,
    Right
}

public class Stage3MiniGame2Bird : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO m_collided;
    private bool _isOutside;
    private Direction _direction;
    private RectTransform _rectTransform;
    private float _dropTime;
    private float _passedTime;
    private bool _isDropped;
    private bool _isCollided = false;
    public float speed;
    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    void Start()
    {
        _isOutside = true;
        _direction = Direction.Right;
        _rectTransform = GetComponent<RectTransform>();
        _dropTime = Random.Range(5f, 15f);
        _passedTime = 0f;
        _isDropped = false;
    }


    void Update()
    {
        if (_isCollided) return;
        _passedTime += Time.deltaTime;

        _isOutside = Mathf.Abs(_rectTransform.anchoredPosition.x) - _rectTransform.sizeDelta.x / 2f >= 420f ||
            Mathf.Abs(_rectTransform.anchoredPosition.y) - _rectTransform.sizeDelta.y / 2f >= 480f;

        if (_isOutside && _rectTransform.anchoredPosition.y < 0f)
        {
            StartCoroutine(Reset());
            return;
        }

        if (_isDropped) return;
        if (_isOutside && _rectTransform.anchoredPosition.y - _rectTransform.sizeDelta.y / 2f >= 480f)
        {
            _direction = _direction == Direction.Left ? Direction.Right : Direction.Left;
            _rectTransform.anchoredPosition = new Vector2(
               (_direction == Direction.Left ? 1 : -1) * 480f,
                0f);
           
            return;
        }

        float angle = Mathf.Atan((420f + _rectTransform.sizeDelta.y) / (960f + _rectTransform.sizeDelta.x));
        float velX = Mathf.Cos(angle) * speed
            * (_direction == Direction.Right ? 1 : -1);
        float velY = Mathf.Sin(angle) * speed;


        _rectTransform.anchoredPosition = new Vector2(
            _rectTransform.anchoredPosition.x + velX * Time.deltaTime,
            _rectTransform.anchoredPosition.y + velY * Time.deltaTime);

        if (!_isOutside && !_isCollided &&
            Mathf.Abs(_rectTransform.anchoredPosition.x) <= 480f - _rectTransform.sizeDelta.x &&
            _passedTime >= _dropTime)
        {
            _isDropped = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.GetComponent<Rigidbody2D>().simulated = true;
        }
    }


    private void OnEnable()
    {
        m_collided.OnEventRaised -= OnObjCollided;
        m_collided.OnEventRaised += OnObjCollided;
    }
    private void OnDisable()
    {
        m_collided.OnEventRaised -= OnObjCollided;
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1f);
        _direction = Direction.Right;
        _passedTime = 0f;
        _isDropped = false;
        _dropTime = Random.Range(minTime, maxTime);
        _rectTransform.anchoredPosition = new Vector2(-480f, 0f);
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
    }

    public void OnObjCollided()
    {
        Debug.Log("Bird Event");
        _isDropped = false;
        _isCollided = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
    }
}
