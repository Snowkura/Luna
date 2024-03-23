using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunaSkillList : MonoBehaviour
{
    public static LunaSkillList Instance;
    public GameObject flameEffect;
    public GameObject earthquakeEffect;
    public GameObject normalHealEffect;
    public Skill flame;
    public Skill earthquake;
    public Skill normalHeal;

    public AudioClip flameSound;
    public AudioClip earthquakeSound;
    public AudioClip normalHealSound;
    private void Awake()
    {
        Instance = this;        
        flame = new Skill("Skill", flameEffect, 1.2f, -20, 2f, 0.1f, flameSound);
        earthquake = new Skill("Skill", earthquakeEffect, 1.5f, -50, 4f, 1.2f, earthquakeSound);
        normalHeal = new Skill("RecoverHP", normalHealEffect, 1f, -40, 1.267f, 0.1f, normalHealSound);
    }    
}

public class Skill
{
    public string animationName;
    public GameObject effect;
    public float damagePercent;//�˺����ʣ������˺����ɹ���������һ������ֵ�õ�
    public int cost;
    public float secTotal;//������Ч��ʱ��
    public float secUntilHit;//���ܻ���ǰʱ��
    public AudioClip skillSound;

    /// <summary>
    /// �����캯���������ֱ���Luna���Ŷ��������֣�������Ч���˺�ֵ�����ģ�������Ч��ʱ���� ���ܻ���ǰʱ��
    /// </summary>
    /// <param name="animationName"></param>
    /// <param name="effect"></param>
    /// <param name="damage"></param>
    /// <param name="cost"></param>
    /// <param name="secTotal"></param>
    /// <param name="secUntilHit"></param>
    public Skill(string animationName, GameObject effect, float damagePercent, int cost, float secTotal, float secUntilHit, AudioClip sound)
    {
        this.animationName = animationName;
        this.effect = effect;
        this.damagePercent = damagePercent;
        this.cost = cost;
        this.secTotal = secTotal;
        this.secUntilHit = secUntilHit;
        this.skillSound = sound;
    }

}
