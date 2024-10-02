using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Loading : MonoBehaviour
{
    public SceneFader sceneFader;
    //public Image img;
    public float coolDown1 = 2f;


    void Start()
    {
        //img.fillAmount = 0;
        StartCoroutine(LoadSceneMainMenu());
    }

    //void Update()
    //{
    //    CoolDown();
    //}

    //void CoolDown()
    //{
    //    img.fillAmount += 1 / coolDown1 * Time.deltaTime;

    //}
    IEnumerator LoadSceneMainMenu()
    {
        yield return new WaitForSeconds(coolDown1);
        sceneFader.FadeTo("MainMenu");
    }
}
