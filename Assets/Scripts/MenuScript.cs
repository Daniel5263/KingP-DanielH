using System.Collections;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gotoGame()
    {
        StartCoroutine(WaitForSoundAndTransition("MainGame"));
        //AudioSource source = GetComponentInChildren<AudioSource>();
        //source.Play();
        //UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

    private IEnumerator WaitForSoundAndTransition(string MainGame)
    {
        AudioSource audioSource = GetComponentInChildren<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene(MainGame);
    }
}
