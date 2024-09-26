using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class EnemyFollow : MonoBehaviour
{
    public GameObject xrOrigin;
    public float speed = 2.0f;
    public float stopDistance = 0f;
    public XRBaseController leftController;

    private Transform playerCamera;
    private Rigidbody rb;
    private bool isDead = false;

    private void Start()
    {
        playerCamera = xrOrigin.GetComponentInChildren<Camera>().transform;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void Update()
    {
        if (isDead)
        {
            // Si está muerto, no sigue al jugador
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerCamera.position);

        if (distanceToPlayer > stopDistance)
        {
            Vector3 direction = (playerCamera.position - transform.position).normalized;
            direction.y = 0;

            transform.position += direction * speed * Time.deltaTime;
            
            Vector3 lookAtPosition = new Vector3(playerCamera.position.x, transform.position.y, playerCamera.position.z);
            transform.LookAt(lookAtPosition);
        }
        
        DetectControllerInput();
    }
    private void DetectControllerInput()
    {
        InputDevice leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        if (leftHandDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed) && triggerPressed)
        {
            Debug.Log("Has presionado el Gatillo");
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        if (!isDead)
        {
            isDead = true; 

            rb.useGravity = true;
            rb.isKinematic = false;
            Debug.Log("El pibe esta muerto");

            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
    }

}

