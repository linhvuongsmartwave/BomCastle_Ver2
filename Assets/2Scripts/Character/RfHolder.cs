using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CandyCoded.HapticFeedback;
using TMPro;

public class RfHolder : MonoBehaviour
{
    private TextMeshProUGUI txtBom;
    private TextMeshProUGUI txtGold;
    private TextMeshProUGUI txtHeart;
    private TextMeshProUGUI txtSpeed;
    private TextMeshProUGUI txtExplosion;

    private int gold;
    private Player player;
    private BomControl bomControl;

    private GameObject nomoney;
    private GameObject confirm1;
    private GameObject confirm2;
    private GameObject confirm3;

    private Button button1;
    private Button button2;
    private Button button3;
    private Image buttonImage1;
    private Image buttonImage2;
    private Image buttonImage3;

    public static RfHolder Instance;
    private void Awake()
    {
        Instance = this;

        nomoney = GameObject.Find("Nomoney");
        confirm1 = GameObject.Find("confirm1");
        confirm2 = GameObject.Find("confirm2");
        confirm3 = GameObject.Find("confirm3");

        button1 = GameObject.Find("BtnBom").GetComponent<Button>();
        button3 = GameObject.Find("BtnSpeed").GetComponent<Button>();
        button2 = GameObject.Find("BtnExplosion").GetComponent<Button>();

        txtBom = GameObject.Find("txtBom").GetComponent<TextMeshProUGUI>();
        txtGold = GameObject.Find("txtGold").GetComponent<TextMeshProUGUI>();
        txtHeart = GameObject.Find("txtHeart").GetComponent<TextMeshProUGUI>();
        txtSpeed = GameObject.Find("txtSpeed").GetComponent<TextMeshProUGUI>();
        txtExplosion = GameObject.Find("txtExplosion").GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        player = FindObjectOfType<Player>();
        if (player != null)
        {
            txtHeart.text = (player.currentHealth).ToString();
            txtSpeed.text = player.speedMove.ToString();
            txtExplosion.text = 1.ToString();
            txtBom.text = 1.ToString();
        }
        else print("khong tim thay player rfholder");
        bomControl = FindObjectOfType<BomControl>();
        if (bomControl == null) print("khong tim thay bomControl rfholder");
        UpdateGold();
        confirm1.SetActive(false);
        confirm2.SetActive(false);
        confirm3.SetActive(false);
        nomoney.SetActive(false);

        buttonImage1=button1.GetComponent<Image>();
        buttonImage2=button2.GetComponent<Image>();
        buttonImage3=button3.GetComponent<Image>();
    }

    private void UpdateGold()
    {
        gold = PlayerPrefs.GetInt("gold");
        txtGold.text = gold.ToString();
    }

    public void UpdateHeart()
    {
        txtHeart.text = (player.currentHealth).ToString();
    }

    public void UpdateSpeed()
    {
        txtSpeed.text = (player.speedMove).ToString();
    }

    public void UpdateExplosion()
    {
        txtExplosion.text=bomControl.radius.ToString();
    } 

    public void UpdateBomAmount()
    {
        txtBom.text = bomControl.bomRemaining.ToString();
    }

    public void BuyBom()
    {
        BtnClick();
        if (gold >= 200)
        {
            confirm1.SetActive(true);
            OpenPopup();
        }
        else
        {
            nomoney.SetActive(true);
            OpenPopup();
        }
    }

    public void BuyExplosion()
    {
        BtnClick();
        if (gold >= 200)
        {
            confirm2.SetActive(true);
            OpenPopup();
        }
        else
        {
            nomoney.SetActive(true);
            OpenPopup();
        }
    }

    public void BuySpeed()
    {
        BtnClick();
        if (gold >= 200)
        {
            confirm3.SetActive(true);
            OpenPopup();
        }
        else
        {
            nomoney.SetActive(true);
            OpenPopup();
        }
    }

    public void PutBom()
    {
        bomControl.PutBom();
    }

    public void SaveGold()
    {
        txtGold.text=gold.ToString();
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.Save();
    }

    public void BomAmount()
    {
        BtnClick();
        bomControl.bomRemaining += 1;
        gold -= 200;
        SaveGold();
        UpdateBomAmount();
        button1.interactable = false;
        buttonImage1.color = new Color(100,100,100);
    }

    public void Radius()
    {
        BtnClick();
        bomControl.radius += 1;
        gold -= 200;
        SaveGold();
        UpdateExplosion();
        button2.interactable = false;
    }

    public void Speed()
    {
        BtnClick();
        player.speedMove += 0.5f;
        gold -= 200;
        SaveGold();
        UpdateSpeed();
        button3.interactable = false;
    }

    public void BtnClick()
    {
        AudioManager.Instance.AudioButtonClick();
    }

    public void OpenPopup()
    {
        AudioManager.Instance.AudioOpen();
    }

    public void ShowIconPushBom()
    {
        bomControl.ShowIconPushBom();
    }

    public void HideIconPushBom()
    {
        bomControl.HideIconPushBom();
    }

    public void Vibrate()
    {
        HapticFeedback.LightFeedback();
    }

}