using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class PersonaClass : MonoBehaviour
{
    public InputField pname;
    public InputField pdesc;

    private int id;
    private string personaName;
    private string personaDesc;
    private List<SkillClass> personaSkills = new List<SkillClass>();

    public string PersonaName
    {
        get { return personaName; }
        set
        {
            personaName = value;
            pname.text = value;
        }
    }

    public string PersonaDesc
    {
        get { return personaDesc; }
        set
        {
            personaDesc = value;
            pdesc.text = value;
        }
    }

    public int PersonaID
    {
        get { return id; }
        set { id = value; }
    }

    public List<SkillClass> PersonaSkills
    {
        get { return personaSkills; }
        set { personaSkills = value; }
    }

    public void DestroyGO()
    {
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        //select
    }
}
