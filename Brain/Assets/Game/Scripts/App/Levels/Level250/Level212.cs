
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level212 : LevelBasePage {
	public List<Sprite> images;
	public Sprite imageOpen;
	public Sprite imageClose;
	public Image myDiceBone;
	public Image otherDiceBone;
	public Image myDice;
	public Image otherDice;
	private bool _open;
	private int _myDiceNum;
	private int _otherDiceNum;
	public DragMove diceMove;
	protected override void Start() {
		base.Start();
		_otherDiceNum = 5;
		_myDiceNum = 1;
		diceMove.enabelDrag = false;
		diceMove.onSwipe = (a) => {
			if (a == SwipeDirection.Up) {
				_otherDiceNum = 2;
				otherDice.sprite = images[_otherDiceNum-1];
			}
		};
		myDiceBone.GetComponent<Button>().onClick.AddListener(() => {
			if (!_open) {
				_open = true;
				myDiceBone.sprite = imageOpen;
				_myDiceNum = Random.Range(1, 5);
				myDice.sprite = images[_myDiceNum-1];
				myDice.gameObject.SetActive(true);
				After(() => {
						if (_myDiceNum > _otherDiceNum) {
							Completion();
						}
						else {
							ShowError();
							Refresh();
						}
				},0.5f);
			}
		});
	}

	public override void Refresh() {
		base.Refresh();
		_open = false;
		_otherDiceNum = 5;
		_myDiceNum = 1;
		otherDice.sprite = images[_otherDiceNum-1];
		myDiceBone.sprite = imageClose;
		myDice.sprite = images[_myDiceNum-1];
		myDice.gameObject.SetActive(false);
	}
}
