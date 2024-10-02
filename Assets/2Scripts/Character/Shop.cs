using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public int gold;
    public TextMeshProUGUI txtGold;
    public static Shop Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Load();
    }

    public void BuyGold(int gold)
    {
        AudioManager.Instance.AudioCoin();
        this.gold += gold;
        Save();
        UpdateGold();
    }

    public void Load()
    {
        gold = PlayerPrefs.GetInt("gold", gold);
        UpdateGold();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.Save();
    }

    public void UpdateGold()
    {
        txtGold.text = gold.ToString();
    }
}

