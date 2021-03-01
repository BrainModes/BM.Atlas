using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

namespace Utils
{
    public static class LocalStreamingAssetLoader 
    {

        public static string GetStreamingAsset(string pathAndFileNameInsideStreamingAssets) {
            string streamingAssetsPath = GetStreamingAssetsPath();
            string filePath = streamingAssetsPath + "/" + pathAndFileNameInsideStreamingAssets;
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            else
            {
                Debug.LogError("Can not find file! " + filePath);
                return null;
            }
        }

        public static string GetStreamingAssetsPath() {
            string filePath = "";

#if UNITY_EDITOR
            filePath = Path.Combine(Application.streamingAssetsPath);

#elif UNITY_IOS
        filePath = Path.Combine(Application.dataPath + "/Raw");

#elif UNITY_ANDROID
        filePath = Path.Combine("jar:file://" + Application.dataPath + "!/assets/");

#else
        filePath = Path.Combine(Application.streamingAssetsPath);
#endif
            return filePath;
        }
    }
}
