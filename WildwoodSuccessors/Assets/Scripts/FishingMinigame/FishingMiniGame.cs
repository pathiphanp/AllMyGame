using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum FishingState
{
    Normal, Fishing, Waiting, FishCatching, Finish
}
public class FishingMiniGame : MonoBehaviour
{
    [SerializeField] PlayerFarming playerFarming;
    public AudioSource sound;
    [SerializeField] Transform topPivot;
    [SerializeField] Transform bottomPivot;

    [Header("Fish movement")]
    [SerializeField] Transform fish;

    float fishPosition;
    float fishDestination;

    float fishTimer;
    [SerializeField] float timerMultiplicator = 3f;

    [SerializeField] float fishSpeed;
    [SerializeField] float smoothMotion = 1f;

    [Header("Hook area")]
    [SerializeField] Transform hook;
    [SerializeField] Transform topHook;
    [SerializeField] Transform bottomHook;
    float hookPosition;
    [SerializeField] float hookSize = 0.1f;
    [SerializeField] float hookPower = 5f;

    [Header("Hook properties")]
    public float hookProgress;
    public float hookPullVelocity;
    [SerializeField] float hookPullPower = 0.01f;
    [SerializeField] float hookGravityPower = 0.005f;
    [SerializeField] float hookProgressDegradationPower = 1f;

    [SerializeField] SpriteRenderer hookSpriteRenderer;

    [Header("Container bar")]
    [SerializeField] Transform progressBarContainer;

    [Header("Pasue game")]
    public bool isPause;
    public bool isEnd;
    [SerializeField] float failTimer = 10f;

    [Header("Interface")]
    public TextMeshProUGUI textInterface;
    [Header("Fish data")]
    public ItemData[] fishData;

    bool isLose;
    private void Start()
    {
        Resize();
        isPause = false;
    }

    private void Resize()
    {
        hook.localScale = new Vector3(1, hookSize, 1);
    }

    private void OnEnable()
    {
        AudioManager.Instance.PlaySFX("FishBiteBait");
        AudioManager.Instance.PlaySFXInObject(sound, "FishProcess");

        RestartMinigame();
    }

    void OnDisable()
    {
        textInterface.text = "Press spacebar to fishing";
    }

    private void Update()
    {
        if (playerFarming.isFishsing)
        {
            textInterface.text = "";
        }
        if (!isPause)
        {
            Fish();
            Hook();
            ProgressCheck();
            LoseCount();
            FishFace();
        }

        if (failTimer < 0f)
        {
            if (!isLose)
            {
                isLose = true;
                Lose();
            }
        }
    }

    private void ProgressCheck()
    {
        textInterface.text = "Hold it";
        Vector3 ls = progressBarContainer.localScale;
        ls.y = hookProgress;
        progressBarContainer.localScale = ls;

        float min = hookPosition - hookSize / 2;
        float max = hookPosition + hookSize / 2;

        if (min < fishPosition && fishPosition < max)
        {
            hookProgress += hookPower * Time.deltaTime;
            failTimer = 1;

        }
        else
        {
            hookProgress -= hookProgressDegradationPower * Time.deltaTime;
        }

        if (hookProgress >= 1)
        {
            Win();
        }

        hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);
    }

    void Hook()
    {
        if (Input.GetMouseButton(0))
        {
            hookPullVelocity = Mathf.Clamp(hookPullVelocity, -0.001f, 0.003f);
            hookPullVelocity += hookPullPower * Time.deltaTime;
            //Debug.Log(hook.position.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (hook.position == topHook.position)
            {
                hookPullVelocity = -0.0009f;
            }
        }

        hookPullVelocity -= hookGravityPower * Time.deltaTime;
        hookPullVelocity = Mathf.Clamp(hookPullVelocity, -0.003f, 0.003f);
        hookPosition += hookPullVelocity;
        hookPosition = Mathf.Clamp(hookPosition, hookSize / 2, 1 - hookSize / 2);
        hook.position = Vector3.Lerp(bottomPivot.position, topPivot.position, hookPosition);
    }

    void Fish()
    {
        fishTimer -= Time.deltaTime;
        isLose = false;
        if (fishTimer < 0f)
        {
            fishTimer = Random.value * timerMultiplicator;
            fishDestination = Random.value;
        }
        fishPosition = Mathf.SmoothDamp(fishPosition, fishDestination, ref fishSpeed, smoothMotion);
        fish.position = Vector3.Lerp(bottomPivot.position, topPivot.position, fishPosition);
    }

    void Win()
    {
        StartCoroutine(closeMinigame());
        AudioManager.Instance.PlaySFX("GetFish");
        textInterface.text = "You got fish";
        RandomDrop();
        isPause = true;
        Debug.Log("You win!");
    }

    void Lose()
    {
        StartCoroutine(closeMinigame());
        AudioManager.Instance.PlaySFX("FishEscape");
        textInterface.text = "The Fish has fied";
        isPause = true;
        Debug.Log("You lose!");
    }

    void LoseCount()
    {
        if (hookProgress <= 0)
        {
            failTimer -= Time.deltaTime;
        }
    }

    IEnumerator closeMinigame()
    {
        AudioManager.Instance.StopSFXInObject(sound);
        yield return new WaitForSeconds(1);
        playerFarming.isCatchFish = false;
        playerFarming.bait.SetActive(false);
        this.gameObject.SetActive(false);
    }

    void RestartMinigame()
    {
        hook.position = bottomHook.position;
        fish.position = bottomHook.position;
        isPause = false;
        hookProgress = 0f;
        failTimer = 0.5f;
        hookPosition = 0f;
        fishPosition = 0f;
        Resize();
    }

    void FishFace()
    {
        if (fishSpeed > 0)
        {
            fish.localScale = new Vector3(-1, fish.localScale.y, fish.localScale.z);
        }
        else
        {
            fish.localScale = new Vector3(1, fish.localScale.y, fish.localScale.z);
        }
    }

    void RandomDrop()
    {
        int rnd = Random.Range(0, 4);
        InventoryData.Instance.inventory.Add(InventoryData.Instance.inventory.backpack, fishData[rnd], 1);
    }

    void FishinState()
    {

    }
}
