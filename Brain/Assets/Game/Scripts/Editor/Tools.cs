using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Linq;

public class Tools
{

    [MenuItem("Tools/CheckSpriteAtlas")]
    public static void CheckSpriteAtlas()
    {

        int maxAtlasCount = 999;

        string imagePath = Application.dataPath + "/Game/Images/Levels/";
        string atlasPath = Application.dataPath + "/Game/SpriteAtlas/Levels/";
        string example = File.ReadAllText(atlasPath + "Level001.spriteatlas");

        DirectoryInfo atlasDI = new DirectoryInfo(atlasPath);
        HashSet<string> hadAtlasFiles = new HashSet<string>();
        foreach (var file in atlasDI.GetFiles())
        {
            string name = file.Name;
            if (!name.Contains(".meta"))
            {
                hadAtlasFiles.Add(name);
            }
        }

        DirectoryInfo imagesDI = new DirectoryInfo(imagePath);
        HashSet<int> imagesFiles = new HashSet<int>();
        foreach (var file in imagesDI.GetFiles())
        {
            string name = file.Name.Replace(".meta", "").Replace("Level", "");
            if (!string.IsNullOrEmpty(name) && int.TryParse(name, out int i))
            {
                imagesFiles.Add(i);
            }
        }

        Debug.Log($"CheckSpriteAtlas imagesFiles Count: {imagesFiles.Count}");
        Debug.Log($"CheckSpriteAtlas hadAtlasFiles Count: {hadAtlasFiles.Count}");

        for (int i = 1; i <= maxAtlasCount; ++i)
        {
            string fileName = $"Level{i.ToString("000")}.spriteatlas";
            if (!imagesFiles.Contains(i))
            {
                if (hadAtlasFiles .Contains(fileName))
                {
                    Debug.LogError($"CheckSpriteAtlas useless atlas: {fileName}, please delete it manually");
                }
                continue;
            }
           
            if (hadAtlasFiles.Contains(fileName))
            {
                continue;
            }

            File.WriteAllText(atlasPath + fileName, example);
            Debug.Log($"CheckSpriteAtlas Creat File: {fileName}");
        }

        AssetDatabase.Refresh();
        Debug.Log("CheckSpriteAtlas Finish");
    }
}
