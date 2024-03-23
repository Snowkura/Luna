using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeBase : MonoBehaviour
{
    public int maxHP;
    public int currentHP;
    public int ATK;
    public int DEF;
    public int SPE;

    public int EXPGiven;
    public float EXPRatio;

    //public AudioSource enemyAttackSound;
    //public AudioSource enemyBeenHitSound;

    public void ChangeHP(int value)
    {
        currentHP = Mathf.Clamp(currentHP + value, 0, maxHP) ;
    }

    public int EXPGivenDynamic()
    {
        return (int)(EXPGiven * EXPRatio);
    }
}
