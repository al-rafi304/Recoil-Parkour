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

    float timer = 0f;
    Vector3 forceTemp = Vector3.zero;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(gun.position, gun.forward * gunRange, Color.red);

        // playerScript.velocity += forceTemp;

        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        forceTemp = Vector3.Lerp(forceTemp, Vector3.zero, 1 * Time.deltaTime);
    }

    public void Shoot()
    {
        RaycastHit hit;
        

        if(Physics.Raycast(gun.position, gun.forward, out hit, gunRange, shotableLayer))
        {
            Vector3 dir;
            dir = gun.forward * gunForce;
            playerScript.velocity = -dir;
            Debug.Log("Shot");
        }
    }
}
