using UnityEngine;
using UnityEngine.UI;

namespace BaseFramework
{
    [RequireComponent(typeof(Text))]
    public class LocalizotionText : MonoBehaviour
    {
        public string key;
        private void Awake()
        {
            if (!string.IsNullOrEmpty(key))
            {
                GetComponent<Text>().text = Localization.GetText(key);
            }
        }
    }
}
