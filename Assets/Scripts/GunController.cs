using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] GameObject objBullet;
    [SerializeField] Transform objDynamic;
    [SerializeField] float bulletForce = 100f;
    Camera mainCam;
    float aimDistance = 200f;
    Transform trsMuzzle;
    [SerializeField] bool gravityCannon = false;

    
    void Start()
    {
        mainCam = Camera.main;
        trsMuzzle = transform.Find("Gun/Muzzle");
    }

    
    void Update()
    {
        gunTarget();
        isShoot();
    }

    /// <summary>
    /// 총이 에이밍하는 부분으로 총구를 이동
    /// </summary>
    private void gunTarget()
    {
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out RaycastHit hit, aimDistance, LayerMask.GetMask("Ground")))
        {
            transform.LookAt(hit.point);
        }
        else// 아무것도 없을때 알아서 빈오브젝트를 생성하여 주시하는 코드
        {
            transform.LookAt(mainCam.transform.position + mainCam.transform.forward * aimDistance);            
        }
    }
    private void isShoot()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            shootBullet();
        }
    }
    private void shootBullet()
    {
        GameObject obj = Instantiate(objBullet, trsMuzzle.position, transform.rotation, objDynamic);
        Bullet sc = obj.GetComponent<Bullet>();

        if (gravityCannon == true)
        {
            sc.AddForce(bulletForce);
        }

        else if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out RaycastHit hit, aimDistance, LayerMask.GetMask("Ground")))
        {
            sc.SetDestination(hit.point, bulletForce);            
        }
        else
        {
            sc.SetDestination(mainCam.transform.position + mainCam.transform.forward * aimDistance, bulletForce);
        }       
    }
}
