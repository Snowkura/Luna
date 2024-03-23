using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManeger : MonoBehaviour
{
    public static UIManeger Instance;
    public Image HPBar;
    public Image MPBar;
    public Image EXPBar;
    public Text EXPText;
    public Text lunaStatText;
    public GameObject levelUpText;
    public Text FPS;
    public GameObject battlePanel;
    public GameObject talkPanel;
    public GameObject ESCPanel;
    public GameObject helpText;
    public GameObject battleSkillPanel;
    public Text contentText;
    public Text nameText;
    public Sprite[] characterSprite;
    public Image characterImage;
    public GameObject volumnSlider;
    public GameObject itemBox;
    public GameObject importantItemBox;

    private float originalSizeHP;
    private float originalSizeMP;
    private float originalSizeEXP;
    private float deltaTime = 0.0f;
    private bool volumnSliderShowOrNot;


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        originalSizeHP = HPBar.rectTransform.rect.width;
        originalSizeMP = MPBar.rectTransform.rect.width;
        originalSizeEXP = EXPBar.rectTransform.rect.width;        
    }
    private void Start()
    {
        EXPText.text = GameManager.Instance.currentEXP.ToString() + "/" + GameManager.Instance.maxEXP.ToString();
        lunaStatText.text = $"Lv.{GameManager.Instance.lv}\nHP: {GameManager.Instance.currentHP}/{GameManager.Instance.maxHP}\n" +
            $"MP: {GameManager.Instance.currentMP}/{GameManager.Instance.maxMP}\n" +
            $"ATK: {GameManager.Instance.ATK}\nDEF: {GameManager.Instance.DEF}\nSPE: {GameManager.Instance.SPE}";
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        // 计算FPS
        float fps = 1.0f / deltaTime;
        // 更新文本组件以显示FPS
        FPS.text = string.Format("{0:0.} fps", fps);
    }

    public void SetHPBar(float currentHPPercent)
    {
        HPBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentHPPercent * originalSizeHP);
        lunaStatText.text = $"Lv.{GameManager.Instance.lv}\nHP: {GameManager.Instance.currentHP}/{GameManager.Instance.maxHP}\n" +
            $"MP: {GameManager.Instance.currentMP}/{GameManager.Instance.maxMP}\n" +
            $"ATK: {GameManager.Instance.ATK}\nDEF: {GameManager.Instance.DEF}\nSPE: {GameManager.Instance.SPE}";
    }

    public void SetMPBar(float currentMPPercent)
    {
        MPBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentMPPercent * originalSizeMP);
        lunaStatText.text = $"Lv.{GameManager.Instance.lv}\nHP: {GameManager.Instance.currentHP}/{GameManager.Instance.maxHP}\n" +
            $"MP: {GameManager.Instance.currentMP}/{GameManager.Instance.maxMP}\n" +
            $"ATK: {GameManager.Instance.ATK}\nDEF: {GameManager.Instance.DEF}\nSPE: {GameManager.Instance.SPE}";
    }

    public void SetEXPBar(float currentEXPPercent)
    {
        EXPBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentEXPPercent * originalSizeEXP);
        EXPText.text = GameManager.Instance.currentEXP.ToString() + "/" + GameManager.Instance.maxEXP.ToString();
        lunaStatText.text = $"Lv.{GameManager.Instance.lv}\nHP: {GameManager.Instance.currentHP}/{GameManager.Instance.maxHP}\n" +
            $"MP: {GameManager.Instance.currentMP}/{GameManager.Instance.maxMP}\n" +
            $"ATK: {GameManager.Instance.ATK}\nDEF: {GameManager.Instance.DEF}\nSPE: {GameManager.Instance.SPE}";
    }   

    public void ShowOrHideBattelPanel(bool show)
    {
        battlePanel.SetActive(show);
    }

    public void ShowOrHideLevelUpText(bool show)
    {
        levelUpText.SetActive(show);
    }

    public void SetLevelUpText(string str)
    {
        levelUpText.GetComponent<Text>().text = str;
    }

    public void ShowDialogue(string content = null, string name = null)
    {
        if (content == null)
        {
            talkPanel.SetActive(false);
        }
        else
        {
            talkPanel.SetActive(true);
            if (name != null)
            {
                if (name == "ルナ")
                {
                    characterImage.sprite = characterSprite[0];
                }
                else
                {
                    characterImage.sprite = characterSprite[1];
                }
                characterImage.SetNativeSize();
            }
            contentText.text = content;
            nameText.text = name;
        }
    }

    public void AdjustVolumn()
    {
        AudioListener.volume = volumnSlider.GetComponent<Slider>().value; // 调整全局音量
    }

    public void ClickVolumnButton()
    {
        if (volumnSliderShowOrNot)
        {
            volumnSlider.SetActive(false);
            volumnSliderShowOrNot = false;
        }
        else
        {
            volumnSlider.SetActive(true);
            volumnSliderShowOrNot = true;
        }
    }

    public void ClickReturnButton()
    {
        ESCPanel.SetActive(false);
    }
    public void ClickBackToMainMenuButton()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void ClickExitButton()
    {
        Application.Quit();
    }
    public void CloseItemPanel()
    {
        GameManager.Instance.itemPanel.SetActive(false);
        GameManager.Instance.itemPanelOpenOrNot = false;
    }

    public void ChangeToImportantItem()
    {
        itemBox.SetActive(false);
        importantItemBox.SetActive(true);
    }
    public void ChangeToItem()
    {
        itemBox.SetActive(true);
        importantItemBox.SetActive(false);
    }

    public void OpenSkillPanel()
    {
        battleSkillPanel.SetActive(true);
        battlePanel.SetActive(false);
    }
    public void CloseSkillPanel()
    {
        battleSkillPanel.SetActive(false);
        battlePanel.SetActive(true);
    }
}
