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
            //cam.transform.position += Vector3.forward * camSpeed * Time.deltaTime; //���������� �������� ������.
            //cam.transform.position += cam.transform.rotation * Vector3.forward * camSpeed * Time.deltaTime; //ī�޶������� �������� ������
            cam.transform.position += cam.transform.TransformDirection(Vector3.forward) * camSpeed * Time.deltaTime; // �� ǥ���� ������
        }
        else if (Input.GetKey(KeyCode.S))
        {
            cam.transform.position += cam.transform.TransformDirection(Vector3.back) * camSpeed * Time.deltaTime; // �� ǥ���� ������
        }
        if (Input.GetKey(KeyCode.A))
        {
            cam.transform.position += cam.transform.TransformDirection(Vector3.left) * camSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            cam.transform.position += cam.transform.TransformDirection(Vector3.right) * camSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            cam.transform.position += Vector3.up * camSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            cam.transform.position += Vector3.down * camSpeed * Time.deltaTime;
        }
    }
    private void rotating()
    {
        if (Input.GetKey(KeyCode.Mouse1) == false) // ���콺������ ��ư �Է� ���� �� ����, �ԷµǸ� �Ʒ� ����� ���۵�
        {
            return;
        }

        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSens * Time.deltaTime;

        rotateValue.x += mouseY * -1;
        rotateValue.y += mouseX;

        //if (rotateValue.x > 90)
        //{
        //    rotateValue.x = 90;
        //}
        //else if (rotateValue.x < -90)
        //{
        //    rotateValue.x = -90;
        //}
        rotateValue.x = Mathf.Clamp(rotateValue.x, -90, 90); // �� if���� ������ ���

        cam.transform.rotation = Quaternion.Euler(rotateValue.x, rotateValue.y, 0f);//Quaternion.Euler(rotateValue) �� ����
    }
}
