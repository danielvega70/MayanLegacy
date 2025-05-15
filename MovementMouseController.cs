using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMouseController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public LayerMask groundLayer; // Para que solo detecte el suelo

    private Rigidbody rb;
    private Camera mainCamera;
    private Vector3 targetPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>() ?? gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        mainCamera = Camera.main; // Obtener la cámara principal
    }

    private void Update()
    {
        UpdateTargetPosition();
        MoveTowardsTarget();
    }

    private void UpdateTargetPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            targetPosition = hit.point;
        }
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0; // Evitar movimientos verticales innecesarios

        if (direction.magnitude > 0.1f)
        {
            RotateCharacter(direction);
            rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
        }
    }

    private void RotateCharacter(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}