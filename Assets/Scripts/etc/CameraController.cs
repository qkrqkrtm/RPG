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
        // Y���� �������� ���콺 ȸ��  �¿�
        eulerAngleY += mouseX * rotateSpeedX;
        // X���� �������� ���콺 ȸ��  ����
        eulerAngleX -= mouseY * rotateSpeedY;

        // �¿� ���콺 ȸ���� �޸� ���� ȸ���� �Ѱ� ����.
        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);

        // rotation�� Euler�� ����.
        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
    }

    // ���� ���콺 �̵� �Ѱ� ���� �Լ�
    float ClampAngle(float angle, float min, float max)
    {
        if (angle > 360) angle -= 360;
        if (angle < -360) angle += 360;
        angle = Mathf.Clamp(angle, min, max);
        return angle;
    }
}


