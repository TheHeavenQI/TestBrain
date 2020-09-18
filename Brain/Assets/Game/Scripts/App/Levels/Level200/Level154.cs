using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Level154 : LevelBasePage
{
	public int answer;
	private Button _plus;
	private Button _reduce;
	private Button _okBtn;
	private Text _numText;
	private int _num;
	protected override void Awake()
	{
		base.Awake();
		_plus = transform.Find("plus").GetComponent<Button>();
		_reduce = transform.Find("reduce").GetComponent<Button>();
		var ok = transform.Find("ok");
		_okBtn = transform.Find("ok").GetComponent<Button>();
		_numText = transform.Find("Text").GetComponent<Text>();

		_plus.onClick.AddListener(() => {
			_num++;
			_numText.text = $"{_num}";
		});
		_reduce.onClick.AddListener(() => {
			_num--;
			_num = Math.Max(_num, 0);
			_numText.text = $"{_num}";
		});
		_okBtn.onClick.AddListener(() => {
			if (_num == answer && _reduce.transform.localPosition.x < _numText.transform.localPosition.x)
			{
				OnCompletion();
				Completion();
			}
			else
			{
				ShowError();
			}
		});
		Refresh();
	}

	public override void Refresh()
	{
		base.Refresh();
		_num = 0;
		_numText.text = $"{_num}";
	}
}
