using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class GhostCam : MonoBehaviour
{
    // int , uint 
    [SerializeField] private float mouseSens = 100;
    [SerializeField] private float camSpeed = 5;
    private Vector3 rotateValue;//현재의 회전값 저장용
    private Camera cam;


    void Start()
    {
        cam = Camera.main;
        rotateValue = cam.transform.rotation.eulerAngles; // 쿼터니언 값을 vec3값으로 변경하기위해서 오일러앵글을 이용해 형변환

    }
    
    void Update()
    {
        moving();//이동
        rotating();//회전

    }
    private void moving()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //cam.transform.position += Vector3.forward * camSpeed * Time.deltaTime; //월드포지션 기준으로 움직임.
            //cam.transform.position += cam.transform.rotation * Vector3.forward * camSpeed * Time.deltaTime; //카메라포지션 기준으로 움직임
            cam.transform.position += cam.transform.TransformDirection(Vector3.forward) * camSpeed * Time.deltaTime; // 위 표현과 동일함
        }
        else if (Input.GetKey(KeyCode.S))
        {
            cam.transform.position += cam.transform.TransformDirection(Vector3.back) * camSpeed * Time.deltaTime; // 위 표현과 동일함
        }
        if (Input.GetKey(KeyCode.A))
        {
            cam.transform.position += cam.transform.TransformDirection(Vector3.left) * camSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            cam.transform.position += cam.transform.TransformDirection(Vector3.right) * camSpeed * Time.deltaTime;
        }
    }
    private void rotating()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSens * Time.deltaTime;

        rotateValue.x += mouseY * -1;
        rotateValue.y += mouseX;

        cam.transform.rotation = Quaternion.Euler(rotateValue.x, rotateValue.y, 0f);//(rotateValue) 과 동일
    }
}
