using UnityEngine;
using UnityEngine.UI;

public class Christ001ImageHide : MonoBehaviour
{

    public Image img;

    private void Reset()
    {
        img = this.GetComponent<Image>();
    }

    private void Awake()
    {
        if (img == null || img.gameObject != this.gameObject)
        {
            img = this.GetComponent<Image>();
        }
        if (img != null)
        {
            img.color = Color.clear;
        }
    }
}
