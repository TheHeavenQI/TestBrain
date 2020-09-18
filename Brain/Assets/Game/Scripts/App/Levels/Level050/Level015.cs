
using UnityEngine.UI;

public class Level015 : LevelBasePage {
    public Button btn;
    protected override void Start() {
        base.Start();
        btn.onClick.AddListener(() => {
            ShowErrorWithMousePosition();
        });
        levelQuestionText.gameObject.AddComponent<Button>();
        levelQuestionText.raycastTarget = true;
        var question = levelQuestionText.gameObject.GetComponent<Button>();
        question.onClick.AddListener(() => {
            CompletionWithMousePosition();
        });
    }
}
