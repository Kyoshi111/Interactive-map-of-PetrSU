#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace GitIntegration.SmartMerge
{
    [InitializeOnLoad]
    public class SmartMergeRegistrar
    {
        private const string SmartMergeRegistrarEditorPrefsKey = "smart_merge_installed";
        private const int Version = 1;
        private static readonly string VersionKey = $"{Version}_{Application.unityVersion}";

        [MenuItem("Tools/Git/SmartMerge registration")]
        static void SmartMergeRegister()
        {
            try
            {
                var unityYamlMergePath = EditorApplication.applicationContentsPath + "/Tools" + "/UnityYAMLMerge.exe";
                Utils.ExecuteGitWithParams("config merge.unityyamlmerge.name \"Unity SmartMerge (UnityYamlMerge)\"");
                Utils.ExecuteGitWithParams($"config merge.unityyamlmerge.driver \"\\\"{unityYamlMergePath}\\\" merge -h -p --force --fallback none %O %B %A %A\"");
                Utils.ExecuteGitWithParams("config merge.unityyamlmerge.recursive binary");
                EditorPrefs.SetString(SmartMergeRegistrarEditorPrefsKey, VersionKey);
                Debug.Log($"Successfully registered UnityYAMLMerge with path {unityYamlMergePath}");
            }
            catch (Exception e)
            {
                Debug.Log($"Fail to register UnityYAMLMerge with error: {e}");
            }
        }

        [MenuItem("Tools/Git/SmartMerge unregistration")]
        static void SmartMergeUnRegister()
        {
            Utils.ExecuteGitWithParams("config --remove-section merge.unityyamlmerge");
            Debug.Log($"Successfully unregistered UnityYAMLMerge");
        }

        //Unity calls the static constructor when the engine opens
        static SmartMergeRegistrar()
        {
            var installedVersionKey = EditorPrefs.GetString(SmartMergeRegistrarEditorPrefsKey);
            if (installedVersionKey != VersionKey)
                SmartMergeRegister();
        }
    }
}
#endif