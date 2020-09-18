using BaseFramework;
using UnityEngine.UI;

public class Level231 : InputNumLevel
{
    public Image image1;
    public Image image2;

    protected override void Start()
    {
        base.Start();

        this.answer = Localization.GetText("level_231_text");

        this.GetComponent<Shake>().shakeAction = () => {
            image1.gameObject.SetActive(false);
            image2.gameObject.SetActive(true);
        };

        image1.gameObject.SetActive(true);
        image2.gameObject.SetActive(false);
    }
}
