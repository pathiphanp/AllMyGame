using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CutScene : MonoBehaviour
{
    [SerializeField] GameObject[] scripts;
    [SerializeField] float delay;
    [SerializeField] GameObject player;
    bool start = true;

    [SerializeField] Button skipCutScenesBtn;
    // Start is called before the first frame update
    void Start()
    {
        skipCutScenesBtn.onClick.AddListener(SkipCutScenes);
    }
    // Update is called once per frame

    void Update()
    {
        if (start == true)
        {
            start = false;
            StartCoroutine(PlayCutSecne());
        }
    }
    IEnumerator PlayCutSecne()
    {
        yield return new WaitForSeconds(1);
        AudioManager.Instance.PlaySFX("DropItemMap2");
        scripts[0].SetActive(true);
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlaySFX("DropItemMap2");
        scripts[0].SetActive(false);
        scripts[1].SetActive(true);
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlaySFX("DropItemMap2");
        scripts[1].SetActive(false);
        scripts[2].SetActive(true);
        yield return new WaitForSeconds(delay);
        AudioManager.Instance.PlaySFX("DropItemMap2");
        scripts[2].SetActive(false);
        scripts[3].SetActive(true);
        yield return new WaitForSeconds(delay);
        scripts[3].SetActive(false);
        scripts[4].SetActive(true);
        AudioManager.Instance.PlaySFX("LoseMiniGame");
        yield return new WaitForSeconds(4);
        EndCutScenes();
    }
    private void SkipCutScenes()
    {
        EndCutScenes();
    }

    void EndCutScenes()
    {
        StopAllCoroutines();
        player.SetActive(true);
        Destroy(gameObject);
    }
}
