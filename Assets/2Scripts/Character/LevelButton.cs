using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using TMPro;
public class LevelButton : MonoBehaviour
{
    [SerializeField] private Image buttonImg;
    [SerializeField] private Sprite currentButton;
    [SerializeField] private Sprite lockedButton;
    [SerializeField] private TextMeshProUGUI txtNumberLevel;

    public int numLevel;
    public int nextLevel;
    private Button button;
    private bool canClick = true;
    public SceneFader sceneFader;
    public static LevelButton Instance;

    private void Awake()
    {
        Assert.IsNotNull(currentButton);
        Assert.IsNotNull(lockedButton);
        Assert.IsNotNull(buttonImg);
        Assert.IsNotNull(txtNumberLevel);
        Instance = this;
        txtNumberLevel.text = (numLevel+1).ToString();
        int nb =PlayerPrefs.GetInt("CompletedLevel");
        nextLevel = nb;
    }

    private void Start()
    {
        button = GetComponent<Button>();
        if (numLevel == nextLevel) buttonImg.sprite = currentButton;

        else if (numLevel < nextLevel) buttonImg.sprite = currentButton;

        else
        {
            buttonImg.sprite = lockedButton;
            txtNumberLevel.gameObject.SetActive(false);
            canClick = false;
        }
    }

    public void OnButtonClick()
    {
        AudioManager.Instance.AudioButtonClick();
        if (canClick)
        {
            PlayerPrefs.SetInt("SelectedLevel", numLevel);
            PlayerPrefs.Save();
            sceneFader.FadeTo("GamePlay");
        }
    }
}
