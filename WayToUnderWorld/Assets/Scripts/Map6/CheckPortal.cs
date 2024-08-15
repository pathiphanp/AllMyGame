using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckPortal : MonoBehaviour
{
    [SerializeField] GameObject rollPortal;
    [SerializeField] GameObject devil;
    [SerializeField] float radius, delay;
    [SerializeField] LayerMask player;
    [SerializeField] GameObject[] scripts;
    bool endGame = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Roll();
        StartEndScene();
    }
    void Roll()
    {
        rollPortal.transform.Rotate(0, 0, 0.2f);
    }

    void StartEndScene()
    {
        if (Physics2D.OverlapCircle(transform.position, radius, player))
        {
            Player.player.speedMove = 0;
            devil.SetActive(true);
            AudioManager.Instance.ToggleMusic();
            if (endGame)
            {
                endGame = false;
                StartCoroutine(RunScriptsEndGame());
            }
        }
    }
    IEnumerator RunScriptsEndGame()
    {
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.PlaySFX("DropItemMap2");
        scripts[0].SetActive(true);
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlaySFX("DropItemMap2");
        scripts[0].SetActive(false);
        scripts[1].SetActive(true);
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlaySFX("LoseMiniGame");
        Gamemanager.gamemanager.playerFly = 0;
        SceneManager.LoadScene(1);
        while(true)
        {
            break;
        }

    }
}
