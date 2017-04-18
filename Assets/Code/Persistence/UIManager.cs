using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public InputField _inputFirstName;
    public InputField _inputLastName;
    public InputField _inputMail;
    public InputField _inputUser;
    public InputField _inputPass;

    public Button _userCreateBtn;
    public Button _userLoginBtn;
    public Button _ExitBtn;

    public Text _SuccessMsg;
    
    private static UIManager instance = null;
    public static UIManager Instance
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
            Debug.LogWarning("UI Manager created while not running.");
        }
#endif
        if (instance != null)
        {
            return;
        }

        UIManager manager = FindObjectOfType<UIManager>();
        if (manager == null)
        {
            GameObject gameObject = new GameObject("UIManager", typeof(UIManager));
            manager = gameObject.GetComponent<UIManager>();
        }
        instance = manager;
        manager.Initialize();
    }

    private void Start()
    {
        _userCreateBtn.onClick.AddListener(() => UserCreateButton());
        _userLoginBtn.onClick.AddListener(() => UserLoginButton());
        _ExitBtn.onClick.AddListener(() => ExitApplication());

        if (instance == null)
        {
            instance = this;
            Initialize();
        }
        else if (instance != this)
        {
            Debug.LogWarning("Multiple UI Managers in scene.");
            Destroy(this);
        }
    }

    private void Initialize()
    {
        DontDestroyOnLoad(this);
        Show_Buttons();
    }

    #region buttons
    private void UserCreateButton()
    {
        if (!_inputFirstName.gameObject.activeSelf && !_inputLastName.gameObject.activeSelf && !_inputMail.gameObject.activeSelf && !_inputUser.gameObject.activeSelf && !_inputPass.gameObject.activeSelf) Show_InputCreateUser();
        else
        {
            CreateUser();
            Hide_Input();
            StartCoroutine(Show_SuccessMsg());
        }
    }

    private void UserLoginButton()
    {
        if (!_inputUser.gameObject.activeSelf && !_inputPass.gameObject.activeSelf) Show_InputLoginUser();
        else
        {
            LoginUser();
            Hide_Input();
            StartCoroutine(Show_SuccessMsg());
            Hide_Buttons();
        }
    }

    private void ExitApplication()
    {
#if !UNITY_EDITOR
        Application.Quit();
#else 
        Debug.Log("Exit");
#endif
    }
    #endregion

    #region ui drawing
    private void Show_Buttons()
    {
        if (!_userCreateBtn.gameObject.activeSelf) _userCreateBtn.gameObject.SetActive(true);
        if (!_userLoginBtn.gameObject.activeSelf) _userLoginBtn.gameObject.SetActive(true);
        if (!_ExitBtn.gameObject.activeSelf) _ExitBtn.gameObject.SetActive(true);
    }

    private void Hide_Buttons()
    {
        if (_userCreateBtn.gameObject.activeSelf) _userCreateBtn.gameObject.SetActive(false);
        if (_userLoginBtn.gameObject.activeSelf) _userLoginBtn.gameObject.SetActive(false);
        if (_ExitBtn.gameObject.activeSelf) _ExitBtn.gameObject.SetActive(false);
    }

    private void Show_InputCreateUser()
    {
        if (!_inputFirstName.gameObject.activeSelf) _inputFirstName.gameObject.SetActive(true);
        if (!_inputLastName.gameObject.activeSelf) _inputLastName.gameObject.SetActive(true);
        if (!_inputMail.gameObject.activeSelf) _inputMail.gameObject.SetActive(true);
        if (!_inputUser.gameObject.activeSelf) _inputUser.gameObject.SetActive(true);
        if (!_inputPass.gameObject.activeSelf) _inputPass.gameObject.SetActive(true);
    }

    private void Show_InputLoginUser()
    {
        if (!_inputUser.gameObject.activeSelf) _inputUser.gameObject.SetActive(true);
        if (!_inputPass.gameObject.activeSelf) _inputPass.gameObject.SetActive(true);
    }

    private void Hide_Input()
    {
        if (_inputFirstName.gameObject.activeSelf) _inputFirstName.gameObject.SetActive(false);
        if (_inputLastName.gameObject.activeSelf) _inputLastName.gameObject.SetActive(false);
        if (_inputMail.gameObject.activeSelf) _inputMail.gameObject.SetActive(false);
        if (_inputUser.gameObject.activeSelf) _inputUser.gameObject.SetActive(false);
        if (_inputPass.gameObject.activeSelf) _inputPass.gameObject.SetActive(false);
    }

    private IEnumerator Show_SuccessMsg()
    {
        if (!_SuccessMsg.gameObject.activeSelf)
        {
            _SuccessMsg.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }

        if (_SuccessMsg.gameObject.activeSelf)
        {
            _SuccessMsg.gameObject.SetActive(false);
        }
    }
    #endregion

    #region create user
    private void CreateUser()
    {
        if (_inputFirstName.text.Length > 0 && _inputLastName.text.Length > 0 && _inputUser.text.Length > 0 && _inputMail.text.Length > 0 && _inputPass.text.Length > 0 && VerifyUserInput(_inputFirstName.text, _inputLastName.text, _inputUser.text, _inputMail.text, _inputPass.text))
        {
            StartCoroutine(NetworkManager.Instance.CreateMember(_inputFirstName.text, _inputLastName.text, _inputUser.text, _inputMail.text, _inputPass.text));
            Debug.Log("Created user - proceed to log in");
        }
    }

    private bool VerifyUserInput(string fname, string lname, string uname, string mail, string pass)
    {
        bool tempBool = false;

        int capsCount = 0;
        char[] tempCharArray = fname.ToCharArray();
        for (int i = 0; i < tempCharArray.Length; i++)
        {
            if (char.IsUpper(tempCharArray[i]))
                capsCount++;
            if (!char.IsLetter(tempCharArray[i]))
                return false;
            if (capsCount == 1 && i == 0)
                tempBool = true;
            if (capsCount > 1)
            {
                tempBool = false;
                break;
            }
        }
        if (tempBool == false) return false;

        capsCount = 0;
        tempCharArray = lname.ToCharArray();
        for (int i = 0; i < tempCharArray.Length; i++)
        {
            if (char.IsUpper(tempCharArray[i]))
                capsCount++;
            if (!char.IsLetter(tempCharArray[i]))
                return false;
            if (capsCount == 1 && i == 0)
                tempBool = true;
            if (capsCount > 1)
            {
                tempBool = false;
                break;
            }
        }
        if (tempBool == false) return false;

        int numCount = 0;
        capsCount = 0;
        tempCharArray = uname.ToCharArray();
        for (int i = 0; i < tempCharArray.Length; i++)
        {
            if (char.IsUpper(tempCharArray[i]))
                capsCount++;
            if (!char.IsLetterOrDigit(tempCharArray[i]))
                numCount++;
            if (capsCount > 0)
            {
                tempBool = false;
                break;
            }
            if (numCount > 0)
            {
                tempBool = false;
                break;
            }
        }
        if (tempBool == false) return false;

        string[] tempStringArray = mail.Split('@');
        if (tempStringArray.Length > 2) return false;
        else
        {
            numCount = 0;
            string[] tempStringArray2 = tempStringArray[0].Split('.');
            for (int j = 0; j < tempStringArray2.Length; j++)
            {
                tempCharArray = tempStringArray2[j].ToCharArray();
                for (int i = 0; i < tempCharArray.Length; i++)
                {
                    if (!char.IsLetterOrDigit(tempCharArray[i]))
                        numCount++;
                    if (numCount > 0)
                    {
                        tempBool = false;
                        break;
                    }
                }
                if (tempBool == false) return false;
            }
            numCount = 0;
            tempStringArray2 = tempStringArray[1].Split('.');
            if (tempStringArray2.Length < 2) return false;
            for (int j = 0; j < tempStringArray2.Length; j++)
            {
                tempCharArray = tempStringArray2[j].ToCharArray();
                for (int i = 0; i < tempCharArray.Length; i++)
                {
                    if (!char.IsLetterOrDigit(tempCharArray[i]))
                        numCount++;
                    if (numCount > 0)
                    {
                        tempBool = false;
                        break;
                    }
                }
                if (tempBool == false) return false;
            }
        }

        numCount = 0;
        tempCharArray = pass.ToCharArray();
        if (tempCharArray.Length < 8) return false;
        for (int i = 0; i < tempCharArray.Length; i++)
        {
            if (!char.IsLetterOrDigit(tempCharArray[i]))
                numCount++;
            if (numCount > 0)
            {
                tempBool = false;
                break;
            }
        }
        if (tempBool == false) return false;

        return true;
    }
    #endregion

    #region login user
    private void LoginUser()
    {
        if (_inputUser.text.Length > 0 && _inputPass.text.Length > 0)
        {
            StartCoroutine(NetworkManager.Instance.LoginToServer(_inputUser.text, _inputPass.text));
            Debug.Log("Log in success - move on");
        }
    }
    #endregion
}
