using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController03 : MonoBehaviour
{
    
    [SerializeField]
    Transform target;  // 카메라가 추적하는 대상
    [SerializeField] 
    float minDistance = 3;  // 카메라와 target의 최소 거리
    [SerializeField]
    float maxDistance = 30;  // 카메라와 target의 최대 거리
    [SerializeField]
    float wheelSpeed = 500;  // 마우스 휠 스크롤 속도
    [SerializeField]
    float xMoveSpeed = 500;  // 카메라의 y축 회전 속도
    [SerializeField]
    float yMoveSpeed = 500;  // 카메라의 x축 회전 속도
    [SerializeField]
    float yMinLimit = 5;     // 카메라의 x축 회전 제한 최소 값
    [SerializeField]
    float yMaxLimit = 80;    // 카메라의 x축 회전 제한 최댓 값
    float x, y;              // 마우스 이동 방향 값
    float distance;          // 카메라와 target의 거리

    GameObject player;
    AnimatorController animatorController;
    PlayerController3D playerController;

    // 초기 값 설정.
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

    // 우클릭 시 카메라 회전 처리
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

        

        // 마우스 휠 확대 축소
        distance -= Input.GetAxis("Mouse ScrollWheel") * wheelSpeed * Time.deltaTime;
        distance = Mathf.Clamp (distance, minDistance,maxDistance);
    }

    float ClampAngle(float angle,float min,float max) 
    {
        if (angle > 360) angle -= 360;
        if (angle < -360) angle += 360;

        return angle = Mathf.Clamp(angle, min, max);
    }

    // 타겟 쫓기
    private void LateUpdate()
    {
        transform.position = target.position + transform.rotation * new Vector3(0, 0, -distance);
    }


}
