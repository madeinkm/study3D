using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    Vector3 destination = Vector3.zero;
    private float force = 0;
    private bool gravityCannon = false;

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) // 그라운드라는 레이어에 닿으면 삭제
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Destroy(gameObject,3.0f);
    }

    
    void Update()
    {
        if (gravityCannon == true)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, destination, force * Time.deltaTime);
    }

    public void SetDestination(Vector3 _destination, float _force)
    {
        destination = _destination;
        force = _force;
    }

    public void AddForce(float _force)
    {
        rb.useGravity = true;
        rb.AddForce(transform.rotation * Vector3.forward * _force, ForceMode.Impulse);
        gravityCannon = true;
    }
}
