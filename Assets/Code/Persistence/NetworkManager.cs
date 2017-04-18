using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour
{    
    private static NetworkManager instance = null;
    public static NetworkManager Instance
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
            Debug.LogWarning("Network Manager created while not running.");
        }
#endif
        if (instance != null)
        {
            return;
        }

        NetworkManager manager = FindObjectOfType<NetworkManager>();
        if (manager == null)
        {
            GameObject gameObject = new GameObject("NetworkManager", typeof(NetworkManager));
            manager = gameObject.GetComponent<NetworkManager>();
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
            Debug.LogWarning("Multiple Network Managers in scene.");
            Destroy(this);
        }
    }

    private void Initialize()
    {
        DontDestroyOnLoad(this);
    }

    public IEnumerator CreateMember(string fname, string lname, string user, string mail, string pass)
    {
        WWWForm formData = new WWWForm();
        formData.AddField("QueryType", "createuser");
        formData.AddField("FirstName", fname);
        formData.AddField("LastName", lname);
        formData.AddField("Username", user);
        formData.AddField("Mail", mail);
        formData.AddField("Password", pass);
        UnityWebRequest www = UnityWebRequest.Post("http://avcmediasystems.ro/test/gameyfire/GameyFire_ServerService.php", formData);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.Send();
        if (!www.isError)
        {
            if (www.downloadHandler.text.Contains("Success"))
            {
                Debug.Log("Success");
            }
        }
    }

    public IEnumerator RemoveMember(string user, string mail, string pass)
    {
        WWWForm formData = new WWWForm();
        formData.AddField("QueryType", "removeuser");
        formData.AddField("Username", user);
        formData.AddField("Mail", mail);
        formData.AddField("Password", pass);
        UnityWebRequest www = UnityWebRequest.Post("http://avcmediasystems.ro/test/gameyfire/GameyFire_ServerService.php", formData);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.Send();
        if (!www.isError)
        {
            if (www.downloadHandler.text.Contains("Success"))
            {
                Debug.Log("Success");
            }
        }
    }

    public IEnumerator CreatePersona(string pname, string pdesc)
    {
        WWWForm formData = new WWWForm();
        formData.AddField("QueryType", "createpersona");
        formData.AddField("UserID", UserDataClass.UserID);
        formData.AddField("PersonaName", pname);
        formData.AddField("PersonaDesc", pdesc);
        UnityWebRequest www = UnityWebRequest.Post("http://avcmediasystems.ro/test/gameyfire/GameyFire_ServerService.php", formData);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.Send();
        if (!www.isError)
        {
            if (www.downloadHandler.text.Contains("Success"))
            {
                Debug.Log("Success");
                StartCoroutine(DownloadPersonaListFromServer());
            }
        }
    }

    public IEnumerator UpdatePersona(string pname, string pdesc)
    {
        WWWForm formData = new WWWForm();
        formData.AddField("QueryType", "updatepersona");
        formData.AddField("id", PersonaDataClass.id);
        formData.AddField("PersonaName", pname);
        formData.AddField("PersonaDesc", pdesc);
        UnityWebRequest www = UnityWebRequest.Post("http://avcmediasystems.ro/test/gameyfire/GameyFire_ServerService.php", formData);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.Send();
        if (!www.isError)
        {
            if (www.downloadHandler.text.Contains("Success"))
            {
                Debug.Log("Success");
                StartCoroutine(DownloadPersonaListFromServer());
            }
        }
    }

    public IEnumerator RemovePersona(string pname)
    {
        WWWForm formData = new WWWForm();
        formData.AddField("QueryType", "removepersona");
        formData.AddField("UserID", UserDataClass.UserID);
        formData.AddField("PersonaName", pname);
        UnityWebRequest www = UnityWebRequest.Post("http://avcmediasystems.ro/test/gameyfire/GameyFire_ServerService.php", formData);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.Send();
        if (!www.isError)
        {
            if (www.downloadHandler.text.Contains("Success"))
            {
                Debug.Log("Success");
                StartCoroutine(DownloadPersonaListFromServer());
            }
        }
    }

    public IEnumerator CreateSkill(string sname, string srating)
    {
        WWWForm formData = new WWWForm();
        formData.AddField("QueryType", "createskill");
        formData.AddField("UserID", UserDataClass.UserID);
        formData.AddField("SkillName", sname);
        formData.AddField("SkillRating", srating);
        UnityWebRequest www = UnityWebRequest.Post("http://avcmediasystems.ro/test/gameyfire/GameyFire_ServerService.php", formData);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.Send();
        if (!www.isError)
        {
            if (www.downloadHandler.text.Contains("Success"))
            {
                Debug.Log("Success");
                StartCoroutine(DownloadPersonaSkillListFromServer());
            }
        }
    }

    public IEnumerator UpdateSkill(string sname, string srating)
    {
        WWWForm formData = new WWWForm();
        formData.AddField("QueryType", "updateskill");
        formData.AddField("UserID", UserDataClass.UserID);
        formData.AddField("SkillName", sname);
        formData.AddField("SkillRating", srating);
        UnityWebRequest www = UnityWebRequest.Post("http://avcmediasystems.ro/test/gameyfire/GameyFire_ServerService.php", formData);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.Send();
        if (!www.isError)
        {
            if (www.downloadHandler.text.Contains("Success"))
            {
                Debug.Log("Success");
                StartCoroutine(DownloadPersonaSkillListFromServer());
            }
        }
    }

    public IEnumerator RemoveSkill(string sname)
    {
        WWWForm formData = new WWWForm();
        formData.AddField("QueryType", "removeskill");
        formData.AddField("UserID", UserDataClass.UserID);
        formData.AddField("SkillName", sname);
        UnityWebRequest www = UnityWebRequest.Post("http://avcmediasystems.ro/test/gameyfire/GameyFire_ServerService.php", formData);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.Send();
        if (!www.isError)
        {
            if (www.downloadHandler.text.Contains("Success"))
            {
                Debug.Log("Success");
                StartCoroutine(DownloadPersonaSkillListFromServer());
            }
        }
    }
    
    public IEnumerator LoginToServer(string username, string password)
    {
        WWWForm formData = new WWWForm();
        formData.AddField("QueryType", "loginuser");
        formData.AddField("Username", username);
        formData.AddField("Password", password);
        UnityWebRequest www = UnityWebRequest.Post("http://avcmediasystems.ro/test/gameyfire/GameyFire_ServerService.php", formData);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.Send();
        if (!www.isError)
        {
            JSONDataClass jsonTxt = JSON_UtilityClass.DecodeJSON(www.downloadHandler.text);
            UserDataClass.UserID = JSON_UtilityClass.ParseIntFromString(jsonTxt.id);
            UserDataClass.FirstName = jsonTxt.FirstName;
            UserDataClass.LastName = jsonTxt.LastName;
            UserDataClass.Username = jsonTxt.Username;
            UserDataClass.Usermail = jsonTxt.Mail;
            UserDataClass.Password = jsonTxt.Password;

            //StartCoroutine(DownloadPersonaListFromServer());
        }
    }

    private IEnumerator DownloadPersonaListFromServer()
    {
        WWWForm formData = new WWWForm();
        formData.AddField("QueryType", "getpersonadata");
        formData.AddField("UserID", UserDataClass.UserID);
        UnityWebRequest www = UnityWebRequest.Post("http://avcmediasystems.ro/test/gameyfire/GameyFire_ServerService.php", formData);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.Send();
        if (!www.isError)
        {
            PersonaManager.Instance.InitPersonaListFromJSONArray(JSON_UtilityClass.DecodeJSONArray(www.downloadHandler.text));
        }
    }

    public IEnumerator DownloadPersonaSkillListFromServer()
    {
        WWWForm formData = new WWWForm();
        formData.AddField("QueryType", "getskilldata");
        formData.AddField("UserID", UserDataClass.UserID);
        formData.AddField("id", PersonaDataClass.id);
        UnityWebRequest www = UnityWebRequest.Post("http://avcmediasystems.ro/test/gameyfire/GameyFire_ServerService.php", formData);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.Send();
        if (!www.isError)
        {
            SkillManager.Instance.InitSkillListFromJSONArray(JSON_UtilityClass.DecodeJSONArray(www.downloadHandler.text));
        }
    }
}
