using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

// �ִϸ��̼� ����.
// ȥ�ڼ� ���� �� �� ���� �� ���� �ݺ�.
// 1������
// 2ī�޶�
// 3���ϸ��̼�





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
        // �Է°� ����.
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        // ī�޶� rotation ����.
        cameraController.RotateTo(mouseX, mouseY);

        //Ű���� xz�Է¹���
        XZmoveDir = new Vector3(x, 0, z);

        // Ű���� �Է¿� ���� ī�޶��� ������ ����� �̵� ���� ����
        Vector3 movedis = cameraTransform.rotation * XZmoveDir;

        // �̵� ����
        moveDir = new Vector3(movedis.x, moveDir.y, movedis.z);

        // �÷��̾� �̵�
        cc.Move(moveDir * speed * Time.deltaTime);

        // �߷�
        if (cc.isGrounded == false)
        {
            moveDir.y += gravity * Time.deltaTime;
        }

        // ����
        if(cc.isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            moveDir.y = jumpPower;
        }

    }
}
