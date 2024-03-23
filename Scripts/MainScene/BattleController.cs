using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public static BattleController Instance;
    public GameObject battleScene;//当前应该触发的战斗场景由被触碰到的怪物传参获得
    public GameObject enemyRealBody;//敌人的实体,由被触碰到的怪物传参获得

    private Animator lunaAnimator;
    private Transform lunaTransform;
    private Transform enemyTransform;
    private SpriteRenderer lunaSprite;//luna战斗场景的精灵组件，由当前战斗场景的子对象获得
    private SpriteRenderer enemySprite;//敌人战斗场景的精灵组件，由当前战斗场景的子对象获得
    private GameObject enemyObject;//敌人战斗场景的实体，由当前战斗场景的子对象获得    
    public GameObject lunaRealBody;//luna的实体
    private EnemyTypeBase enemy;//记录了所有敌人属性的父类
    public Image enemyHPBar;
    public Text enemyTakenDamage;
    public Text lunaRecoverHP;
    public float originalHPBarWidth;


    private Vector3 enemyOriginalPos;
    private Vector3 lunaOriginalPos;


    private void Awake()
    {
        Instance = this;       
    }
    public void InitializeBattle(GameObject battleScene, GameObject enemyRealBody)
    {
        this.battleScene = battleScene;
        this.enemyRealBody = enemyRealBody;
        lunaAnimator = battleScene.transform.GetChild(0).GetComponent<Animator>();
        lunaTransform = battleScene.transform.GetChild(0).GetComponent<Transform>();
        enemyTransform = battleScene.transform.GetChild(1).GetComponent<Transform>();
        lunaSprite = battleScene.transform.GetChild(0).GetComponent<SpriteRenderer>();
        enemySprite = battleScene.transform.GetChild(1).GetComponent<SpriteRenderer>();
        enemyObject = battleScene.transform.GetChild(1).gameObject;
        enemy = enemyRealBody.GetComponent<EnemyTypeBase>();
        enemyHPBar = enemyObject.transform.GetChild(0).GetChild(1).GetComponent<Image>();
        enemyTakenDamage = enemyObject.transform.GetChild(0).GetChild(2).GetComponent<Text>();
        lunaRecoverHP = battleScene.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        originalHPBarWidth = enemyHPBar.rectTransform.rect.width;
        enemyOriginalPos = enemyTransform.localPosition;
        lunaOriginalPos = lunaTransform.localPosition;
        lunaRealBody.SetActive(false);
        enemy.currentHP = enemy.maxHP;
        lunaTransform.localPosition = lunaOriginalPos;
        enemyTransform.localPosition = enemyOriginalPos;
        Debug.Log("怪物当前血量：" + enemy.currentHP);
    }

    public void LunaAttack()
    {
        StartCoroutine("LunaAttackLogic");
    }

    public void LunaDefend()
    {
        StartCoroutine("LunaDefendLogic");
    }
    public void LunaUseItem()
    {

    }
    public void LunaEscape()
    {
        StartCoroutine("LunaEscapeLogic");
    }
    public void LunaUseFlame()
    {
        Skill flame = LunaSkillList.Instance.flame;
        if (!GameManager.Instance.EnoughMPOrNot(flame.cost))
        {
            return;
        }
        StartCoroutine("LunaSkillLogic", flame);
    }
    public void LunaUseEarthquake()
    {
        Skill earthquake = LunaSkillList.Instance.earthquake;
        if (!GameManager.Instance.EnoughMPOrNot(earthquake.cost))
        {
            return;
        }
        StartCoroutine("LunaSpecialSkillLogic", earthquake);
    }
    public void LunaUseNormalHeal()
    {
        Skill heal = LunaSkillList.Instance.normalHeal;
        if (!GameManager.Instance.EnoughMPOrNot(heal.cost))
        {
            return;
        }
        StartCoroutine("LunaHealSkillLogic", heal);
    }
    
    IEnumerator LunaEscapeLogic()
    {
        UIManeger.Instance.ShowOrHideBattelPanel(false);
        lunaAnimator.SetBool("MoveState", true);
        lunaAnimator.SetFloat("MoveValue", 1);
        lunaTransform.DOLocalMove(lunaOriginalPos + new Vector3(3f, 0, 0), 0.7f).OnComplete(() =>
        {
            GameManager.Instance.EnterBattle(battleScene, false);
            lunaRealBody.SetActive(true);
            lunaTransform.DOLocalMove(lunaOriginalPos, 0.1f);
        });
        enemyRealBody.transform.position += new Vector3(0, 1, 0);
        yield return new WaitForSeconds(0.7f);
    }

    IEnumerator LunaAttackLogic()
    {
        UIManeger.Instance.ShowOrHideBattelPanel(false);
        lunaAnimator.SetBool("MoveState", true);
        lunaAnimator.SetFloat("MoveValue", -1);
        GameManager.Instance.PlaySound(GameManager.Instance.lunaAttackSound);
        lunaTransform.DOLocalMove(enemyOriginalPos + new Vector3(1.3f,0.2f,0), 0.5f).OnComplete(() =>
        {
            lunaAnimator.SetBool("MoveState", false);
            lunaAnimator.SetFloat("MoveValue", 0);
            lunaAnimator.CrossFade("Attack", 0);
            GameManager.Instance.PlaySound(GameManager.Instance.lunaAttackEffectSound);
            JudgeEnemyHP(GameManager.Instance.ATK - enemy.DEF);
            enemySprite.DOFade(0.35f, 0.5f).OnComplete(() => {
                enemySprite.DOFade(1f, 0.35f);                                
                Debug.Log("对敌人造成了" + (GameManager.Instance.ATK - enemy.DEF) + "点伤害！");
            }) ;
        });
        yield return new WaitForSeconds(1.2f);
        JudgeBattleEndOrNot();
        lunaAnimator.SetBool("MoveState", true);
        lunaAnimator.SetFloat("MoveValue", 1);
        lunaTransform.DOLocalMove(lunaOriginalPos, 0.5f).OnComplete(() => {lunaAnimator.SetBool("MoveState", false); });
        yield return new WaitForSeconds(0.8f);
        StartCoroutine("EnemyAttackLogic");
    }

    IEnumerator EnemyAttackLogic()
    {
        GameManager.Instance.PlaySound(GameManager.Instance.enemyAttackSound);
        enemyTransform.DOLocalMove(lunaOriginalPos - new Vector3(2.5f,0,0), 0.5f).OnComplete(() =>
        {
            enemyTransform.DOLocalMove(lunaOriginalPos - new Vector3(1.3f, 0, 0), 0.1f);
            lunaAnimator.CrossFade("Hit", 0);
            GameManager.Instance.PlaySound(GameManager.Instance.lunaBeenHitSound);
            lunaSprite.DOFade(0.5f, 0.2f).OnComplete(() =>
            {
                if(enemy.ATK > GameManager.Instance.DEF)
                {
                    JudgeLunaHP(enemy.ATK - GameManager.Instance.DEF);                   
                }
                else
                {
                    JudgeLunaHP(1);                    
                }
                
            });
        });
        yield return new WaitForSeconds(1.0f);
        JudgeBattleEndOrNot();
        enemyTransform.DOLocalMove(enemyOriginalPos, 0.5f);
        yield return new WaitForSeconds(0.5f);
        UIManeger.Instance.ShowOrHideBattelPanel(true);
    }

    IEnumerator LunaDefendLogic()
    {
        UIManeger.Instance.ShowOrHideBattelPanel(false);
        lunaAnimator.SetBool("Defend", true);
        GameManager.Instance.PlaySound(GameManager.Instance.enemyAttackSound);
        enemyTransform.DOLocalMove(lunaOriginalPos - new Vector3(2.5f, 0, 0), 0.5f).OnComplete(() =>
        {
            enemyTransform.DOLocalMove(lunaOriginalPos - new Vector3(1.3f, 0, 0), 0.2f).OnComplete(() => {
                GameManager.Instance.PlaySound(GameManager.Instance.lunaDefendSound);
                lunaTransform.DOLocalMove(lunaOriginalPos + new Vector3(0.5f,0,0), 0.2f).OnComplete(() =>
                {
                    lunaTransform.DOLocalMove(lunaOriginalPos, 0.1f);
                });
                lunaSprite.DOFade(0.5f, 0.2f).OnComplete(() =>
                {
                    if (enemy.ATK >= GameManager.Instance.DEF)
                    {
                        JudgeLunaHP((enemy.ATK - GameManager.Instance.DEF) /2);                        
                    }
                    else
                    {
                        JudgeLunaHP(1);                        
                    }
                });
            });           
        });
        yield return new WaitForSeconds(1.0f);
        JudgeBattleEndOrNot();
        enemyTransform.DOLocalMove(enemyOriginalPos, 0.5f);
        yield return new WaitForSeconds(0.1f);
        lunaAnimator.SetBool("Defend", false);
        yield return new WaitForSeconds(0.4f);
        UIManeger.Instance.ShowOrHideBattelPanel(true);

    }
    
    IEnumerator LunaSkillLogic(Skill skill)
    {
        UIManeger.Instance.CloseSkillPanel();
        GameManager.Instance.ChangeMP(skill.cost);
        UIManeger.Instance.ShowOrHideBattelPanel(false);
        GameManager.Instance.PlaySound(GameManager.Instance.lunaAttackSound);
        lunaAnimator.CrossFade(skill.animationName, 0);
        GameManager.Instance.PlaySound(skill.skillSound);
        yield return new WaitForSeconds(0.35f);
        GameObject go =Instantiate(skill.effect, enemyTransform);
        go.transform.localPosition = Vector3.zero;
        yield return new WaitForSeconds(skill.secUntilHit);//技能击中前等待时长
        JudgeEnemyHP((int)(skill.damagePercent * GameManager.Instance.ATK));
        enemySprite.DOFade(0.35f, 0.35f).OnComplete(() => {
            enemySprite.DOFade(1f, 0.35f);           
            Debug.Log("对敌人造成了" + (int)(skill.damagePercent * GameManager.Instance.ATK) + "点伤害！");
        });
        yield return new WaitForSeconds(skill.secTotal - skill.secUntilHit - 0.35f + 0.3f);//技能特效总时长 - 技能击中前等待时长 - 敌人闪烁时长 + 额外等待时长
        JudgeBattleEndOrNot();
        yield return null;
        StartCoroutine("EnemyAttackLogic");
    }

    IEnumerator LunaSpecialSkillLogic(Skill skill)
    {
        UIManeger.Instance.CloseSkillPanel();
        GameManager.Instance.ChangeMP(skill.cost);
        UIManeger.Instance.ShowOrHideBattelPanel(false);
        GameManager.Instance.PlaySound(GameManager.Instance.lunaAttackSound);
        lunaAnimator.CrossFade(skill.animationName, 0);
        GameManager.Instance.PlaySound(skill.skillSound);
        yield return new WaitForSeconds(0.35f);
        GameObject go = Instantiate(skill.effect, lunaTransform);
        go.transform.localPosition = new Vector3(-1.4f, -0.5f, 0);
        yield return new WaitForSeconds(skill.secUntilHit);//技能击中前等待时长
        JudgeEnemyHP((int)(skill.damagePercent * GameManager.Instance.ATK));
        enemySprite.DOFade(0.35f, 0.35f).OnComplete(() => {
            enemySprite.DOFade(1f, 0.35f);
            Debug.Log("对敌人造成了" + (int)(skill.damagePercent * GameManager.Instance.ATK) + "点伤害！");
        });
        yield return new WaitForSeconds(skill.secTotal - skill.secUntilHit - 0.35f + 0.3f);//技能特效总时长 - 技能击中前等待时长 - 敌人闪烁时长 + 额外等待时长
        JudgeBattleEndOrNot();
        yield return null;
        StartCoroutine("EnemyAttackLogic");
    }
    IEnumerator LunaHealSkillLogic(Skill skill)
    {
        UIManeger.Instance.CloseSkillPanel();
        GameManager.Instance.ChangeMP(skill.cost);
        UIManeger.Instance.ShowOrHideBattelPanel(false);
        GameManager.Instance.PlaySound(GameManager.Instance.lunaAttackSound);
        lunaAnimator.CrossFade(skill.animationName, 0);
        yield return new WaitForSeconds(1.5f);
        GameObject go = Instantiate(skill.effect, lunaTransform);
        go.transform.localPosition = Vector3.zero;
        GameManager.Instance.PlaySound(skill.skillSound);
        GameManager.Instance.ChangeHP((int)(skill.damagePercent * GameManager.Instance.ATK));
        lunaRecoverHP.text = "+" + ((int)(skill.damagePercent * GameManager.Instance.ATK)).ToString();
        lunaRecoverHP.DOFade(1f, 0.01f);
        yield return new WaitForSeconds(skill.secTotal + 0.3f);//技能特效总时长 - 技能击中前等待时长 - 敌人闪烁时长 + 额外等待时长
        lunaRecoverHP.DOFade(0f, 1f);
        StartCoroutine("EnemyAttackLogic");
    }

    private void JudgeEnemyHP(int value)
    {       
        enemy.ChangeHP(-value);
        enemyHPBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, (float)enemy.currentHP/(float)enemy.maxHP * originalHPBarWidth);
        enemyTakenDamage.text = "-" + value.ToString();
        enemyTakenDamage.DOFade(1f, 0.01f);
        new WaitForSeconds(2f);
        enemyTakenDamage.DOFade(0f, 2f);       
    }

    private void JudgeLunaHP(int value)
    {
        lunaSprite.DOFade(1f, 0.2f);
        GameManager.Instance.ChangeHP(-value);
    }

    private void JudgeBattleEndOrNot()
    {
        if (GameManager.Instance.currentHP == 0 || enemy.currentHP == 0)
        {
            if (GameManager.Instance.currentHP == 0)
            {
                GameManager.Instance.LunaDead();
                enemyRealBody.transform.position += new Vector3(0, 1, 0);
                GameManager.Instance.EnterBattle(battleScene, false);
                GameManager.Instance.PlaySound(GameManager.Instance.lunaDeadSound);
                lunaRealBody.SetActive(true);
                gameObject.SetActive(false);
            }
            else
            {
                if (enemyRealBody.CompareTag("MissionMon1"))
                {
                    GameManager.Instance.monKillNum++;
                    if (GameManager.Instance.monKillNum >= 5 && GameManager.Instance.dialogueInfoIndex == 6)
                    {
                        GameManager.Instance.dialogueInfoIndex++;
                    }
                    Destroy(enemyRealBody);
                }
                else
                {                    
                    Destroy(enemyRealBody);
                }
                GameManager.Instance.ChangeEXP(enemy.EXPGivenDynamic());
                GameManager.Instance.EnterBattle(battleScene, false);
                lunaRealBody.SetActive(true);
                gameObject.SetActive(false);
            }                        
        }
    }

    public void SetBCActive()
    {
        gameObject.SetActive(true);
    }
}
