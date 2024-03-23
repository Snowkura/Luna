using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Image startButtom;
    public AudioClip startGameSound;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }    

    public void StartGame()
    {
        StartCoroutine("PlayStartSound");
    }
    IEnumerator PlayStartSound()
    {
        PlaySound(startGameSound);
        yield return new WaitForSeconds(2.3f);
        SceneManager.LoadScene("MainScene");
    }
    public void PlaySound(AudioClip audio)
    {
        if (audio)
        {
            audioSource.PlayOneShot(audio);
        }
    }
}
