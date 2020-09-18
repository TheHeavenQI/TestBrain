using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;

public class Level162 : LevelBasePage
{
    public List<Image> images;
    private List<Vector3> positons = new List<Vector3>();

    private bool theKeyIsShow = false;

    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < images.Count; i++)
        {
            positons.Add(images[i].transform.localPosition);
            images[i].gameObject.GetComponent<DragMove>().onClick = () => {
                ShowErrorWithMousePosition();
            };

            if (i == 0)
            {
                images[i].gameObject.GetComponent<DragMove>().onDragEnd = OnDreagEnded;
            }
            else
            {
                images[i].gameObject.GetComponent<DragMove>().enabelDrag = false;
                images[i].gameObject.GetComponent<DragMove>().onDragBegin = SopeOnDreagEnded;
            }

        }

    }

    public override void Refresh()
    {
        base.Refresh();

        for (int i = 0; i < images.Count; i++)
        {
            images[i].transform.localPosition = positons[i];
        }
    }

    private void SopeOnDreagEnded()
    {
        images[1].transform.DOLocalMove(new Vector3(images[1].transform.localPosition.x + 120, images[1].transform.localPosition.y + 50, images[1].transform.localPosition.z), 0.5f).SetEase(Ease.OutQuart);
    }


    private void OnDreagEnded()
    {

        if (RectTransformExtensions.IsRectTransformOverlap(images[0].rectTransform, images[1].rectTransform))
        {
            CompletionWithMousePosition();
        }

    }

}
