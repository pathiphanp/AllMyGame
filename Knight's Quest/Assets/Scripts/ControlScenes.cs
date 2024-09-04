using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ControlScenes : MonoBehaviour

{
    [Header("Fade")]
    [SerializeField] float fadespeed;
    [SerializeField] GameObject canvasFade;
    [SerializeField] Image fadeScenes;
    Color _fade;
    GameObject mainScenes;
    GameObject lastScenes;
    [Header("Button")]
    [SerializeField] Button backScenes;
    [SerializeField] Button startGameBtn;
    [SerializeField] Button settingBtn;
    [SerializeField] Button settingBtnInGame;
    [Header("Scenes")]
    [SerializeField] GameObject mainManuScenes;
    [SerializeField] GameObject gamePlayScenes;
    [SerializeField] GameObject settingScenes;

    [Header("Exit")]
    [SerializeField] Button exitBtn;
    // Start is called before the first frame update
    void Start()
    {
        ControlGamePlay._instance.controlScenes = this;
        backScenes.onClick.AddListener(BackScenes);
        startGameBtn.onClick.AddListener(PlayGame);
        settingBtn.onClick.AddListener(Setting);
        settingBtnInGame.onClick.AddListener(Setting);
        exitBtn.onClick.AddListener(ExitGame);
        mainScenes = mainManuScenes;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void PlayGame()
    {
        StartCoroutine(ChangeScenes(gamePlayScenes));
    }
    void Setting()
    {
        StartCoroutine(ChangeScenes(settingScenes));
    }
    void ExitGame()
    {
        Application.Quit();
    }
    void BackScenes()
    {
        StartCoroutine(ChangeScenes(lastScenes));
    }
    public void ResetGamePlay()
    {
        StartCoroutine(ChangeScenes(null));
    }
    IEnumerator ChangeScenes(GameObject _OpenScenes)
    {
        canvasFade.SetActive(true);
        fadeScenes.fillAmount = 0;
        while (fadeScenes.fillAmount != 1)
        {

            fadeScenes.fillAmount += Time.deltaTime * fadespeed;
            yield return new WaitForSeconds(0);
        }
        yield return new WaitForSeconds(0.2f);
        if (_OpenScenes != null)
        {
            if (_OpenScenes != lastScenes)
            {
                ChangeNewScenes(_OpenScenes);
            }
            else
            {
                ChangeBackScenes();
            }
        }
        while (fadeScenes.fillAmount != 0)
        {
            fadeScenes.fillAmount -= Time.deltaTime * fadespeed;
            yield return new WaitForSeconds(0);
        }
        canvasFade.SetActive(false);
    }
    void ChangeNewScenes(GameObject _OpenScenes)
    {
        lastScenes = mainScenes;
        lastScenes.SetActive(false);
        mainScenes = _OpenScenes;
        _OpenScenes.SetActive(true);
    }
    void ChangeBackScenes()
    {
        lastScenes.SetActive(true);
        mainScenes.SetActive(false);
        mainScenes = lastScenes;
    }
}
