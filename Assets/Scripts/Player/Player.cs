using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody playerRigidbody;
    private SphereCollider playerCollider;
    InputModule currentModule;
    bool vibrationEnabled;
    Vector2 input;
    Vector3 moveBall;
    bool isGrounded;
    bool fallenInHole;
    void Start()
    {
        moveBall = Vector3.zero;
        playerCollider = GetComponent<SphereCollider>();
        playerRigidbody = GetComponent<Rigidbody>();
        #if UNITY_STANDALONE 
                currentModule = new KeyboardInput();
        #elif UNITY_ANDROID || UNITY_IOS
                currentModule = new MobileInput();
                vibrationEnabled = true;
        #else
                currentModule = new KeyboardInput();
        #endif

    }
    void FixedUpdate()
    {
        if (isGrounded)
        {
            input = currentModule.GetInput();
            moveBall.x = input.x;
            moveBall.z = input.y;
            playerRigidbody.velocity = moveBall * speed *input.magnitude;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && !fallenInHole)
            isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && !fallenInHole)
            isGrounded = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag=="Hole" && !fallenInHole)
        {
            playerRigidbody.isKinematic = true;
            playerCollider.isTrigger = true;
            fallenInHole = true;
            isGrounded = false;
            StartCoroutine(MoveTOHole(new Vector3(other.transform.position.x, other.transform.position.y-5, other.transform.position.z), 2f));
            
        }
    }
    IEnumerator MoveTOHole(Vector3 Pos,float Duration)
    {
        float t = 0;
        while(t<Duration)
        {
            transform.position = Vector3.Lerp(transform.position, Pos,t/Duration);
            yield return null;
            t += Time.deltaTime;
            if(vibrationEnabled)
                Handheld.Vibrate();
        }
        Invoke("ReloadScene", 2f);
    }
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
