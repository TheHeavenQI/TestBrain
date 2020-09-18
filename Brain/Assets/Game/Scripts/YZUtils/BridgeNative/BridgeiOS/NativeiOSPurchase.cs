using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class NativeiOSPurchase : MonoBehaviour
{

#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void NativeiOSPurchase_initProducts(string ids);
    [DllImport("__Internal")]
    private static extern void NativeiOSPurchase_purchase(string productID);
    [DllImport("__Internal")]
    private static extern void NativeiOSPurchase_restore();
#endif
    static private string[] _ids;
    static private NativeiOSPurchase _instance;
	static private void Init()
	{
		if (_instance == null)
		{
			_instance = new GameObject("NativeiOSPurchase").AddComponent<NativeiOSPurchase>();
		}
	}
    private void Awake()
	{
        DontDestroyOnLoad(this);
	}

    static private Action<bool, string[],string> _initProductsCallBack;
    static private Action<bool, string[], string> _restoreCallBack;
    static private Dictionary<string, Action<bool, string>> _purchaseCallbacks = new Dictionary<string, Action<bool, string>>();

    /// <summary>
    /// iOS初始化请求产品
    /// </summary>
    public static void InitProducts(string[] ids, Action<bool, string[],string> initProductsCallBack)
    {
        _ids = ids;
        _initProductsCallBack = initProductsCallBack;
        string products = string.Join(",", ids);
#if UNITY_EDITOR
        initProductsCallBack?.Invoke(true, ids, null);
        return;
#endif
#if UNITY_IOS && !UNITY_EDITOR
        NativeiOSPurchase.Init();
        NativeiOSPurchase_initProducts(products);
#endif
    }
    public static void Purchase(string productID, Action<bool,string> purchaseCallBack)
    {
        _purchaseCallbacks[productID] = purchaseCallBack;
#if UNITY_EDITOR
        purchaseCallBack(true, productID);
        return;
#endif

#if UNITY_IOS && !UNITY_EDITOR
        HudWait.ShowWaiting();
        NativeiOSPurchase_purchase(productID);
#endif
    }

    public static void Restore(Action<bool, string[], string> restoreCallBack)
    {
        _restoreCallBack = restoreCallBack;
#if UNITY_EDITOR
        restoreCallBack?.Invoke(true,_ids,null);
        return;
#endif
#if UNITY_IOS && !UNITY_EDITOR
        HudWait.ShowWaiting();
        NativeiOSPurchase_restore();
#endif
    }

    /// <summary>
    /// iOS初始化请求产品，成功回调
    /// </summary>
    /// <param name="ids">以‘，’分割的产品id</param>
    private void OnInitialize(string ids)
    {
        _initProductsCallBack?.Invoke(true, ids.Split(','),null);
        _initProductsCallBack = null;
    }
    /// <summary>
    /// iOS初始化请求产品，失败回调
    /// </summary>
    private void OnInitializeFailed(string error)
    {
        _initProductsCallBack?.Invoke(false, null, error);
        _initProductsCallBack = null;
    }
    /// <summary>
    /// 购买成功
    /// </summary>
    private void OnPurchased(string productID)
    {
        HudWait.HideWaiting();
        HudMsg.ShowMsg("Purchase success");
        if (_purchaseCallbacks.ContainsKey(productID))
        {
            _purchaseCallbacks[productID].Invoke(true,null);
            _purchaseCallbacks.Remove(productID);
        }
    }
    /// <summary>
    /// 购买失败
    /// </summary>
    private void OnPurchasedFailed(string error)
    {
        HudWait.HideWaiting();
        HudMsg.ShowMsg("Purchase failed");
        string[] array = error.Split("<__>".ToCharArray());
        if(array.Length == 2)
        {
            string productID = array[0];
            string errorMsg = array[1];
            if (_purchaseCallbacks.ContainsKey(productID))
            {
                _purchaseCallbacks[productID].Invoke(false, errorMsg);
                _purchaseCallbacks.Remove(productID);
            }
        }
        else
        {
            UtilsLog.LogError($"OnPurchasedFailed 格式不正确 {error}");
        }
    }
    /// <summary>
    /// 恢复内购成功
    /// </summary>
    private void OnRestored(string ids)
    {
        HudWait.HideWaiting();
        HudMsg.ShowMsg("Restore success");
        _restoreCallBack?.Invoke(true, ids.Split(','), null);
        _restoreCallBack = null;
    }
    /// <summary>
    /// 恢复内购失败
    /// </summary>
    private void OnRestoreFailed(string error)
    {
        HudWait.HideWaiting();
        HudMsg.ShowMsg("Restore failed");
        _restoreCallBack?.Invoke(false, null, error);
        _restoreCallBack = null;
    }
}
