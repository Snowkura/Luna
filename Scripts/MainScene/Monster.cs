using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : EnemyTypeBase
{    
    public float moveSpeed;
    public float moveRadius;
    public float waitTime;
    public float maxMoveDuration;
    public GameObject battleScene;
    private Vector2 originalPosition;
    private Animator animator;

    void Awake()
    {
        maxHP = 200;
        currentHP = maxHP;
        ATK = 70;
        DEF = 10;
        SPE = 10;
        EXPGiven = 80;
        EXPRatio = 1f;
    }
    // Start is called before the first frame update
    void Start()
    {       
        waitTime = 2;
        maxMoveDuration = 1.2f;
        moveRadius = 1.5f;
        moveSpeed = 1.5f;
        animator = GetComponent<Animator>();
        originalPosition = transform.position;
        StartCoroutine(MoveRandomly());
    }   

    // Update is called once per frame
    IEnumerator MoveRandomly()
    {
        while (true)
        {
            
                originalPosition = transform.position;//更新当前位置
                                                      // 生成一个随机目标位置
                Vector2 randomDirection = Random.insideUnitCircle.normalized * moveRadius;
                Vector2 targetPosition = originalPosition + randomDirection;
                Vector2 direction = (targetPosition - originalPosition).normalized;

                float startTime = Time.time; // 开始移动的时间

                // 移动到目标位置
                while ((targetPosition - (Vector2)transform.position).magnitude > 0.001f)
                {
                    if (Time.time - startTime > maxMoveDuration)
                    {
                        // 如果超过最大移动时间，停止尝试移动
                        break;
                    }
                    animator.SetBool("IsMoving", true);
                    SetLookDirection(direction, animator);
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                    yield return null;
                }

                animator.SetBool("IsMoving", false);
                // 等待一段时间
                yield return new WaitForSeconds(waitTime);
                      
        }
    }

    public void SetLookDirection(Vector2 vector, Animator animator)
    {
        if (vector.x <= -0.95)
        {
            animator.SetFloat("LookX", -1);
            animator.SetFloat("LookY", 0);
        }
        else if (vector.x > -0.95 && vector.x <= -0.1 && vector.y > 0)
        {
            animator.SetFloat("LookX", -1);
            animator.SetFloat("LookY", 1);
        }
        else if (vector.x > -0.95 && vector.x <= -0.1 && vector.y < 0)
        {
            animator.SetFloat("LookX", -1);
            animator.SetFloat("LookY", -1);
        }
        else if (vector.x > -0.1 && vector.x <= 0.1 && vector.y > 0)
        {
            animator.SetFloat("LookX", 0);
            animator.SetFloat("LookY", 1);
        }
        else if (vector.x > -0.1 && vector.x <= 0.1 && vector.y < 0)
        {
            animator.SetFloat("LookX", 0);
            animator.SetFloat("LookY", -1);
        }
        else if (vector.x > 0.1 && vector.x <= 0.95 && vector.y > 0)
        {
            animator.SetFloat("LookX", 1);
            animator.SetFloat("LookY", 1);
        }
        else if (vector.x > 0.1 && vector.x <= 0.95 && vector.y < 0)
        {
            animator.SetFloat("LookX", 1);
            animator.SetFloat("LookY", -1);
        }
        else
        {
            animator.SetFloat("LookX", 1);
            animator.SetFloat("LookY", 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BattleController.Instance.SetBCActive();
            BattleController.Instance.InitializeBattle(this.battleScene, gameObject);
            GameManager.Instance.EnterBattle(battleScene);
        }
    }    
}
