using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class RaycastGun : MonoBehaviour
{
    public Camera playerCamera;
    public Transform laserOrigin;
    public float gunRange = 50f;
    public float fireRate = 0.2f;
    public float laserDuration = 0.05f;

    public LineRenderer laserLine;
    float fireTimer;

    void Awake()
    {
        //laserLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && fireTimer > fireRate)
        {
            fireTimer = 0;
            laserLine.SetPosition(0, laserOrigin.position);
            Vector3 rayOring = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if(Physics.Raycast(rayOring, playerCamera.transform.forward, out hit, gunRange))
            {
                laserLine.SetPosition(1, hit.point);
                Destroy(hit.transform.gameObject);
            }
            else
            {
                laserLine.SetPosition(1, rayOring + (playerCamera.transform.forward * gunRange));
            }
            StartCoroutine(ShoorLaser());
        }
    }

    IEnumerator ShoorLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}
