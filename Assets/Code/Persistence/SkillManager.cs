using UnityEngine;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour
{
    public GameObject SkillPrefab;
    public GameObject viewContent;

    private static SkillManager instance = null;
    public static SkillManager Instance
    {
        get
        {
            EnsureExistence();
            return instance;
        }
    }

    private static void EnsureExistence()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            Debug.LogWarning("Skill Manager created while not running.");
        }
#endif
        if (instance != null)
        {
            return;
        }

        SkillManager manager = FindObjectOfType<SkillManager>();
        if (manager == null)
        {
            GameObject gameObject = new GameObject("SkillManager", typeof(SkillManager));
            manager = gameObject.GetComponent<SkillManager>();
        }
        instance = manager;
        manager.Initialize();
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            Initialize();
        }
        else if (instance != this)
        {
            Debug.LogWarning("Multiple Skill Managers in scene.");
            Destroy(this);
        }
    }

    private void Initialize()
    {
        DontDestroyOnLoad(this);
    }

    public void InitSkillListFromJSONArray(List<JSONDataClass> jsonArray)
    {
        if (jsonArray.Count > 0) ClearPersonaSkillList();
        foreach (JSONDataClass tempJson in jsonArray)
        {
            PersonaDataClass.PersonaSkills.Add(CreateSkill(JSON_UtilityClass.ParseIntFromString(tempJson.id), tempJson.SkillName, tempJson.SkillRating));
        }
    }

    public void RemoveSkill(string sname)
    {
        foreach (SkillClass tempSkill in PersonaDataClass.PersonaSkills)
        {
            if (tempSkill.SkillName.Equals(sname))
            {
                PersonaDataClass.PersonaSkills.Remove(tempSkill);
                break;
            }
        }
    }

    public void ClearPersonaSkillList()
    {
        foreach (SkillClass tempSkill in PersonaDataClass.PersonaSkills)
        {
            tempSkill.DestroyGO();
        }
        PersonaDataClass.PersonaSkills.Clear();
    }

    public void UpdateSkill(string sname, string srating)
    {
        foreach (SkillClass tempSkill in PersonaDataClass.PersonaSkills)
        {
            if (tempSkill.SkillName.Equals(sname))
            {
                tempSkill.SkillRating = srating;
                break;
            }
        }
    }

    private SkillClass CreateSkill(int sid, string sname, string srating)
    {
        GameObject temp = Instantiate(SkillPrefab);
        temp.transform.SetParent(viewContent.transform);
        SkillClass tempSkill = temp.GetComponent<SkillClass>();
        tempSkill.SkillID = sid;
        tempSkill.SkillName = sname;
        tempSkill.SkillRating = srating;
        return tempSkill;
    }
}
