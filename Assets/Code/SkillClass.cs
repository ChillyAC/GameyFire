using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

[Serializable]
public class SkillClass : MonoBehaviour
{
    public Text sname;
    public Text srating;

    private int id;
    private string skillName;
    private string skillRating;

    public string SkillName
    {
        get { return skillName; }
        set
        {
            skillName = value;
            sname.text = value;
        }
    }

    public string SkillRating
    {
        get { return skillRating; }
        set
        {
            skillRating = value;
            srating.text = value;
        }
    }

    public int SkillID
    {
        get { return id; }
        set { id = value; }
    }

    public void DestroyGO()
    {
        Destroy(gameObject);
    }
}
