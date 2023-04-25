using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class AnimatorController : MonoBehaviour
{
    Animator anim;
    public bool isJumping;
    public bool isRolling;
    public bool isBackStep;
    AnimationClip attack1;
    AnimationClip attack2;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //anim.SetFloat("OnWeaponAttack", 2f);

    }

    public void MoveAnim(float horizontal,float vertical)
    {

        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
    }

    public void JumpAmim()
    {
        anim.SetBool("OnJump",true);
        isJumping = true;
    }

    public void RollAnim()
    {
        anim.SetTrigger("OnRoll");
        isRolling = true;
    }

    public void BackStepAnim()
    {
        anim.SetTrigger("OnBackStep");
        isBackStep = true;
    }

    public void WeaponAttackAnim()
    {
        // 점프 중이면 일반 공격 불가.
        if(!isJumping)
        {
            anim.SetFloat("AttackSpeed", PlayerStats.AttackSpeed);
            anim.SetTrigger("OnWeaponAttack");
        }
    }
    

    public void DropAttackAnim()
    {
        // 점프 중에만 낙하 트리거 발동
        if (isJumping)
        {
            anim.SetTrigger("OnDropAttack");
        }
    }

    public bool IsPlayingAnimation__BackStep()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("BackStep");
    }

    public bool IsPlayingAnimation__Roll()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("Roll");
    }

    public bool IsPlayingAnimation__WeaponAttack()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("attack1") || anim.GetCurrentAnimatorStateInfo(0).IsName("attack2");
    }

    public bool IsPlayingAnimation__DropAttack()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("DropAttack");
    }

    public bool IsPlayingAnimation__Jump()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("Jump");
    }


}
