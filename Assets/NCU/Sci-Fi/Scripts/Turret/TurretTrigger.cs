using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTrigger : MonoBehaviour
{
    private Turret turret;
    private Vector3 LookAtPlayer;
    private RaycastHit hit;
    private void Start()
    {
        turret = transform.GetComponentInParent<Turret>();
        LookAtPlayer = new Vector3(0, turret.transform.position.y, 0);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.DrawRay(transform.parent.position, other.gameObject.transform.position-transform.parent.position , Color.red,1f);
            if(Physics.Raycast(transform.parent.position, other.gameObject.transform.position-transform.parent.position ,out hit))
            {
                if(hit.transform.gameObject==other.gameObject)
                {
                    LookAtPlayer.x = other.transform.position.x;
                    LookAtPlayer.z = other.transform.position.z;
                    turret.transform.LookAt(LookAtPlayer);
                    if (!turret.Bullet.activeInHierarchy)
                        turret.SpawnBullet = true;
                }
            }
        }
    }
}
