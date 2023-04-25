using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_2D_FreedomDirectional : MonoBehaviour
{
    float vertical;
    float horizontal;
    float offset;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        offset = 0.5f + Input.GetAxis("Sprint") * 0.5f;

        anim.SetFloat("Horizontal", horizontal*offset);
        anim.SetFloat("Vertical", vertical * offset );
        Debug.Log(vertical * offset);
    }
}
