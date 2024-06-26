using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Video : MonoBehaviour
{
    void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // �������� ������� �������� ������ � ������� �����������
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // �������� ������� ������� � ������� �����������
        float spriteHeight = spriteRenderer.sprite.bounds.size.y;
        float spriteWidth = spriteRenderer.sprite.bounds.size.x;

        // ������������ �������, ����������� ��� ������������ �������� ������� � ������
        Vector3 scale = transform.localScale;
        scale.x = camWidth / spriteWidth;
        scale.y = camHeight / spriteHeight;
        transform.localScale = scale;
    }
}
