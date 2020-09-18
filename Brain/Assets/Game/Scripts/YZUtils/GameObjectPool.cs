using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour {
    public List<GameObject> prefabs;
    private static GameObjectPool Instance;
    private Dictionary<string,GameObjectPoolSingle> _dict = new Dictionary<string,GameObjectPoolSingle>();
    private void Awake() {
        Instance = this;
        DontDestroyOnLoad(this);
        foreach (var prefab in prefabs) {
            _dict[prefab.tag] = new GameObjectPoolSingle(prefab);
        }
    }
    /// <summary>
    /// 延迟执行
    /// </summary>
    public static void After(Action action, float delay) {
        Instance.StartCoroutine(Instance.AfterDoEvent(action, delay));
    }
    private IEnumerator AfterDoEvent(Action action, float delay) {
        yield return new WaitForSeconds(delay);
        try
        {
            action?.Invoke();
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
    private GameObjectPoolSingle Get(string tag) {
        if (_dict.ContainsKey(tag) == false) {
            Debug.LogError($"不存在的key:{tag}");
            return null;
        }else {
            return _dict[tag];
        }
    }

    private void Remove(GameObject obj) {
        var single = Get(obj.tag);
        if (single != null) {
            single.Remove(obj);
        }
        else {
            Debug.LogError("not found pool_name:{obj.tag}");
        }
    }
    
    public static GameObjectPoolSingle GetPool(string tag) {
        return Instance.Get(tag);
    }

    public static void RemoveObject(GameObject obj) { 
        Instance.Remove(obj);
        obj.transform.SetParent(Instance.transform, false);
    }
}

public class GameObjectPoolSingle{

    /// <summary>
    /// 缓存池中的未使用的对象列表
    /// </summary>
    public List<GameObject> cachelist => _cachelist;
    /// <summary>
    /// 缓存池中的正在使用的对象列表
    /// </summary>
    public List<GameObject> showlist => _showlist;

    public string Tag => _prefab.tag;
    /// <summary>
    /// 预制体，对象池的对象prefab，用于生成对象
    /// </summary>
    private GameObject _prefab;
    private List<GameObject> _cachelist = new List<GameObject>();
    private List<GameObject> _showlist = new List<GameObject>();
    
    public GameObjectPoolSingle(GameObject prefab) {
        _prefab = prefab;
    }
    /// <summary>
    /// 从缓存池取对象
    /// </summary>
    public GameObject Get() {
        GameObject obj = null;
        if (_cachelist.Count>0) {
            obj = _cachelist[0];
            _cachelist.RemoveAt(0);
        }
        else {
            obj = GameObject.Instantiate(_prefab); 
            int count = _showlist.Count + _cachelist.Count;
            obj.name = $"Pool_{_prefab.tag}_{count}";
        }
        _showlist.Add(obj);
        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// 将对象放入缓存池
    /// </summary>
    /// <param name="obj"></param>
    public void Remove(GameObject obj) {
        if (obj.tag.Equals(_prefab.tag) == false) {
            Debug.LogError($"不能将不同类型对象放入缓存池 传入：{obj.tag}  需要：{_prefab.tag}");
            return;
        }
        if (_cachelist.Contains(obj) == false) {
            _cachelist.Add(obj);
            _showlist.Remove(obj);
            obj.SetActive(false);
        }
    }
    
}