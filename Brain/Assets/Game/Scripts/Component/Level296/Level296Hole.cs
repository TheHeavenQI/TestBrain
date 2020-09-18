using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class Level296Hole : MonoBehaviour
{
    public GameObject rabbitPrefab;
    public GameObject snakePrefab;
    public GameObject esterEggPrefab;
    public GameObject bombPrefab;
    public GameObject manPrefab;

    public int showCount => _showGameObj.Count;
    public event Action<Level296Hole> onClick;
    public bool isGoOuting { get; private set; }

    public enum GOType
    {
        rabbit,
        snake,
        esterEgg,
        bomb,
        man
    }

    private RectTransform _showPoint;
    private RectTransform _hidePoint;
    private List<GameObject> _showGameObj = new List<GameObject>();
    private readonly float _duration = 0.4f;

    private void Awake()
    {
        _showPoint = transform.Find("ShowPoint") as RectTransform;
        _hidePoint = transform.Find("HidePoint") as RectTransform;

        this.GetComponent<Button>().onClick.AddListener(() => onClick?.Invoke(this));
    }

    public void HideGameObject(GameObject go, float duration = -1, Action<GameObject> onCompletion = null)
    {
        if (isGoOuting || go == null || !_showGameObj.Contains(go))
        {
            return;
        }
        isGoOuting = true;
        duration = duration < 0 ? _duration : duration;
        go.transform.DOLocalMoveY(_hidePoint.localPosition.y, duration).OnComplete(() => {
            isGoOuting = false;
            onCompletion?.Invoke(go);
            _showGameObj.Remove(go);
            UnityEngine.Object.Destroy(go);
        });
    }

    public void ShowGameObject(GOType type, float duration = -1, Action<GameObject> onCompletion = null)
    {
        if (isGoOuting)
        {
            return;
        }
        GameObject prefab = null;
        switch (type)
        {
            case GOType.rabbit:
                prefab = rabbitPrefab;
                break;
            case GOType.snake:
                prefab = snakePrefab;
                break;
            case GOType.esterEgg:
                prefab = esterEggPrefab;
                break;
            case GOType.bomb:
                prefab = bombPrefab;
                break;
            case GOType.man:
                prefab = manPrefab;
                break;
            default: break;
        }

        if (prefab == null)
        {
            return;
        }
        duration = duration < 0 ? _duration : duration;

        isGoOuting = true;
        GameObject go = Instantiate<GameObject>(prefab);
        go.transform.SetParent(this.transform, false);
        go.transform.SetAsFirstSibling();
        float scale = 1 / this.transform.localScale.x;
        go.transform.localScale = new Vector3(scale, scale, scale);
        go.SetActive(true);
        go.transform.localPosition = _hidePoint.localPosition;
        go.transform.DOLocalMoveY(_showPoint.localPosition.y, duration).OnComplete(() => {
            isGoOuting = false;
            onCompletion?.Invoke(go);
        });
        _showGameObj.Add(go);
    }

    public void Clear()
    {
        foreach (GameObject go in _showGameObj)
        {
            UnityEngine.Object.Destroy(go);
        }
        _showGameObj.Clear();
        isGoOuting = false;
    }
}
