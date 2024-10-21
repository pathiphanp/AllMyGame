using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ControlPart : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Coroutine blinkEffect;
    [SerializeField] float speedBlinkEffect;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    private void OnMouseEnter()
    {
        if (ControlGamePlay._instance.canSelectPart)
        {
            spriteRenderer.color = Color.red;
        }
    }
    private void OnMouseExit()
    {
        if (ControlGamePlay._instance.canSelectPart)
        {
            spriteRenderer.color = Color.white;
        }
    }
    private void OnMouseUp()
    {
        if (ControlGamePlay._instance.canSelectPart)
        {
            BlinkEffect();
            ControlGamePlay._instance.PlayerSelectPart(this.gameObject);
        }
    }

    public void BlinkEffect()
    {
        blinkEffect = StartCoroutine(CallBlinkEffect());
    }
    public void StopBlinkEffect()
    {
        if (blinkEffect != null)
        {
            // StopCoroutine(blinkEffect);
            blinkEffect = null;
            spriteRenderer.color = Color.white;
        }
        spriteRenderer.enabled = true;
    }

    IEnumerator CallBlinkEffect()
    {
        spriteRenderer.color = Color.red;
        /*bool startEff = true;
        while (startEff)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(speedBlinkEffect / 2);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(speedBlinkEffect / 2);
            yield return true;
        }*/
        yield return true;
    }
}
