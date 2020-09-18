
using UnityEngine.UI;
public class Level282 : LevelBasePage
{
    public Button bookButton;
    public Button correctButton;

    protected override void Start()
    {
        base.Start();
        correctButton.onClick.AddListener(() =>
        {
            CompletionWithMousePosition();
        });
        bookButton.onClick.AddListener(() =>
        {
            ShowErrorWithMousePosition();
        });
    }
}
