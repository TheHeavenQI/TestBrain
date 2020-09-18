using UnityEngine;
using UnityEngine.UI;

public class Level298 : LevelBasePage
{
    public Level296Hole hole;
    public Text numText;
    private readonly TouchZoom _touchZoom = new TouchZoom();
    private readonly float _maxScale = 2;
    private readonly float _needScale = 1.8f;
    private readonly float _needOutCount = 4;
    private int _outCount = 0;

    protected override void Start()
    {
        base.Start();
        hole.onClick += (h) => {
            if (hole.isGoOuting)
            {
                return;
            }
            switch (_outCount)
            {
                case 0:
                    hole.ShowGameObject(Level296Hole.GOType.esterEgg, -1, OnGoOutHole);
                    break;
                case 1:
                    hole.ShowGameObject(Level296Hole.GOType.rabbit, -1, OnGoOutHole);
                    break;
                case 2:
                    hole.ShowGameObject(Level296Hole.GOType.snake, -1, OnGoOutHole);
                    break;
                case 3:
                    if (hole.transform.localScale.x >= _needScale)
                    {

                        hole.ShowGameObject(Level296Hole.GOType.bomb, -1, (go) => Completion());
                        ++_outCount;
                        numText.text = (_needOutCount - _outCount).ToString();
                    }
                    break;
                default:
                    break;
            }
        };
        numText.text = _needOutCount.ToString();
    }

    private void OnGoOutHole(GameObject go)
    {
        Vector3 pos = go.transform.position;
        go.transform.SetParent(this.transform);
        go.transform.SetAsLastSibling();
        go.transform.position = pos;
        ++_outCount;
        numText.text = (_needOutCount - _outCount).ToString();
    }

    public override void Refresh()
    {
        base.Refresh();

        hole.Clear();
        hole.transform.localScale = Vector3.one;
        _outCount = 0;
        numText.text = _needOutCount.ToString();
    }

    private void Update()
    {
        if (isLevelComplete || hole.isGoOuting)
        {
            return;
        }

        switch (_touchZoom.Update())
        {
            case TouchZoom.ZoomType.Large:
                float scale1 = hole.transform.localScale.x;
                scale1 *= 1.1f;
                scale1 = Mathf.Clamp(scale1, 1, _maxScale);
                hole.transform.localScale = new Vector3(scale1, scale1, scale1);
                break;
            case TouchZoom.ZoomType.Small:
                float scale2 = hole.transform.localScale.x;
                scale2 *= 0.9f;
                scale2 = Mathf.Clamp(scale2, 1, _maxScale);
                hole.transform.localScale = new Vector3(scale2, scale2, scale2);
                break;
            default:
                break;
        }
    }
}
