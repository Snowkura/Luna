using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpSpecial : MonoBehaviour
{
    public Transform target;
    private float jumpUpDistance;
    private float jumpUpTime;
    private float jumpTime = 0.8f;
    private Animator animator;
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            LunaController luna = collider.GetComponent<LunaController>();
            animator = luna.GetComponent<Animator>();
            luna.Jump = true;
            luna.GetComponent<Rigidbody2D>().simulated = false;
            Vector3 direction = (luna.transform.position - target.position).normalized;
            if (direction.y > 0.7f)
            {
                jumpUpDistance = 0.4f;
                jumpUpTime = 0.3f;
                animator.SetFloat("LookDirectionX", 0);
            }
            else if (direction.y < -0.7f)
            {
                jumpUpDistance = 2.3f;
                jumpUpTime = 0.5f;
                animator.SetFloat("LookDirectionX", 0);
            }
            else
            {
                jumpUpDistance = 1f;
                jumpUpTime = 0.28f;
                animator.SetFloat("LookDirectionY", 0);
            }
            luna.GetComponent<Transform>().DOMove(target.position, jumpTime).OnComplete(() => { EndJump(luna); });
            Sequence sequence = DOTween.Sequence();
            sequence.Append(luna.GetComponent<Transform>().DOMoveY(luna.GetComponent<Transform>().position.y + jumpUpDistance, jumpUpTime).SetEase(Ease.InOutSine));
            sequence.Append(luna.GetComponent<Transform>().DOMoveY(target.position.y, jumpTime - jumpUpTime).SetEase(Ease.InOutSine));
            StartCoroutine(DisableMovementTemporarily(luna));
        }        
    }

    private void EndJump(LunaController luna)
    {
        luna.GetComponent<Rigidbody2D>().simulated = true;
        luna.Jump = false;
    }

    IEnumerator DisableMovementTemporarily(LunaController luna)
    {
        GameManager.Instance.canControlLuna = false;
        yield return new WaitForSeconds(0.8f);
        GameManager.Instance.canControlLuna = true;
    }
}
