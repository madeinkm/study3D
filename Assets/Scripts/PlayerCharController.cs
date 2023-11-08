using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class PlayerCharController : MonoBehaviour
{
    private CharacterController cCon;
    private Camera maincam;

    [SerializeField] private GameObject _3rdCam;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private Vector3 moveDir = Vector3.zero;
    private Vector3 rotateValue = Vector3.zero;
    private Vector3 slopeVelocity = Vector3.zero; // 어떤각도로 밀려날지 변수
    private float gravity = 9.81f;
    //private float mouseSense = 100f;
    private float ver_Velocity = 0f;
    [SerializeField]private bool isGround = false;
    private bool isJump = false;
    private bool isSlope = false;

    private void OnDrawGizmos()
    {
        if (cCon == null)
        {
            cCon = GetComponent<CharacterController>();
        }

        Debug.DrawLine(transform.position, transform.position - new Vector3(0, cCon.height * 0.55f, 0), Color.red);
    }

    private void Awake()
    {
        cCon = GetComponent<CharacterController>();
    }

    void Start()
    {
        maincam = Camera.main;
    }

    
    void Update()
    {
        checkGround();
        moving();
        jumping();
        rotation();
        checkGravity();
        checkSlope();

        functionMouseLock();
        checkZoomView();

    }
    private void checkGround()
    {
        //isGround = cCon.isGrounded; // 캐릭터콘트롤러에는 땅체크 기능도 들어가있음 하지만 버전별로 안됨

        isGround = false;
        if (ver_Velocity <= 0)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down, cCon.height * 0.6f, LayerMask.GetMask("Ground"));
        }
    }
    private void moving()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")) * moveSpeed * Time.deltaTime;
        if (isSlope == true)
        {
            cCon.Move(-slopeVelocity);
        }
        else 
        {
            cCon.Move(transform.rotation * moveDir);// 로테이션을 안곱해주면 월드좌표계로 움직이게 됨        
        }
        
    }
    private void jumping()
    {
        if (isGround == false || isSlope == true)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
    }
    private void rotation()
    {
        //float mouseX = Input.GetAxisRaw("Mouse X") * mouseSense * Time.deltaTime;
        //float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSense * Time.deltaTime;

        //rotateValue.x += -mouseY;
        //rotateValue.y += mouseX;
        ////rotateValue += new Vector3(-mouseY, mouseX, 0); // 이렇게도 가능

        ////rotateValue.x = Mathf.Clamp(rotateValue.x, -45, 45);
        //transform.rotation = Quaternion.Euler(0, rotateValue.y, 0);

        // 아래부터는 시네머신용 코드
        transform.rotation = Quaternion.Euler(0f, maincam.transform.eulerAngles.y, 0f);
    }
    private void checkGravity()
    {
        if (isGround == true)
        {
            ver_Velocity += gravity * Time.deltaTime;
        }
        else
        {
            ver_Velocity -= gravity * Time.deltaTime;
        }

        if (isJump == true)
        {
            isJump = false;
            ver_Velocity = jumpForce;
        }

        cCon.Move(new Vector3(0, ver_Velocity, 0) * Time.deltaTime);
    }
    private void checkSlope()//이동불가, 점프불가 하도록
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, cCon.height * 0.75f))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            if (angle >= cCon.slopeLimit) // 경사가 높아서 미끄러져야함
            {
                isSlope = true; // 트루가 되면 미끌림
                slopeVelocity = Vector3.ProjectOnPlane(new Vector3(0, gravity, 0), hit.normal) * Time.deltaTime; // 경사각을 얻기위한 함수
            }
            else
            {
                isSlope = false;
            }
        }        
    }
    private void functionMouseLock() 
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) 
        {
            if (Cursor.lockState == CursorLockMode.Locked) //화면에 나오지 않는 상태였다면
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else 
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    private void checkZoomView()
    {
        if (Input.GetMouseButton(1) && _3rdCam.activeSelf == true) // _3rdCam.activeSelf => gameObject가 on/off 확인하는 bool 값
        {
            _3rdCam.SetActive(false);
        }
        else if (Input.GetMouseButton(1) == false && _3rdCam.activeSelf == false)
        {
            _3rdCam.SetActive(true);
        }
    }
}