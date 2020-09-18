using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour {
    public static ControllerManager Instance;
    public Dictionary<string, BaseController> _controllers = new Dictionary<string, BaseController>();
    private void Awake() {
        Instance = this;
    }
    public void Init() {
        GetController<ContentController>().gameObject.SetActive(true);
        if (QiaoDaoManager.TodayQiaoDao() == false) {
            Invoke("ShowQiaoDao",0.1f);
        }
    }

    /// <summary>
    /// 如果没有签到，显示签到
    /// </summary>
    private void ShowQiaoDao() {
        QiaoDaoController ctr = GetController<QiaoDaoController>();
        ctr.Init();
        ctr.gameObject.SetActive(true);
    }
    
    public T GetController<T> () where T : BaseController{
        var name = typeof(T).Name;
        if (!_controllers.ContainsKey(name)) {
            var a = Utils.GetObjectSingle($"Controller/{name}").GetComponent<T>();
            a.transform.SetParent(transform,false);
            _controllers[name] = a;
            a.gameObject.SetActive(false);
        }
        return _controllers[name] as T;
    }
    
}
