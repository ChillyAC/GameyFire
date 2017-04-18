using System.IO;
using UnityEditor;
using UnityEngine;

public class UnityBuild_Android
{
    [MenuItem("Perform Build/Android Command Line Build")]
    static void CommandLineBuildAndroid()
    {
        Debug.Log("Command line build Android version\n------------------\n------------------");
        
        string[] scenes = UnityBuild_HelperMethods.GetBuildScenes();
        string path = "D:/Unity Projects/Publishing files/AutomatedUnityBuilds/GameyFire.apk";
        EditorUserBuildSettings.SetBuildLocation(BuildTarget.Android, path);
        if (scenes == null || scenes.Length == 0 || path == null)
            return;

        Debug.Log(string.Format("Path: \"{0}\"", path));
        for (int i = 0; i < scenes.Length; ++i)
        {
            Debug.Log(string.Format("Scene[{0}]: \"{1}\"", i, scenes[i]));
        }

        //Debug.Log(string.Format("Creating Directory \"{0}\" if it does not exist", path));
        //(new FileInfo(path)).Directory.Create();

        Debug.Log(string.Format("Switching Build Target to {0}", "Android"));
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);

        Debug.Log("Starting Android Build!");
        BuildPipeline.BuildPlayer(scenes, EditorUserBuildSettings.GetBuildLocation(BuildTarget.Android), BuildTarget.Android, BuildOptions.None);
    }
}
