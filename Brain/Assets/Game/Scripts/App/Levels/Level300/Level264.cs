using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level264 : LevelBasePage
{
    public GameObject doorClosedGo;
    public Button openDoorBtn;
    public RectTransform doorplate;
    public Button nailBtn;

    public GameObject[] nailGos;
    public DragMoveEventTrigger[] dragMoves;

    private int _nailStep = 0;

    protected override void Start()
    {
        base.Start();

        openDoorBtn.onClick.AddListener(() => {
            doorClosedGo.SetActive(false);
            Completion();
        });

        for (int i = 0; i < dragMoves.Length; ++i)
        {
            int j = i;
            dragMoves[j].onEndDrag = (d) => {
                dragMoves[j].Return2OriginPos();
            };
        }

        nailBtn.onClick.AddListener(() => {
            switch (_nailStep)
            {
                case 0:
                    nailGos[0].SetActive(false);
                    nailGos[1].SetActive(true);
                    ++_nailStep;
                    break;
                case 1:
                    nailGos[1].SetActive(false);
                    nailGos[2].SetActive(true);
                    ++_nailStep;
                    break;
                case 2:
                    nailGos[2].SetActive(false);
                    ++_nailStep;
                    doorplate.DORotate(new Vector3(0, 0, -90), 0.5f);
                    break;
                default:
                    break;
            }
        });
    }

    public override void Refresh()
    {
        base.Refresh();
        nailGos[0].SetActive(true);
        nailGos[1].SetActive(false);
        nailGos[2].SetActive(false);
        _nailStep = 0;

        doorplate.DOKill();
        doorplate.localEulerAngles = Vector3.zero;
    }
}
