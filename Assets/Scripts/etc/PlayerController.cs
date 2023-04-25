using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

// 애니메이션 구현.
// 혼자서 조절 할 수 있을 때 까지 반복.
// 1움직임
// 2카메라
// 3에니메이션





public class PlayerController : MonoBehaviour
{
    float x;
    float z;
    float mouseX;
    float mouseY;
    float gravity = -9.8f;

    Vector3 moveDir;
    Vector3 XZmoveDir;

    [SerializeField]
    float speed = 5f;
    [SerializeField]
    float jumpPower = 3f;
     
    NavMeshAgent nav;
    CharacterController cc;
    CameraController cameraController;
    Transform cameraTransform;


    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        nav = GetComponent<NavMeshAgent>();
        cameraController = GetComponentInChildren<CameraController>();
        cameraTransform = cameraController.GetComponentInChildren<Transform>();
    }

    private void Update()
    {
        // 입력값 설정.
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        // 카메라 rotation 설정.
        cameraController.RotateTo(mouseX, mouseY);

        //키보드 xz입력백터
        XZmoveDir = new Vector3(x, 0, z);

        // 키보드 입력에 따라 카메라의 방향을 고려한 이동 방향 벡터
        Vector3 movedis = cameraTransform.rotation * XZmoveDir;

        // 이동 백터
        moveDir = new Vector3(movedis.x, moveDir.y, movedis.z);

        // 플레이어 이동
        cc.Move(moveDir * speed * Time.deltaTime);

        // 중력
        if (cc.isGrounded == false)
        {
            moveDir.y += gravity * Time.deltaTime;
        }

        // 점프
        if(cc.isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            moveDir.y = jumpPower;
        }

    }
}
