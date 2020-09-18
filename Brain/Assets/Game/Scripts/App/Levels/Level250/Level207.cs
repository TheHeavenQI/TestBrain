using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

using UnityEngine.UI;

public class Level207 : LevelBasePage {
	public GameObject bombIamge;
	public Button BombButton;
	public Button offButton;
	public DragMove ClockTransform;
	private float _time;
	private bool _finished;
	protected override void Start() {
		base.Start();
		offButton.onClick.AddListener(() => {
			CompletionWithMousePosition();
		});
		_time = 0;
		BombButton.onClick.AddListener(() => {
			ShowError();
		});
	}

	private void Update() {
		_time += Time.deltaTime;
		if (_time > 3 && !isLevelComplete && !_finished) {
			_finished = true;
			bombIamge.transform.localScale = new Vector3(0,0,0);
			bombIamge.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(() => {
				After(() => {
					bombIamge.transform.localScale = new Vector3(0,0,0);
				},0.3f);
				After(() => {
					Refresh();
				},1.5f);
			});
		}
	}

	public override void Refresh() {
		base.Refresh();
		_time = 0;
		_finished = false;
		ClockTransform.Return2OriginPos();
	}
}
