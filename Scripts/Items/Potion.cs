using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private int amount = 100;
    public GameObject effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.currentHP < GameManager.Instance.maxHP)
            {
                Instantiate(effect, transform.position, Quaternion.identity);
                GameManager.Instance.ChangeHP(amount);
                GameManager.Instance.PlaySound(GameManager.Instance.drinkPosion);
                Destroy(gameObject);
            }
            else
            {
                Instantiate(effect, transform.position, Quaternion.identity);
                ItemManager.Instance.AddItemInBag(ItemManager.Instance.smallHPPotion);
                GameManager.Instance.PlaySound(GameManager.Instance.finishTaskSound);
                Destroy(gameObject);
            }
        }         
    }
}
