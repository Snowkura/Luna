using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int lv;
    public int maxHP;
    public int currentHP;
    public int maxMP;
    public int currentMP;
    public int maxEXP;
    public int currentEXP;
    public int ATK;
    public int DEF;
    public int SPE;
    public int dialogueInfoIndex;
    public bool canControlLuna;

    public bool hasPetTheDog;
    public int candleNum;
    public int monKillNum;

    public GameObject BattleUI;
    public Image NormalUI;

    public AudioSource audioSource;

    public AudioClip normalSceneBGM;
    public AudioClip battleSceneBGM;
    public AudioClip lunaAttackEffectSound;
    public AudioClip lunaAttackSound;
    public AudioClip lunaBeenHitSound;
    public AudioClip lunaDeadSound;
    public AudioClip lunaRecoverSound;
    public AudioClip finishTaskSound;
    public AudioClip drinkPosion;
    public AudioClip lunaDefendSound;
    public AudioClip enemyAttackSound;
    public AudioClip levelUpSound;

    public GameObject ESCPanel;
    public GameObject itemPanel;
    public GameObject candleAll;
    public GameObject monsterAll;
    public bool ESCPanelOpenOrNot;
    public bool itemPanelOpenOrNot;
    public bool battlePanelOpenOrNot;
    public bool helpTextOpenOrNot;


    private void Awake()
    {
        Instance = this;
        lv = 1;
        maxHP = 350;
        currentHP = 300;
        maxMP = 100;
        currentMP = 100;
        maxEXP = 100;
        currentEXP = 0;
        ATK = 80;
        DEF = 20;
        SPE = 20;

        canControlLuna = false;
        hasPetTheDog = false;
        candleNum = 0;
        monKillNum = 0;

        ESCPanelOpenOrNot = false;
        itemPanelOpenOrNot = false;
        battlePanelOpenOrNot=false;
        helpTextOpenOrNot = false;
    }

    private void Start()
    {
        UIManeger.Instance.SetHPBar((float)currentHP / (float)maxHP);
        UIManeger.Instance.SetMPBar((float)currentMP / (float)maxMP);
        UIManeger.Instance.SetEXPBar((float)currentEXP / (float)maxEXP);
        PlayMusic(normalSceneBGM);
    }
    private void Update()
    {
        if(GameManager.Instance.currentHP <= 30)
        {
            NormalUI.color = new Color(1,0,0,0.4f);
        }
        else
        {
            NormalUI.color = new Color(1, 0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !battlePanelOpenOrNot)
        {
            if (ESCPanelOpenOrNot)
            {
                ESCPanel.SetActive(false); 
                ESCPanelOpenOrNot = false;
            }
            else
            {
                ESCPanel.SetActive(true);
                ESCPanelOpenOrNot = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !battlePanelOpenOrNot && canControlLuna)
        {
            if (itemPanelOpenOrNot)
            {
                itemPanel.SetActive(false);
                itemPanelOpenOrNot = false;
            }
            else
            {
                itemPanel.SetActive(true);
                itemPanelOpenOrNot = true;
                UIManeger.Instance.ChangeToItem();
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (helpTextOpenOrNot)
            {
                UIManeger.Instance.helpText.SetActive(false);
                helpTextOpenOrNot = false;
            }
            else
            {
                UIManeger.Instance.helpText.SetActive(true);
                helpTextOpenOrNot = true;
            }
        }
    }

    public void ChangeHP(int amount)
    {
        currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);
        UIManeger.Instance.SetHPBar((float)currentHP / (float)maxHP);
    }
    public void ChangeMP(int amount)
    {
        currentMP = Mathf.Clamp(currentMP + amount, 0, maxMP);
        UIManeger.Instance.SetMPBar((float)currentMP / (float)maxMP);
    }
    public void ChangeEXP(int amount)
    {
        if ((currentEXP + amount) >= maxEXP)
        {
            currentEXP = (currentEXP + amount) - maxEXP;
            maxEXP = (int)(maxEXP * 1.2f);
            int lvBefore = lv; ;
            lv++;
            maxHP += 20;
            maxMP += 5;
            ATK += 5;
            DEF += 2;
            SPE += 1;
            ChangeHP(50000);
            ChangeMP(50000);
            StartCoroutine("ShowLevelUpForSeconds", lvBefore);
            PlaySound(levelUpSound);
        }
        else
        {
            currentEXP += amount;
        }
        UIManeger.Instance.SetEXPBar((float)currentEXP / (float)maxEXP);
    }

    IEnumerator ShowLevelUpForSeconds(int lvBefore)
    {
        UIManeger.Instance.SetLevelUpText($"Level Up!\nLv.{lvBefore}  ¡ú  Lv.{lv}");
        UIManeger.Instance.ShowOrHideLevelUpText(true);
        yield return new WaitForSeconds(2.5f);
        UIManeger.Instance.ShowOrHideLevelUpText(false);
    }

    public void LunaDead()
    {
        currentHP = Mathf.Clamp(10, 0, maxHP);
        UIManeger.Instance.SetHPBar((float)currentHP / (float)maxHP);
        currentMP = Mathf.Clamp(0, 0, maxMP);
        UIManeger.Instance.SetMPBar((float)currentMP / (float)maxMP);
    }

    public void EnterBattle(GameObject battleScene, bool enter = true)
    {
        if (enter)
        {
            GameManager.Instance.PlayMusic(battleSceneBGM);
        }
        else
        {
            GameManager.Instance.PlayMusic(normalSceneBGM);
        }
        battleScene.SetActive(enter);
        BattleUI.SetActive(enter);
        battlePanelOpenOrNot = enter;
    }
    public bool EnoughMPOrNot(int needMP)
    {
        return currentMP >= -needMP;
    }

    public void PlayMusic(AudioClip audio)
    {
        if (audioSource.clip != audio)
        {
            audioSource.clip = audio;
            audioSource.volume = 0.6f;
            audioSource.Play();
        }
    }    

    public void PlaySound(AudioClip audio)
    {
        if (audio)
        {            
            audioSource.PlayOneShot(audio);
        }
    }    

}

