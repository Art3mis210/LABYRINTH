using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTrigger : MonoBehaviour
{
    private Turret turret;
    private Vector3 LookAtPlayer;
    private void Start()
    {
        turret = transform.GetComponentInParent<Turret>();
        LookAtPlayer = new Vector3(0, turret.transform.position.y, 0);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !turret.Bullet.activeInHierarchy)
        {
            LookAtPlayer.x = other.transform.position.x;
            LookAtPlayer.z = other.transform.position.z;
            turret.transform.LookAt(LookAtPlayer);
            turret.SpawnBullet = true;
        }
    }
}
