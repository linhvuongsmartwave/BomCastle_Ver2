using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCharacter : MonoBehaviour
{
    public GameObject male;
    public GameObject feMale;
    public UiPanelDotween ui;
    public PickCharacter pickCharacter;
    bool isPick =true;

    private void Awake()
    {
        pickCharacter=GetComponent<PickCharacter>();
        male.SetActive(true);
        feMale.SetActive(true);
        Invoke("Wait", 0.5f);
    }

    void Start()
    {
        isPick = PlayerPrefs.GetInt("isPick",1)==1;
        int selectedCharacter = PlayerPrefs.GetInt("male", 0);
        if (isPick) pickCharacter.gameObject.SetActive(true);
        else
        {
            pickCharacter.gameObject.SetActive(false);
            if (selectedCharacter == 1)
            {
                male.SetActive(true);
                feMale.SetActive(false);
            }
            else if (selectedCharacter == 2)
            {
                male.SetActive(false);
                feMale.SetActive(true);
            }
        }
    }

    public void Male()
    {
        feMale.SetActive(false);
        ui.PanelFadeOut();
        isPick = false;
        PlayerPrefs.SetInt("isPick",0);
        PlayerPrefs.SetInt("male",1);

    }

    public void FeMale()
    {
        male.SetActive(false);
        ui.PanelFadeOut();
        isPick = false;
        PlayerPrefs.SetInt("isPick", 0);
        PlayerPrefs.SetInt("male", 2);

    }

    void Wait()
    {
        ui.PanelFadeIn();
    }
}
