using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController3D : MonoBehaviour
{
    float moveSpeed = PlayerStats.moveSpeed;
    float x, z;
    float gravity = -9.8f;
    [SerializeField]
    float jumpForce = 3f;
    float offset;
    float number=0f;
    bool isLeftDown;
    bool isAttacking;
    public static bool isDie;

    Vector3 direction;
    Vector3 moveDir;

    CharacterController cc;
    [SerializeField]
    Transform cameraTransform;
    AnimatorController animatorController;

    Animator anim;

    [SerializeField]
    BoxCollider SwordCollider;

    PlayerStatsController PSC;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        SwordCollider.enabled = false;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        animatorController = GetComponent<AnimatorController>();
        anim = GetComponent<Animator>();
        PSC = PlayerStatsController.Instance_PlayerStatsController;
    }


    private void Update()
    {
        PSC.RegenHPMPSP();

        if (Input.GetKeyDown(KeyCode.B))
        {
            PSC.HP_P();
            
        }


        if (isDie)
        {
            x = 0;
            z = 0;
            moveDir = new Vector3(0, 0, 0);
            moveSpeed = 0f;
        }
        //Debug.Log(animatorController.isJumping);
        // 공격 중 && 낙하 공격 중
        else if (animatorController.IsPlayingAnimation__WeaponAttack()||
            animatorController.IsPlayingAnimation__DropAttack()||
            isDie)
        {
            x = 0;
            z = 0;
            moveDir = new Vector3(0, moveDir.y, 0);
            //moveSpeed = 0f;

        }
        else
        {
            // 구르기 백스탭 중엔 이동값 입력 못받음.
            if (!animatorController.isRolling && !animatorController.isBackStep)
            {
                x = Input.GetAxis("Horizontal");
                z = Input.GetAxis("Vertical");
                if(z>0) moveSpeed = PlayerStats.moveSpeed;
                else moveSpeed = PlayerStats.moveSpeed * 0.6f;

                moveDir = new Vector3(direction.x, moveDir.y, direction.z);
                //moveSpeed = 5f;
            }
        }


        // r키 누르면 5초 후 부활.
        if (Input.GetKeyDown(KeyCode.R) && isDie)
        {
            Debug.Log("소생!");
            StartCoroutine(PSC.CO_Revive(1f));
        }

        //Debug.Log(moveSpeed);
        // 공격
        if (Input.GetMouseButtonDown(0) && Is_SP_Over10() &&!isDie)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // UI 패널 위에 있으면 공격하지 않음
                return;
            }
            if (!isAttacking)
            {
                StartCoroutine(AttackSpendSP());
                StartCoroutine(WeaponCollider());
            }

            animatorController.WeaponAttackAnim();
            animatorController.DropAttackAnim();

            
        }

        // 공격 시 칼 콜라이더 온
        if (animatorController.IsPlayingAnimation__WeaponAttack() || animatorController.IsPlayingAnimation__DropAttack())
        {
            SwordCollider.enabled = true;
        }
        else
        {
            SwordCollider.enabled = false;
        }

        IEnumerator WeaponCollider()
        {
            SwordCollider.enabled = true;
            yield return new WaitForSeconds(PlayerStats.AttackSpeed);
            SwordCollider.enabled = false;
        }


        animatorController.MoveAnim(x, z);

        // 중력
        if (cc.isGrounded == false)
        {
            moveDir.y += gravity * Time.deltaTime;
        }

        if (cc.isGrounded)
        {
            anim.SetBool("OnJump", false);
            animatorController.isJumping = false;
        }


        // 점프
        if(cc.isGrounded == true && Input.GetKeyDown(KeyCode.F) 
            && !animatorController.IsPlayingAnimation__DropAttack()
            && !animatorController.IsPlayingAnimation__Jump() &&!animatorController.IsPlayingAnimation__WeaponAttack()
            && !animatorController.IsPlayingAnimation__Roll() &&!animatorController.IsPlayingAnimation__BackStep()
            && Is_SP_Over10()
            && !isDie)
        {
            moveDir.y = jumpForce;
            animatorController.JumpAmim();
            PSC.Spend_SP(10);
        }

        //구르기  백스텝
        if (Input.GetKeyDown(KeyCode.Space)
            &&!animatorController.IsPlayingAnimation__Roll() &&!animatorController.IsPlayingAnimation__Jump()
            && !animatorController.IsPlayingAnimation__WeaponAttack()&&!animatorController.IsPlayingAnimation__BackStep()
            && Is_SP_Over10()
            && !isDie)
        {
            if(x<0.1f &&-0.1f < x && z<=0)
            {
                animatorController.BackStepAnim();
                PSC.Spend_SP(10);
                StartCoroutine(BackStep());

            }
            else if(z > 0.99 && x == 0)
            {
                animatorController.RollAnim();
                PSC.Spend_SP(10);
                StartCoroutine(Roll());
                
            }
        }

        //if (!animatorController.IsPlayingAnimation__Roll())
        //{
        //    anim.SetBool("OnRoll", false);
        //    animatorController.isRolling = false;
        //}

        //direction = cameraTransform.rotation * new Vector3(x, 0, z).normalized;
        //moveDir = new Vector3(direction.x, moveDir.y, direction.z);

        Vector3 horizontalDirection = transform.right * x;
        Vector3 verticalDirection = transform.forward * z;
        direction = (horizontalDirection + verticalDirection).normalized;
        moveDir = new Vector3(direction.x, moveDir.y, direction.z);

        cc.Move(moveDir * moveSpeed * Time.deltaTime);

        if(!animatorController.isRolling && !animatorController.isBackStep)
            transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
    }

    IEnumerator AttackSpendSP()
    {
        PlayerStatsController.Instance_PlayerStatsController.Spend_SP(10);
        isAttacking = true;
        yield return new WaitForSeconds(PlayerStats.AttackSpeed);
        isAttacking = false;
    }

    IEnumerator Roll()
    {
        moveDir = new Vector3(1f, moveDir.y, 0);
        moveSpeed *= 2;

        yield return new WaitForSeconds(1f);

        moveSpeed /= 2;
        animatorController.isRolling = false;

        yield return null;
    }

    IEnumerator BackStep()
    {
        z = -1;
        moveSpeed *= 2;
        yield return new WaitForSeconds(0.8f);

        moveSpeed /= 2;
        animatorController.isBackStep = false;

        yield return null;
    }

    bool Is_SP_Over10()
    {
        if (PlayerStats.currentStaminaPoint > 10)
            return true;
        else
            return false;
    }
}
