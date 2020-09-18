using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class Level122 : LevelBasePage {


    public List<Image> images;
    private List<Vector3> positons = new List<Vector3>();

    public Shake shake;
    
    
    protected override void Start() {
        base.Start();
        images[3].gameObject.SetActive(false);
        for (int i = 0; i < images.Count; i++)
        {
            positons.Add(images[i].transform.localPosition);
            if (i == 3) {
                break;
            }
            images[i].gameObject.GetComponent<DragMove>().onClick = () => {
                ShowErrorWithMousePosition();
            };
            
            if (i == 0)
            {
                images[i].gameObject.GetComponent<DragMove>().enabelDrag = false;
            }

            images[i].gameObject.GetComponent<DragMove>().onDragEnd = () =>
            {
                if (RectTransformExtensions.IsRectTransformOverlap(images[0].rectTransform, images[2].rectTransform))
                {
                    images[2].gameObject.SetActive(false);
                    images[3].gameObject.SetActive(true);
                    images[3].transform.DOLocalMoveX(-228, 0.8f).SetEase(Ease.OutQuart);
                    images[0].transform.DOLocalMoveX(-380, 0.8f).SetEase(Ease.OutQuart).onComplete = () => {
                        Completion();
                    };
                }
            };
        }
        
        shake.shakeAction = () =>
        {
            images[2].transform.DOLocalMoveY(100, 0.5f).SetEase(Ease.OutQuart);
        };

    }

    public override void Refresh()
    {
        base.Refresh();

        for (int i = 0; i < images.Count; i++)
        {
            images[i].transform.localPosition = positons[i];
        }
        images[2].gameObject.SetActive(true);
        images[3].gameObject.SetActive(false);
        
    }


}
