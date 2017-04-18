using System.IO;
using UnityEditor;
using UnityEngine;

public class UnityBuild_Windows64
{
    [MenuItem("Perform Build/Win64 Command Line Build")]
    static void CommandLineBuildWin64()
    {
        Debug.Log("Command line build Win64 version\n------------------\n------------------");

        string[] scenes = UnityBuild_HelperMethods.GetBuildScenes();
        string path = "D:/Unity Projects/Publishing files/AutomatedUnityBuilds/GameyFire.exe";
        EditorUserBuildSettings.SetBuildLocation(BuildTarget.StandaloneWindows64, path);
        if (scenes == null || scenes.Length == 0 || path == null)
            return;

        Debug.Log(string.Format("Path: \"{0}\"", path));
        for (int i = 0; i < scenes.Length; ++i)
        {
            Debug.Log(string.Format("Scene[{0}]: \"{1}\"", i, scenes[i]));
        }

        //Debug.Log(string.Format("Creating Directory \"{0}\" if it does not exist", path));
        //(new FileInfo(path)).Directory.Create();

        Debug.Log(string.Format("Switching Build Target to {0}", "Win64"));
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneWindows64);

        Debug.Log("Starting Win64 Build!");
        BuildPipeline.BuildPlayer(scenes, EditorUserBuildSettings.GetBuildLocation(BuildTarget.StandaloneWindows64), BuildTarget.StandaloneWindows64, BuildOptions.None);
    }
}
