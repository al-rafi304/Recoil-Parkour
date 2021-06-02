using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float speed;
    private Vector3 dir;

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    public void AddForce(Vector3 _dir, float _speed)
    {
        speed = _speed;
        dir = _dir;

        //Debug.Log("Called Bullet AddForce" + speed + dir);
    }

    private void OnEnable() 
    {
        Destroy(this.gameObject, 3f);    
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "respawn")
        {
            Destroy(this.gameObject);
            Debug.Log("Collided");
        }
        
    }
}
