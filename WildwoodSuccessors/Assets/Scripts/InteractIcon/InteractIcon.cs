using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractIcon : MonoBehaviour
{
    public ChangeCamera changeCamera;
    public GameObject camera;
    Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnMouseDown()
    {
        changeCamera.CameraOn(camera);
        anim.SetBool("Zoom", true);
    }
}
