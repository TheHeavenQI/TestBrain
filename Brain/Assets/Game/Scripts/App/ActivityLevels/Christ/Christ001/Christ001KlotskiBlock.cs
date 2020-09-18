using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Christ001KlotskiBlock :
    MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    public Transform gridTrans;
    public bool isLevelCompletion = false;

    private float _moveSpeed = 50;

    private Vector3 _lastMousePos;
    private Vector3 _currentPos;
    private Rigidbody2D _rigidbody;
    private Vector3 _originPos;
    private List<Vector2> _gridList = new List<Vector2>();

    private void Start()
    {
        _originPos = transform.position;
        _currentPos = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();

        foreach (Transform grid in gridTrans)
        {
            _gridList.Add(grid.position);
        }
    }

    public void Return2OriginPos()
    {
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _rigidbody.velocity = Vector2.zero;
        transform.position = _originPos;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        if (isLevelCompletion)
        {
            return;
        }
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = 10f;
        _lastMousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    public void OnDrag(PointerEventData data)
    {
        if (isLevelCompletion)
        {
            return;
        }
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = 0;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        Vector3 velocity = (mouseWorldPos - _lastMousePos) * _moveSpeed;
        velocity.x = Mathf.Clamp(velocity.x, -10, 10);
        velocity.y = Mathf.Clamp(velocity.y, -10, 10);
        _rigidbody.velocity = velocity;
        _lastMousePos = mouseWorldPos;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -228, 228);
        pos.y = Mathf.Clamp(pos.y, -170, 170);
        transform.position = pos;
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (isLevelCompletion)
        {
            return;
        }

        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _rigidbody.velocity = Vector2.zero;
        Vector3 pos = transform.position;
        float minDis = float.PositiveInfinity;
        Vector2 latelyPos = pos;
        foreach (Vector2 gridPos in _gridList)
        {
            float distance = Vector2.Distance(pos, gridPos);
            if (distance < minDis)
            {
                minDis = distance;
                latelyPos = gridPos;
            }
        }

        transform.position = latelyPos;
        if (Vector3.Distance(latelyPos, _currentPos) > 0.1f)
        {
            // AddStep();
            //Debug.LogError($"Move to mPPos {_currentPos}, latelyPos {latelyPos}, realPos {transform.position}");
        }
        _currentPos = latelyPos;
    }

    public void OnLevelCompletion()
    {
        isLevelCompletion = true;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _rigidbody.velocity = Vector2.zero;
    }
}
