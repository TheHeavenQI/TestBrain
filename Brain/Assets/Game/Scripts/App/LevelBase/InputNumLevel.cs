using UnityEngine;
using UnityEngine.UI;

public class InputNumLevel : LevelBasePage
{
    public GameObject inputNum;
    public string answer;
    private InputField _inputField;

    protected override void Awake()
    {
        base.Awake();
        Button okBtn = inputNum.transform.Find("ok").GetComponent<Button>();
        _inputField = inputNum.transform.Find("InputField").GetComponent<InputField>();
        okBtn.onClick.AddListener(() => {
            if (answer.Replace(" ", "").ToUpper() == _inputField.text.Replace(" ", "").ToUpper())
            {
                Completion();
            }
            else
            {
                ShowError();
            }
        });
    }

    public override void Refresh()
    {
        base.Refresh();
        _inputField.text = "";
    }
}
