using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Level096 : LevelBasePage
{

    public FingureMoveIsPassingBy touchCallBack;

    private bool isShowBoom = false;

    public List<Image> images;

    public Image head;

    public Image boom;

    private List<Vector3> positons = new List<Vector3>();

    protected override void Start()
    {
        base.Start();

        touchCallBack.fingureMovePassByCallBack += Toucheed;
        var parentRect = transform.GetComponent<RectTransform>();

        for (int i = 0; i < images.Count; i++)
        {
            positons.Add(images[i].transform.localPosition);
            images[i].gameObject.GetComponent<DragMove>().onClick = () => {
                ShowErrorWithMousePosition();
            };

            if (i <= 1)
            {
                images[i].gameObject.GetComponent<DragMove>().enabelDrag = false;
            }

        }

        boom.GetComponent<DragMove>().onDragEnd = () => {
            if (boom.rectTransform.anchoredPosition.y > parentRect.rect.height * 0.5f||
            boom.rectTransform.anchoredPosition.x > parentRect.rect.width * 0.5f ||
            boom.rectTransform.anchoredPosition.x < -parentRect.rect.width * 0.5f ||
            boom.rectTransform.anchoredPosition.y < -parentRect.rect.height * 0.5f)
            {
                Completion();
            }
        };

    }

    
    private void Toucheed(GameObject obj)
    {
        if (isShowBoom == true)
        {
            return;
        }
        isShowBoom = true;

        head.transform.DOLocalMoveY(200,1f);
        
    }

    public override void Refresh()
    {
        base.Refresh();
        isShowBoom = false;
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
