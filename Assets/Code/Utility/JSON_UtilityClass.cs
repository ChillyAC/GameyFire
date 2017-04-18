using System;
using System.Collections.Generic;
using UnityEngine;

public class JSON_UtilityClass : MonoBehaviour
{
    public static JSONDataClass DecodeJSON(string jsonText)
    {
#if UNITY_EDITOR
        //Debug.Log(jsonText);
#endif
        return JsonUtility.FromJson<JSONDataClass>(jsonText);
    }

    public static List<JSONDataClass> DecodeJSONArray(string jsonText)
    {
#if UNITY_EDITOR
        //Debug.Log(jsonText);
#endif
        List<JSONDataClass> result = new List<JSONDataClass>();
        string[] jsonSplit = jsonText.Split(new string[] { "},{", "[", "]" }, StringSplitOptions.None);
        for (int i = 0; i < jsonSplit.Length; i++)
        {
            string temp = jsonSplit[i];

            if (!temp.StartsWith("{"))
            {
                temp = "{" + temp;
            }
            if (!temp.EndsWith("}"))
            {
                temp = temp + "}";
            }
            if (!temp.Contains("{ }") && !temp.Contains("{}"))
            {
                JSONDataClass tempJson = DecodeJSON(temp);
                result.Add(tempJson);
            }
        }
        if (result.Count > 0) return result;
        return null;
    }

    public static int ParseIntFromString(string s)
    {
        int retVal = -1;
#if UNITY_EDITOR
        //Debug.Log(int.TryParse(s, out retVal));
#endif
        bool result = int.TryParse(s, out retVal);
        return retVal;
    }
}
