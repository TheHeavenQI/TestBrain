
using UnityEngine;
using UnityEngine.UI;

public class FPSInfo : MonoBehaviour
{
    private Text _text;
    private int currentCount;
    private float currentTime;
    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        currentCount++;
        if (currentTime > 1)
        {
            _text.text = $"FPS:{currentCount}";
            currentTime = 0;
            currentCount = 0;
        }
    }
}
