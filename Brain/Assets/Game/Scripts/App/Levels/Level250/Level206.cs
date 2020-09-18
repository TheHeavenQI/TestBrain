using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Level206 : LevelBasePage
{
	public List<Button> btnlist;

    public Image tree;

    private Vector3 treePos;

	protected override void Start()
	{
		base.Start();
		for (int i = 0; i < btnlist.Count; i++)
		{
			var btn = btnlist[i];
			btn.onClick.AddListener(() =>
			{
				if (btn.name == "Green")
				{
					Completion();
				}
				else
				{
					ShowErrorWithMousePosition();
				}
			});
		}

        treePos = tree.transform.localPosition;

	}

    public override void Refresh()
    {
        base.Refresh();
        tree.transform.localPosition = treePos;
    }

}
