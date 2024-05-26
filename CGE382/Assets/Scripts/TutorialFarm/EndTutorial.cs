using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTutorial : MonoBehaviour
{
    public AudioManager audioManager;
    public GameObject animations;
    private void OnMouseDown()
    {
        StartCoroutine(delayLoadScene());
    }
    IEnumerator delayLoadScene()
    {
        animations.SetActive(true);
        yield return new WaitForSeconds(3f);
        if (audioManager != null)
        {
            Destroy(audioManager.gameObject);
        }
        SceneManager.LoadScene(3);
    }
}
