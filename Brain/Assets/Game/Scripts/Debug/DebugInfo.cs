using UnityEngine;
using BaseFramework;

public class DebugInfo : MonoSingleton<DebugInfo>
{

    #region FPS
    [HideInInspector]
    public bool isShowFPS;
    /// <summary>
    /// 上一次更新帧率的时间;
    /// </summary>
    private float _lastUpdateShowTime = 0f;
    /// <summary>
    /// 更新帧率的时间间隔;
    /// </summary>
    private float _updateShowDeltaTime = 0.5f;
    /// <summary>
    /// 帧数
    /// </summary>
    private int _frameUpdate = 0;
    /// <summary>
    /// 帧率
    /// </summary>
    private int _FPS = 0;
    #endregion

    private GUIStyle _style = new GUIStyle();

    private void Start()
    {
        _lastUpdateShowTime = Time.realtimeSinceStartup;

        _style.fontSize = 40;
        _style.normal.textColor = Color.red;
    }

    private void Update()
    {
        #region FPS
        _frameUpdate++;
        if (Time.realtimeSinceStartup - _lastUpdateShowTime >= _updateShowDeltaTime)
        {
            _FPS = (int)(_frameUpdate / (Time.realtimeSinceStartup - _lastUpdateShowTime));
            _frameUpdate = 0;
            _lastUpdateShowTime = Time.realtimeSinceStartup;
        }
        #endregion
    }

    private void OnGUI()
    {
        if (isShowFPS)
        {
            GUI.Label(new Rect(10, 10, 300, 100), "FPS: " + _FPS, _style);
        }
    }
}
