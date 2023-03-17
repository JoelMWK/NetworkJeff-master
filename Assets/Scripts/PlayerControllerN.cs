using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerControllerN : NetworkBehaviour
{

    [SerializeField]
    float movementSpeed = 3f; // Unity-enheter per sekund

    [SerializeField]
    float rotationSpeed = 150f; // Grader per sekund

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform bulletSpawnPoint;

    [SerializeField]
    float timeBetweenShots = 0.5f;
    float timeSinceLastShot = 0f;

    [SerializeField]
    GameObject Camera;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (!isLocalPlayer) Camera.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        float yRotation = Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime;
        float zMovement = Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime;

        Vector3 rotationVector = new Vector3(0, yRotation, 0);
        Vector3 movementVector = new Vector3(0, 0, zMovement);

        transform.Rotate(rotationVector);
        transform.Translate(movementVector);

        timeSinceLastShot += Time.deltaTime;

        if (isLocalPlayer)
        {
            if (Input.GetAxisRaw("Fire1") > 0)
            {
                if (timeSinceLastShot > timeBetweenShots)
                {
                    CmdFire();
                    timeSinceLastShot = 0;
                }
            }
        }
    }
    public override void OnStartLocalPlayer()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }

    [Command]
    void CmdFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        NetworkServer.Spawn(bullet);
    }
}
