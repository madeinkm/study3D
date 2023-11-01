using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    private Rigidbody rigid;
    private CapsuleCollider caps;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private bool isGround = false;
    private float ver_Velocity = 0;
    private float gravity = 9.81f;
    private bool isJump = false;
    private Vector3 moveDir = Vector3.zero;
    
    [Header("Player 회전")]
    [SerializeField, Range(0f, 500f)] private float mouseSens = 100f;
    private Vector3 rotValue = Vector3.zero;
    private Camera camMain;

    private GameObject eye;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        caps = GetComponent<CapsuleCollider>();
        eye = transform.GetChild(0).gameObject;

    }

    private void Start()
    {
        camMain = Camera.main;
    }

    void Update()
    {
        checkGrounded();
        moving();
        jump();
        checkGravity();
        rotation();
        
    }
    private void checkGrounded()
    {
        isGround = false;
        if (ver_Velocity <= 0) // 떨어지는 중
        {
            isGround = Physics.Raycast(transform.position, Vector3.down, caps.height * 0.55f, LayerMask.GetMask("Ground"));
        }

    }
    private void moving()
    {
        moveDir.x = inputHorizontal();//A D 왼쪽 오른쪽
        moveDir.z = inputVertical();//W S 앞 뒤
        moveDir.y = rigid.velocity.y;
        rigid.velocity = transform.rotation * moveDir;
    }
    private float inputHorizontal()
    {
        if (Input.GetKey(KeyCode.A))
        {
            return -1 * moveSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            return 1 * moveSpeed;
        }
        return 0;

        //return Input.GetAxisRaw("Horizontal"); wasd 방향키 등 모두 사용됨
    }
    private float inputVertical()
    {
        return Input.GetAxisRaw("Vertical") * moveSpeed;
    }
    private void jump()
    {
        if (isGround == false) 
        {
            return;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            isJump = true;            
        }
    }
    private void checkGravity()
    {
        if (isGround == true) 
        {
            ver_Velocity = 0;
        }

        if (isJump == true)
        {
            isJump = false;
            ver_Velocity = jumpForce;
        }

        else if (isGround == false)
        {
            ver_Velocity -= gravity * Time.deltaTime;

        }
        
        rigid.velocity = new Vector3(rigid.velocity.x, ver_Velocity, rigid.velocity.z);
    }
    private void rotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSens * Time.deltaTime;

        rotValue.x += mouseY * -1;
        rotValue.y += mouseX;

        rotValue.x = Mathf.Clamp(rotValue.x, -45, 45);
        transform.rotation = Quaternion.Euler(0, rotValue.y, 0);//좌우 움직임       
        //camMain.transform.rotation = Quaternion.Euler(rotValue);


        if (Input.GetMouseButton(1) && mouseY > 0)
        {
            camMain.transform.RotateAround(eye.transform.position, Vector3.left, mouseSens * Time.deltaTime);
        }
        else if (Input.GetMouseButton(1) && mouseY < 0)
        {
            camMain.transform.RotateAround(eye.transform.position, Vector3.right, mouseSens * Time.deltaTime);
        }
    }
}
