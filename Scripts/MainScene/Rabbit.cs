using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMoveRandom : MonoBehaviour
{
    public float moveSpeed;
    public float moveRadius;
    public float waitTime;
    public float maxMoveDuration;
    private Vector2 originalPosition;
    private Animator animator;
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
            originalPosition = transform.position;//���µ�ǰλ��
            // ����һ�����Ŀ��λ��
            Vector2 randomDirection = Random.insideUnitCircle.normalized * moveRadius;
            Vector2 targetPosition = originalPosition + randomDirection;
            Vector2 direction = (targetPosition - originalPosition).normalized; 

            float startTime = Time.time; // ��ʼ�ƶ���ʱ��

            // �ƶ���Ŀ��λ��
            while ((targetPosition - (Vector2)transform.position).magnitude > 0.001f)
            {
                if (Time.time - startTime > maxMoveDuration)
                {
                    // �����������ƶ�ʱ�䣬ֹͣ�����ƶ�
                    break;
                }
                animator.SetBool("IsMoving", true);
                SetLookDirection(direction, animator);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);                             
                yield return null;
            }

            animator.SetBool("IsMoving", false);
            // �ȴ�һ��ʱ��
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
}
