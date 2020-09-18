using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFramework;

public class InputNumMutiLanguageLevel : InputNumLevel
{
	public string text_key;
	protected override void Start()
	{
		base.Start();
		base.answer = Localization.GetText(text_key);
	}
}
