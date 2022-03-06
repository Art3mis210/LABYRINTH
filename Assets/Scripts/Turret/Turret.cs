using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject Bullet;
    public float BulletResetTime;
    public GameObject Muzzle;
    public float BulletTime;
    public bool SpawnBullet;
    private void Update()
    {
        if((Input.GetKey(KeyCode.Space) || SpawnBullet==true) && Bullet.activeInHierarchy==false)
        {
            SpawnBullet = false;
            Bullet.transform.position = Muzzle.transform.position;
            Bullet.transform.rotation = Quaternion.Euler(Muzzle.transform.rotation.eulerAngles.x, Muzzle.transform.rotation.eulerAngles.y, Muzzle.transform.rotation.eulerAngles.z);
            Bullet.transform.parent = null;
            Bullet.SetActive(true);
        }
        if(Bullet.activeInHierarchy)
        {
            BulletTime += Time.deltaTime;
            if(BulletTime>=BulletResetTime)
            {
                Bullet.SetActive(false);
                Bullet.transform.parent = transform;
                BulletTime = 0;
            }
        }
    }
}
