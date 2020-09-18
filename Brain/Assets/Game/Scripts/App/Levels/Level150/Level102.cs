using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Level102 : LevelBasePage
{
    public FingureMoveIsPassingBy touchCallBack;

    private bool isShow = false;

    public List<Image> images;

    public Image left;

    public Image right;

    public Image rightarea;

    public Image leftArea;

    public Sprite leftCleanImg;

    public Sprite rightCleanImg;

    private List<Vector3> positons = new List<Vector3>();
    
    private bool leftClean = false;
    private bool rightClean = false;

    protected override void Start()
    {
        base.Start();

        touchCallBack.fingureMovePassByCallBack += Toucheed;
        var parentRect = transform.GetComponent<RectTransform>();

        for (int i = 0; i < images.Count; i++)
        {
            positons.Add(images[i].transform.localPosition);
            images[i].gameObject.GetComponent<DragMove>().onClick = () =>
            {
                ShowErrorWithMousePosition();
            };

            if (i <= 2)
            {
                images[i].gameObject.GetComponent<DragMove>().enabelDrag = false;
            }else
            {
                images[i].gameObject.GetComponent<DragMove>().enabelDrag = false;
                images[i].gameObject.GetComponent<DragMove>().onDragEnd = OnDreagEnded;
            }

        }
    }

    private void Toucheed(GameObject obj)
    {
        if (isShow == true)
        {
            return;
        }
        isShow = true;

        left.transform.DOLocalMoveX(100, 0.5f);
        right.transform.DOLocalMoveX(-100, 0.5f);

        left.GetComponent<DragMove>().enabelDrag = true;
        right.GetComponent<DragMove>().enabelDrag = true;

    }

    private void OnDreagEnded()
    {

        if (leftClean == false)
        {
            if (RectTransformExtensions.IsRectTransformOverlap(leftArea.rectTransform, left.rectTransform))
            {
                images[0].sprite = leftCleanImg;
                leftClean = true;
                left.gameObject.SetActive(false);

            } else if (RectTransformExtensions.IsRectTransformOverlap(leftArea.rectTransform, right.rectTransform))
            {
                images[0].sprite = leftCleanImg;
                leftClean = true;
                right.gameObject.SetActive(false);
            }
        }
        if (rightClean == false)
        {
            if (RectTransformExtensions.IsRectTransformOverlap(rightarea.rectTransform, left.rectTransform))
            {
                images[1].sprite = rightCleanImg;
                rightClean = true;
                left.gameObject.SetActive(false);

            }
            else if (RectTransformExtensions.IsRectTransformOverlap(rightarea.rectTransform, right.rectTransform))
            {
                images[1].sprite = rightCleanImg;
                rightClean = true;
                right.gameObject.SetActive(false);
            }
            
        }

        JudgeIsSuccess();

    }

    private void JudgeIsSuccess()
    {
        if (leftClean && rightClean)
        {
            CompletionWithMousePosition();
        }
    }

    public override void Refresh()
    {
        base.Refresh();
        isShow = false;
        leftClean = false;
        rightClean = false;
        left.gameObject.SetActive(true);
        right.gameObject.SetActive(true);

        for (int i = 0; i < images.Count; i++)
        {
            images[i].transform.localPosition = positons[i];
        }
    }

    private void OnDestroy()
    {
        touchCallBack.fingureMovePassByCallBack -= Toucheed;
    }

}
