using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Level080 : LevelBasePage
{
    public List<Image> images;
    private List<Vector3> positons = new List<Vector3>();

    public Sprite sprite;

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

        if (!theKeyIsShow && !RectTransformExtensions.IsRectTransformOverlap(images[0].rectTransform, images[1].rectTransform))
        {
            theKeyIsShow = true;
            images[1].transform.SetAsLastSibling();
        }

        if (RectTransformExtensions.IsRectTransformOverlap(images[0].rectTransform, images[1].rectTransform) && theKeyIsShow)
        {
            images[0].sprite = sprite;
            images[0].transform.SetAsLastSibling();
            CompletionWithMousePosition();
        }

    }

}
