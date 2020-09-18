using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

using UnityEngine.UI;

public class Level208 : LevelBasePage {
	public Sprite sprite1;
	public Sprite sprite2;
	public List<Button> list;
	public DragMove yun;
	public Transform sun;
	private bool _moveYun;
	protected override void Start() {
		base.Start();
		yun.onDragEnd = () => {
			_moveYun = Vector2.Distance(yun.transform.localPosition, sun.localPosition) > 30;
			if (_moveYun) {
				for (int i = 0; i < list.Count; i++) {
					if (i == 2) {
						list[i].GetComponent<Image>().sprite = sprite1;
					}
					else {
						list[i].GetComponent<Image>().sprite = sprite2;
					}
				}
			}
		};
		for (int i = 0; i < list.Count; i++) {
			int k = i;
			list[i].onClick.AddListener(() => {
				if (_moveYun && k == 2) {
					CompletionWithMousePosition();
				}
				else {
					ShowErrorWithMousePosition();
				}
			});
		}
	}

	public override void Refresh() {
		base.Refresh();
		_moveYun = false;
		for (int i = 0; i < list.Count; i++) {
			list[i].GetComponent<Image>().sprite = sprite1;
		}
		yun.Return2OriginPos();
	}
}
