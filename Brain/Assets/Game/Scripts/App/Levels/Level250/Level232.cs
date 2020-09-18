using UnityEngine.UI;
public class Level232 : LevelBasePage
{
    public Button button;
    public Image imageOff;

    protected override void Start()
    {
        base.Start();
        imageOff.gameObject.SetActive(false);

        button.onClick.AddListener(() => {
            Completion();
            button.gameObject.SetActive(false);
            imageOff.gameObject.SetActive(true);
        });
    }
}
