using UnityEngine;
using System.Collections.Generic;

public class PersonaManager : MonoBehaviour
{
    public GameObject PersonaPrefab;
    public GameObject viewContent;

    private static PersonaManager instance = null;
    public static PersonaManager Instance
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
            Debug.LogWarning("Persona Manager created while not running.");
        }
#endif
        if (instance != null)
        {
            return;
        }

        PersonaManager manager = FindObjectOfType<PersonaManager>();
        if (manager == null)
        {
            GameObject gameObject = new GameObject("PersonaManager", typeof(PersonaManager));
            manager = gameObject.GetComponent<PersonaManager>();
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
            Debug.LogWarning("Multiple Persona Managers in scene.");
            Destroy(this);
        }
    }

    private void Initialize()
    {
        DontDestroyOnLoad(this);
    }

    public void InitPersonaListFromJSONArray(List<JSONDataClass> jsonArray)
    {
        if (jsonArray.Count > 0) ClearUserPersonaList();
        foreach (JSONDataClass tempJson in jsonArray)
        {
            UserDataClass.UserPersonas.Add(CreatePersona(JSON_UtilityClass.ParseIntFromString(tempJson.id), tempJson.PersonaName, tempJson.PersonaDesc));
        }
    }

    public void RemovePersona(string pname)
    {
        foreach (PersonaClass tempPersona in UserDataClass.UserPersonas)
        {
            if (tempPersona.PersonaName.Equals(pname))
            {
                UserDataClass.UserPersonas.Remove(tempPersona);
                break;
            }
        }
    }

    public void ClearUserPersonaList()
    {
        foreach (PersonaClass tempPersona in UserDataClass.UserPersonas)
        {
            tempPersona.DestroyGO();
        }
        UserDataClass.UserPersonas.Clear();
    }

    public void SelectPersona(string pname)
    {
        foreach (PersonaClass tempPersona in UserDataClass.UserPersonas)
        {
            if (tempPersona.PersonaName.Equals(pname))
            {
                PersonaDataClass.id = tempPersona.PersonaID;
                PersonaDataClass.PersonaName = tempPersona.PersonaName;
                PersonaDataClass.PersonaDescription = tempPersona.PersonaDesc;
                StartCoroutine(NetworkManager.Instance.DownloadPersonaSkillListFromServer());
                break;
            }
        }
    }

    public void UpdatePersona(string pname, string pdesc)
    {
        foreach (PersonaClass tempPersona in UserDataClass.UserPersonas)
        {
            if (tempPersona.PersonaName.Equals(pname))
            {
                tempPersona.PersonaDesc = pdesc;
                break;
            }
        }
    }

    private PersonaClass CreatePersona(int pid, string pname, string pdesc)
    {
        GameObject temp = Instantiate(PersonaPrefab);
        temp.transform.SetParent(viewContent.transform);
        PersonaClass tempPersona = temp.GetComponent<PersonaClass>();
        tempPersona.PersonaID = pid;
        tempPersona.PersonaName = pname;
        tempPersona.PersonaDesc = pdesc;
        return tempPersona;
    }
}
