using UnityEngine;
using System.Collections;

public class GOMenuScript : MonoBehaviour
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
        StartCoroutine(WaitForSoundAndTransition("MainMenu"));
    }

    private IEnumerator WaitForSoundAndTransition(string MainMenu)
    {
        AudioSource audioSource = GetComponentInChildren<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene(MainMenu);
    }
}
