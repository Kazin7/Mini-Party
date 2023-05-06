using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAbovePlayer : MonoBehaviour
{
    public TMP_Text textObject;
    public Transform playerTransform;
    public float yOffset = 1.15f;

    void Update()
    {
        // 텍스트 오브젝트 위치 계산
        Vector3 textPosition = new Vector3(playerTransform.position.x,playerTransform.position.y+yOffset,playerTransform.position.z);

        // 텍스트 오브젝트 위치 설정
        textObject.transform.position = textPosition;

        // 카메라에서 텍스트 오브젝트까지의 방향 벡터 계산
        Vector3 direction = textObject.transform.position - Camera.main.transform.position;

        // y값만 0으로 만들어주어서 카메라의 forward 벡터와 평행하게 만듦
        direction.y = 0;

        // 방향 벡터의 회전값을 구하고, 텍스트 오브젝트의 방향으로 설정
        Quaternion rotation = Quaternion.LookRotation(direction);
        textObject.transform.rotation = rotation;
    }
}

