using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    public GameObject effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            ItemManager.Instance.AddImportantItemInBag(ItemManager.Instance.candle);
            GameManager.Instance.PlaySound(GameManager.Instance.finishTaskSound);
            GameManager.Instance.candleNum++;
            if (GameManager.Instance.candleNum >= 5 && GameManager.Instance.dialogueInfoIndex ==4 )
            {
                GameManager.Instance.dialogueInfoIndex++;
            }
            Destroy(gameObject);
        }        
    }
}
