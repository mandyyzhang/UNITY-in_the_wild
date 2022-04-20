using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleRandomRBMovement : MonoBehaviour
{
    [SerializeField] private Vector3 randomMovementRange = Vector3.zero;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float rotateSpeed = 1f;

    [SerializeField] private float timeToNewMove = 5f;

    private Rigidbody rb;
    private Vector3 initialPositionCache;
    private Vector3 wantedPosition;
    private float elapsedTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPositionCache = transform.position;
        wantedPosition = initialPositionCache;
        elapsedTime = 0f;
    }

    private void Update()
    {
        if(elapsedTime >= timeToNewMove)
        {
            GetNewWantedPosition();
            elapsedTime = 0f;
        }
        elapsedTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void GetNewWantedPosition()
    {
        Vector3 randomV3 = new Vector3(Random.Range(-randomMovementRange.x, randomMovementRange.x), Random.Range(-randomMovementRange.y, randomMovementRange.y), Random.Range(-randomMovementRange.z, randomMovementRange.z));
        wantedPosition = initialPositionCache + randomV3;
    }

    private void HandleMovement()
    {
        Vector3 wantedDirection = (wantedPosition - transform.position);
        if(wantedDirection.magnitude > 1f)
        {
            wantedDirection = wantedDirection.normalized;
        }

        Vector3 movement = moveSpeed * Time.deltaTime * wantedDirection;
        rb.MovePosition(rb.position + movement);

        if(wantedDirection != Vector3.zero)
        {
            Quaternion wantedRot = Quaternion.LookRotation(wantedDirection);
            Quaternion smoothRot = Quaternion.Slerp(rb.rotation, wantedRot, Time.deltaTime * rotateSpeed);

            rb.rotation = smoothRot;
        }
    }
}
