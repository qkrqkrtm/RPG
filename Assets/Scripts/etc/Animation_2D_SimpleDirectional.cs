using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_2D_SimpleDirectional : MonoBehaviour
{
    float vertical;
    float horizontal;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
    }
}
