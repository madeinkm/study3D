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
            cam.transform.position += Vector3.forward * camSpeed * Time.deltaTime;
        }
    }
    private void rotating()
    {

    }
}
