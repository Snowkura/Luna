using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LunaController luna = collision.GetComponent<LunaController>();
            luna.Climb = true;
        }       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LunaController luna = collision.GetComponent<LunaController>();
            luna.Climb = true;
        }       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LunaController luna = collision.GetComponent<LunaController>();
            luna.Climb = false;
        }        
    }
}
