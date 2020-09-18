using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class Level209 : LevelBasePage {
	public List<Image> images;
	private List<Vector3> _pos = new List<Vector3>();
	public Sprite imageOpen;
	public Sprite imageClose;
	public Button douzi;
	private float _time;
	private bool _startAnim;

	private float _animTime = 0.5f;
	private List<int> _animIndex = new List<int>();
	private Tweener _tweener1;
	private Tweener _tweener2;
	protected override void Start() {
		base.Start();
		_time = 0;
		for (int i = 0; i < images.Count; i++) {
			int k = i;
			_pos.Add(images[i].transform.localPosition);
			images[i].GetComponent<Button>().onClick.AddListener(() => {
				if (_animIndex.Count == 0) {
					ShowErrorWithMousePosition();
					images[k].sprite = imageOpen;
					_time = 0;
					After(() => {
						Refresh();
					},1.5f);
				}
			});
		}
		douzi.onClick.AddListener(() => {
			CompletionWithMousePosition();
		});
	}

	private void Update() {
		_time += Time.deltaTime;
		if (_time > 2 && _startAnim == false) {
			_startAnim = true;
			images[0].sprite = imageClose;
			douzi.gameObject.SetActive(false);
			_animIndex = new List<int>(){01,12,20,01,12};
			StartAnimation();
		}
	}

	private void StartAnimation() {
		if (!_startAnim || _animIndex.Count == 0) {
			return;
		}
		int index = _animIndex[0];
		_animIndex.RemoveAt(0);
		int index1 = index / 10;
		int index2 = index % 10;
		StartAnim(index1, index2);
		After(() => { StartAnimation(); },_animTime+0.2f);
	}
	private void StartAnim(int index1,int index2) {
		var tmpImage1 = images[index1];
		var tmpImage2 = images[index2];
		var loc1 = tmpImage1.transform.localPosition;
		var loc2 = tmpImage2.transform.localPosition;
		_tweener1 = tmpImage1.transform.DOLocalMove(loc2, _animTime);
		_tweener2 = tmpImage2.transform.DOLocalMove(loc1, _animTime);
		images[index1] = tmpImage2;
		images[index2] = tmpImage1;
	}

	public override void Refresh() {
		base.Refresh();
		_time = 0;
		_startAnim = false;
		_tweener1?.Kill();
		_tweener2?.Kill();
		for (int i = 0; i < _pos.Count; i++) {
			images[i].transform.localPosition = _pos[i];
			if (i == 0) {
				images[i].sprite = imageOpen;
			}
			else {
				images[i].sprite = imageClose;
			}
		}
		douzi.gameObject.SetActive(true);
	}
}
