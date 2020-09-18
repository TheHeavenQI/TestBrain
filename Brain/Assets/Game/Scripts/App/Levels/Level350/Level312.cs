using UnityEngine;
using UnityEngine.UI;
using BaseFramework;


public class Level312 : LevelBasePage
{

    public Button door2;
    public Text doorText;

    public DragMoveEventTrigger twig1;
    public RectTransform twig2;

    public DragMoveEventTrigger[] dragmoves;

    private readonly string[] _textKey = new string[] { "level_312_doyou", "level_312_ismy", "level_312_kid" };
    private int _step = 0;

    protected override void Start()
    {
        base.Start();
        doorText.text = Localization.GetText(_textKey[0]);
        door2.gameObject.SetActive(false);

        twig1.onEndDrag = (d) => {
            if (!twig1.rectTransform.IsRectTransformOverlap(twig2))
            {
                twig1.enableDragMove = false;
                ++_step;
                door2.gameObject.SetActive(true);
            }
        };

        door2.onClick.AddListener(() => {
            switch (_step)
            {
                case 1:
                    ++_step;
                    doorText.text = Localization.GetText(_textKey[1]);
                    break;
                case 2:
                    ++_step;
                    doorText.text = Localization.GetText(_textKey[2]);
                    break;
                case 3:
                    ++_step;
                    Completion();
                    break;
                default:
                    break;
            }
        });
    }


    public override void Refresh()
    {
        base.Refresh();
        _step = 0;
        door2.gameObject.SetActive(false);
        doorText.text = Localization.GetText(_textKey[0]);

        foreach (var dm in dragmoves)
        {
            dm.Return2OriginPos();
        }
        twig1.Return2OriginPos();
        twig1.enableDragMove = true;
    }
}
