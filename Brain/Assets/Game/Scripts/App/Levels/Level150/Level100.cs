using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Level100 : LevelBasePage
{
	public List<Image> images;
	private List<Vector3> positons = new List<Vector3>();

    private Text level;

	protected override void Start()
	{
		base.Start();

		for (int i = 0; i < images.Count; i++)
		{
			positons.Add(images[i].transform.localPosition);
			images[i].gameObject.GetComponent<DragMove>().onClick = () => {
				ShowErrorWithMousePosition();
			};
            images[i].gameObject.GetComponent<DragMove>().enabelDrag = false;
        }

        level = Instantiate(levelTitleText, transform);
        level.rectTransform.anchoredPosition = levelTitleText.rectTransform.anchoredPosition;

        int id = ConfigManager.Current().Questions.GetModel(levelIndex - 1).id;
        level.text = $"<color=#00000000>Lv.</color>{id}";
        
        levelTitleText.text = $"Lv.<color=#00000000>{id}</color>";

        

        level.gameObject.GetComponent<DragMove>().onClick = () => {
            ShowErrorWithMousePosition();
        };

        level.gameObject.GetComponent<DragMove>().onDragEnd = () => {
            
            if (level.gameObject.transform.position.x - images[0].rectTransform.position.x > 1.2 && Mathf.Abs(level.gameObject.transform.position.y - images[0].rectTransform.position.y) < 2)
            {
                Completion();
            }
        };

    }

	public override void Refresh()
	{
		base.Refresh();

		for (int i = 0; i < images.Count; i++)
		{
			images[i].transform.localPosition = positons[i];
		}

        level.rectTransform.anchoredPosition = levelTitleText.rectTransform.anchoredPosition;

    }

	
}
