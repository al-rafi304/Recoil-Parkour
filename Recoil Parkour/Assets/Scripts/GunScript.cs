using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    public PlayerScript playerScript;
    public Transform gun;
    public Transform cameraPos;

    [Header("Shooting")]
    public GameObject bullet;
    public Transform shootingPos;

    public LayerMask shotableLayer;
    public Image fireRateIndicator;
    public float gunRange;
    public float gunForce;

    public float fireRate;
    private float nextFire;
    public float bulletSpeed;

    Vector3 forceTemp = Vector3.zero;

    void Start()
    {

    }

    void Update()
    {
        Debug.DrawRay(cameraPos.position, cameraPos.forward * gunRange, Color.red);

        if(Input.GetMouseButton(0) && Time.time > nextFire)
        {
            Shoot();
            
        }

        //Debug.Log(Time.time - nextFire);

        forceTemp = Vector3.Lerp(forceTemp, Vector3.zero, 1 * Time.deltaTime);

        fireRateIndicator.fillAmount = Mathf.Clamp01((nextFire - Time.time) / fireRate);
    }

    public void Shoot()
    {
        RaycastHit hit;
        GameObject bulletInstance;

        bulletInstance = Instantiate(bullet, shootingPos.position, Quaternion.identity);
        BulletScript bulletScript;
        bulletScript = bulletInstance.GetComponent<BulletScript>();
        bulletScript.AddForce(cameraPos.forward, bulletSpeed);
        
        //Destroy(bulletInstance, 5f);

        if(Physics.Raycast(cameraPos.position, cameraPos.forward, out hit, gunRange, shotableLayer))
        {
            Vector3 dir;
            dir = cameraPos.forward * gunForce; //Calculates opposite direction of what player is facing
            playerScript.velocity = -dir; //Applies above force to player

            

            Debug.Log("Shot");
        }

        nextFire = Time.time + fireRate;
    }
}
