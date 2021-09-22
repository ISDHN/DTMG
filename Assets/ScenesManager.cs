using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void EnterWelcomeScene()
    {
        SceneManager.LoadScene("Welcome", LoadSceneMode.Single);
        
    }
    public void EnterPlayScene()
    {
        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }
}
