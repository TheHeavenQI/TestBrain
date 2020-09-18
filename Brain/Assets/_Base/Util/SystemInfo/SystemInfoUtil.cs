using System;
using BaseFramework;
using UnityEngine;

public class SystemInfoUtil
{
    public static bool IsGpuSupportBlur()
    {
        string graphicsDeviceName = SystemInfo.graphicsDeviceName;

        Log.I("SystemInfoUtil", $"graphicsDeviceName:{graphicsDeviceName}");

        if (graphicsDeviceName.IsNullOrEmpty())
            return false;

#if UNITY_EDITOR
        return true;
#elif UNITY_ANDROID
        // 高通GPU
        if (graphicsDeviceName.Contains("Adreno"))
        {
            string[] sections = graphicsDeviceName.Split(new []{' '},
                                                         StringSplitOptions.RemoveEmptyEntries);
            if (sections.Any()
                && int.TryParse(sections[sections.Length - 1], out int adrenoVersion))
            {
                return adrenoVersion >= 500;
            }
        }

        return SystemInfo.graphicsMemorySize >= 1024;
#elif UNITY_IOS
        string[] sections = graphicsDeviceName.Split(new[] {' '},
                                                     StringSplitOptions.RemoveEmptyEntries);

        if (sections.Length > 1
            && int.TryParse(sections[1].Substring(1), out int gpuVersion))
        {
            return gpuVersion > 8;
        }

        return false;
#else
        return false;
#endif
    }
}