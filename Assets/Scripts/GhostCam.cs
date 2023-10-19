using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class GhostCam : MonoBehaviour
{
    // int , uint 
    [SerializeField] private float mouseSens = 100;
    [SerializeField] private float camSpeed = 5;
    private Vector3 rotateValue;//������ ȸ���� �����
    private Camera cam;


    void Start()
    {
        cam = Camera.main;
        rotateValue = cam.transform.rotation.eulerAngles; // ���ʹϾ� ���� vec3������ �����ϱ����ؼ� ���Ϸ��ޱ��� �̿��� ����ȯ

    }
    
    void Update()
    {
        moving();//�̵�
        rotating();//ȸ��

    }
    private void moving()
    {
        if (Input.GetKey(KeyCode.W))
        {
            cam.transform.position += Vector3.forward * camSpeed * Time.deltaTime;
        }
    }
    private void rotating()
    {

    }
}
