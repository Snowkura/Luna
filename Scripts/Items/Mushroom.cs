using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private int amount = 50;
    public GameObject effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.currentMP < GameManager.Instance.maxMP)
            {
                Instantiate(effect, transform.position, Quaternion.identity);
                GameManager.Instance.ChangeMP(amount);
                GameManager.Instance.PlaySound(GameManager.Instance.drinkPosion);
                Destroy(gameObject);
            }
            else
            {
                Instantiate(effect, transform.position, Quaternion.identity);
                ItemManager.Instance.AddItemInBag(ItemManager.Instance.mushroom);
                GameManager.Instance.PlaySound(GameManager.Instance.finishTaskSound);
                Destroy(gameObject);
            }
        }
    }
}
