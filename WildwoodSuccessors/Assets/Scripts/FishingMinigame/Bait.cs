using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    public PlayerFarming playerFarming;
    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public Rigidbody2D rb;
    public Animator anim;

    void Update()
    {
        Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _lineRenderer.SetPosition(0, mousePos);
        _lineRenderer.SetPosition(1, transform.position);
        catchFish();
    }

    void OnEnable()
    {
        StartCoroutine(countToAnimation());
    }

    IEnumerator countToAnimation()
    {
        AudioManager.Instance.PlaySFX("SwingRod");

        rb.gravityScale = 1;
        anim.SetBool("isFishing", false);
        yield return new WaitForSeconds(0.4f);

        AudioManager.Instance.PlaySFX("BaitTouchWater");

        anim.SetBool("isFishing", true);
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
    }

    public void catchFish()
    {
        if (playerFarming.isCatchFish)
        {
            anim.SetBool("isCatch", true);
        }
        else
        {
            anim.SetBool("isCatch", false);
        }

        if (playerFarming.isFishingAgain)
        {
            anim.SetBool("isFishingAgain", true);
        }
        else
        {
            anim.SetBool("isFishingAgain", false);
        }
    }
}
