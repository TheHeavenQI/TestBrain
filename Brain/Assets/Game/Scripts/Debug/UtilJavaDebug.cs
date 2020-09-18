using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class UtilJavaDebug:MonoBehaviour
{
    private float _ti;
    void Start () {
        _ti = 0;
        var hash = new Hashtable();
        var hash2 = new Hashtable();
        hash2.Add("worth",0.006);
        hash.Add("info","ok");
        hash.Add("status",1);
        hash.Add("data",hash2);
        var str = JsonConvert.SerializeObject(hash);
        Debug.LogError($"aa:{getWorth(str)}");
    }

    private float getWorth(string text) {
        Hashtable a = (Hashtable)clientCore.MiniJSON.jsonDecode(text);
        if (a.ContainsKey("data")) {
            Hashtable b = a["data"] as Hashtable;
            if (b.ContainsKey("worth")) {
                var worth = b["worth"].ToString();
                float worthFloat = float.Parse(worth);
                return worthFloat;
            }
        }
        return 0;
    }
    private void Update() {
        _ti += Time.deltaTime;
        if (_ti > 5) {
            _ti = 0;
        }
    }
    
    
}
