using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerTrigger : MonoBehaviour
{
    public SpriteRenderer layer1;
    public SpriteRenderer layer2;
    private int counter = 0;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        counter++;
        if(counter == 1)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                layer1.sortingOrder = 10;
                layer2.sortingOrder = 10;
            }

        }
        else if (counter == 2)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                layer1.sortingOrder = 0;
                layer2.sortingOrder = 0;
                counter = 0;
            }
        }
    }
}
