using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Level074 : LevelBasePage
{

    public List<Image> images;

    private List<Vector3> positons = new List<Vector3>();

    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < images.Count; i++)
        {
            positons.Add(images[i].transform.localPosition);
            images[i].gameObject.GetComponent<DragMove>().onClick = () => {
                ShowErrorWithMousePosition();
            };
            images[i].gameObject.GetComponent<DragMove>().onDragEnd = OnDreagEnded;
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

    private void OnDreagEnded()
    {

        if (RectTransformExtensions.IsRectTransformOverlap(images[0].rectTransform, images[1].rectTransform) && Mathf.Abs(images[0].rectTransform.localPosition.x - images[1].rectTransform.localPosition.x) < 10)
        {
            CompletionWithMousePosition();
        }

    }

}
