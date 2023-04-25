using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController03 : MonoBehaviour
{
    
    [SerializeField]
    Transform target;  // ī�޶� �����ϴ� ���
    [SerializeField] 
    float minDistance = 3;  // ī�޶�� target�� �ּ� �Ÿ�
    [SerializeField]
    float maxDistance = 30;  // ī�޶�� target�� �ִ� �Ÿ�
    [SerializeField]
    float wheelSpeed = 500;  // ���콺 �� ��ũ�� �ӵ�
    [SerializeField]
    float xMoveSpeed = 500;  // ī�޶��� y�� ȸ�� �ӵ�
    [SerializeField]
    float yMoveSpeed = 500;  // ī�޶��� x�� ȸ�� �ӵ�
    [SerializeField]
    float yMinLimit = 5;     // ī�޶��� x�� ȸ�� ���� �ּ� ��
    [SerializeField]
    float yMaxLimit = 80;    // ī�޶��� x�� ȸ�� ���� �ִ� ��
    float x, y;              // ���콺 �̵� ���� ��
    float distance;          // ī�޶�� target�� �Ÿ�

    GameObject player;
    AnimatorController animatorController;
    PlayerController3D playerController;

    // �ʱ� �� ����.
    private void Awake()
    {
        distance = Vector3.Distance(target.position, transform.position);
        player = GameObject.Find("Solus_The_Knight");
        animatorController = player.GetComponent<AnimatorController>();
        playerController = player.GetComponent<PlayerController3D>();
        //Vector3 angle = transform.eulerAngles;
        //x = angle.x;
        //y = angle.y;
    }

    // ��Ŭ�� �� ī�޶� ȸ�� ó��
    private void Update()
    {
        if (!animatorController.IsPlayingAnimation__WeaponAttack() && !animatorController.IsPlayingAnimation__DropAttack()
            &&!animatorController.isBackStep && !animatorController.isRolling
            &&!PlayerController3D.isDie)
        {
            x += Input.GetAxis("Mouse X") * xMoveSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * yMoveSpeed * Time.deltaTime;

            if (y < yMinLimit)
                y = yMinLimit;
            else if (y > yMaxLimit)
                y = yMaxLimit;

            float eulerAngleX = y;
            float eulerAngleY = x;

            eulerAngleX = ClampAngle(eulerAngleX, yMinLimit, yMaxLimit);

            transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
        }

        

        // ���콺 �� Ȯ�� ���
        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelSpeed * Time.deltaTime;
        distance = Mathf.Clamp (distance, minDistance,maxDistance);
    }

    float ClampAngle(float angle,float min,float max) 
    {
        if (angle > 360) angle -= 360;
        if (angle < -360) angle += 360;

        return angle = Mathf.Clamp(angle, min, max);
    }

    // Ÿ�� �ѱ�
    private void LateUpdate()
    {
        transform.position = target.position + transform.rotation * new Vector3(0, 0, -distance);
    }


}
