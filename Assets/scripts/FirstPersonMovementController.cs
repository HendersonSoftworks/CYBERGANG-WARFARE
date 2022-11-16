using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovementController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] CharacterController controller;

    public float x;
    public float z;

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);    
    }
}
