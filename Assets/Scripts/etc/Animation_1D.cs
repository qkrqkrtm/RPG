using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_1D : MonoBehaviour
{
    Animator anim;
    float vertical;
    float offset;
    float moveParameter;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        offset = 0.5f + Input.GetAxis("Sprint");
        moveParameter = vertical * offset;
        anim.SetFloat("moveSpeed", moveParameter);
    }
}
