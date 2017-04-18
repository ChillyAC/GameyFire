using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class UnityBuild_HelperMethods
{
    public static string[] GetBuildScenes()
    {
        List<string> names = new List<string>();

        foreach (EditorBuildSettingsScene e in EditorBuildSettings.scenes)
        {
            if (e == null)
                continue;

            if (e.enabled)
                names.Add(e.path);
        }
        return names.ToArray();
    }
}
