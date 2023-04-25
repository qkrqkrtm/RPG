using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float rotateSpeedX = 3;
    float rotateSpeedY = 5;
    float limitMinX = -80;
    float limitMaxX = 50;
    float eulerAngleX;
    float eulerAngleY;
    
    public void RotateTo(float mouseX,float mouseY)
    {
        // Y축을 기준으로 마우스 회전  좌우
        eulerAngleY += mouseX * rotateSpeedX;
        // X축을 기준으로 마우스 회전  상하
        eulerAngleX -= mouseY * rotateSpeedY;

        // 좌우 마우스 회전과 달리 상하 회전은 한계 설정.
        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);

        // rotation는 Euler로 설정.
        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
    }

    // 상하 마우스 이동 한계 세팅 함수
    float ClampAngle(float angle, float min, float max)
    {
        if (angle > 360) angle -= 360;
        if (angle < -360) angle += 360;
        angle = Mathf.Clamp(angle, min, max);
        return angle;
    }
}


