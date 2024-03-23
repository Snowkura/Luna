using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    private Animator animator;
    public GameObject effect;
    public AudioSource barkSound;
    //public AudioClip bark;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        barkSound.Play();
    }
    
    public void BeHappy()
    {
        animator.CrossFade("Comforted", 0);
        barkSound.Stop();
        GameManager.Instance.hasPetTheDog = true;
        GameManager.Instance.dialogueInfoIndex++;
        Destroy(effect);
        Invoke("SetCanControlLuna", 1.75f);
    }

    public void SetCanControlLuna()
    {
        GameManager.Instance.canControlLuna = true;
    }
}
