using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EnemyFollow : MonoBehaviour
{
    // Referencia al XR Origin
    public GameObject xrOrigin;

    // Velocidad a la que se moverá el enemigo
    public float speed = 2.0f;

    // Distancia mínima a la que el enemigo se detiene del jugador
    public float stopDistance = 1.0f;

    private Transform playerCamera;

    private void Start()
    {
        // Buscamos la cámara en el XR Origin (que es el punto de vista del jugador)
        playerCamera = xrOrigin.GetComponentInChildren<Camera>().transform;
    }

    private void Update()
    {
        // Obtenemos la distancia entre el enemigo y la cámara del jugador
        float distanceToPlayer = Vector3.Distance(transform.position, playerCamera.position);

        // Si la distancia es mayor que la distancia mínima de parada, el enemigo se mueve hacia el jugador
        if (distanceToPlayer > stopDistance)
        {
            // Calculamos la dirección hacia el jugador
            Vector3 direction = (playerCamera.position - transform.position).normalized;
            
            // Movemos al enemigo hacia el jugador
            transform.position += direction * speed * Time.deltaTime;
            
            // Orientamos al enemigo hacia el jugador
            transform.LookAt(new Vector3(playerCamera.position.x, transform.position.y, playerCamera.position.z));
        }
    }
}
