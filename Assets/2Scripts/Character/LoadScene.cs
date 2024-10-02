using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public SceneFader sceneFader;
    public void LoadSceneGamePlay()
    {
        sceneFader.FadeTo("GamePlay");
    }
    public void LoadSceneMainMenu()
    {
        sceneFader.FadeTo("MainMenu");
    }
}
