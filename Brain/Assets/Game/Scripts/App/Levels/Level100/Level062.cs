using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level062 : LevelBasePage
{
    public List<Image> images;
    private List<Vector3> positons = new List<Vector3>();

    public Sprite fire;

    public Sprite paper;

    private int fireCount = 0;

    private bool isOnFire = false;

    private bool canFire = false;

    private Vector3 titlePos;

    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < images.Count; i++)
        {
            int j = i;
            positons.Add(images[j].transform.localPosition);

            images[j].gameObject.GetComponent<DragMove>().onClick = () => {
                if (j == 2 && canFire)
                {
                    Fire();
                }
                else
                {
                    ShowErrorWithMousePosition();
                }

            };
            images[j].gameObject.GetComponent<DragMove>().onDragEnd = () => {
                if (j == 3)
                {
                    return;
                }
                if (!canFire && !isOnFire && RectTransformExtensions.IsRectTransformOverlap(images[2].rectTransform, images[3].rectTransform))
                {
                    canFire = true;
                }

                if (isOnFire && RectTransformExtensions.IsRectTransformOverlap(images[3].rectTransform.localPosition, images[3].rectTransform.sizeDelta * images[3].rectTransform.localScale, images[j].rectTransform.localPosition, images[j].rectTransform.sizeDelta * images[j].rectTransform.localScale))
                {
                    fireCount += 1;
                    images[3].rectTransform.DOScale(images[3].rectTransform.localScale.x * 1.2f, 0.3f);
                    images[j].gameObject.SetActive(false);
                }

                if (fireCount > 8)
                {
                    Completion();
                }

            };

        }

        // 标题

        titlePos = levelQuestionText.transform.localPosition;

        levelQuestionText.gameObject.GetComponent<DragMove>().onDragEnd = () => {

            if (isOnFire && RectTransformExtensions.IsRectTransformOverlap(images[3].rectTransform.localPosition, images[3].rectTransform.sizeDelta * images[3].rectTransform.localScale, levelQuestionText.rectTransform.localPosition, levelQuestionText.rectTransform.sizeDelta))
            {
                fireCount += 1;
                images[3].rectTransform.DOScale(images[3].rectTransform.localScale.x * 1.2f, 0.3f);
                levelQuestionText.gameObject.SetActive(false);
            }

            if (fireCount >= 8)
            {
                Completion();
            }

        };
    }

    protected override void OnCompletion()
    {
        images[3].rectTransform.DOKill();
        images[3].rectTransform.DOScale(0, delayComplete);
        base.OnCompletion();
    }

    public override void Refresh()
    {
        base.Refresh();

        fireCount = 0;
        canFire = false;
        isOnFire = false;

        for (int i = 0; i < images.Count; i++)
        {
            images[i].transform.localPosition = positons[i];
            images[i].gameObject.SetActive(true);
        }

        images[3].rectTransform.DOKill();
        images[3].rectTransform.localScale = new Vector3(1, 1, 1);
        images[3].sprite = paper;
        images[3].SetNativeSize();


        levelQuestionText.transform.localPosition = titlePos;
        levelQuestionText.gameObject.SetActive(true);
    }


    private void Fire()
    {
        fireCount += 1;
        canFire = false;
        isOnFire = true;

        images[3].sprite = fire;
        images[3].SetNativeSize();
        images[3].rectTransform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }
}
