using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlarCamara : MonoBehaviour
{
    public float sensibilidad = 100f;
    public Transform cuerpoJugador;
    public float distanciaCamara = 2f;
    float rotacionX = 0f;
    public float limiteInferior = -45f;
    public float limiteSuperior = 45f;

    void Start()
    {
        cuerpoJugador = GameObject.Find("PlayerArmature").transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;

        // Girar el cuerpo del jugador 360 grados
        cuerpoJugador.Rotate(Vector3.up * mouseX);

        // Control de la cámara
        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, limiteInferior, limiteSuperior);

        // Aplicar la rotación vertical a la cámara
        transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);

        // Mantener la cámara detrás del jugador
        Vector3 desiredPosition = cuerpoJugador.position + cuerpoJugador.rotation * new Vector3(0, 0, -distanciaCamara);
        transform.position = desiredPosition;

        // Hacer que la cámara siempre mire al jugador
        transform.LookAt(cuerpoJugador);
    }
}
