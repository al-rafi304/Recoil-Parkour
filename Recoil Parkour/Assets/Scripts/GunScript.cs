using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public PlayerScript playerScript;
    public Transform gun;
    public Transform cameraPos;

    public LayerMask shotableLayer;
    public float gunRange;
    public float gunForce;

    public float fireRate;
    private float nextFire;

    Vector3 forceTemp = Vector3.zero;

    void Start()
    {

    }

    void Update()
    {
        Debug.DrawRay(cameraPos.position, cameraPos.forward * gunRange, Color.red);

        if(Input.GetMouseButtonDown(0) && Time.time > nextFire)
        {
            Shoot();
            nextFire = Time.time + fireRate;
        }

        forceTemp = Vector3.Lerp(forceTemp, Vector3.zero, 1 * Time.deltaTime);
    }

    public void Shoot()
    {
        RaycastHit hit;
        

        if(Physics.Raycast(cameraPos.position, cameraPos.forward, out hit, gunRange, shotableLayer))
        {
            Vector3 dir;
            dir = gun.forward * gunForce; //Calculates opposite direction of what player is facing
            playerScript.velocity = -dir; //Applies above force to player
            Debug.Log("Shot");
        }
    }
}
