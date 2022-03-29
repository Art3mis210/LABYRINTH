using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScifiPlayerBehaviour : MonoBehaviour
{
    public float speed;
    private Rigidbody playerRigidbody;
    private SphereCollider playerCollider;
    private Player playerScript;
    bool isGrounded;
    bool fallenInHole;
    void Start()
    {
        playerCollider = GetComponent<SphereCollider>();
        playerRigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (isGrounded)
        {
            if (playerScript.enabled == false)
                playerScript.enabled = true;
        }
        else
        {
            if (playerScript.enabled == true)
                playerScript.enabled = false;
        }
    }
    private void OnCollisionStay(Collision collision)
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
        if (other.gameObject.tag == "Hole" && !fallenInHole)
        {
            playerRigidbody.isKinematic = true;
            playerCollider.isTrigger = true;
            fallenInHole = true;
            isGrounded = false;
            StartCoroutine(MoveTOHole(new Vector3(other.transform.position.x, other.transform.position.y - 5, other.transform.position.z), 2f));
        }
        if (other.gameObject.tag == "Portal" && !fallenInHole)
        {
            playerRigidbody.isKinematic = true;
            playerCollider.isTrigger = true;
            fallenInHole = true;
            isGrounded = false;
            StartCoroutine(MoveTOPortal(other.gameObject.GetComponentInParent<Portal>(), new Vector3(other.transform.position.x, other.transform.position.y - 10, other.transform.position.z), 1f));
        }
        if (other.gameObject.tag == "Goal" && !fallenInHole)
        {
            playerRigidbody.isKinematic = true;
            playerCollider.isTrigger = true;
            fallenInHole = true;
            isGrounded = false;
            StartCoroutine(MoveTOGoal(other.gameObject.GetComponentInParent<GoalPoint>(), new Vector3(other.transform.position.x, other.transform.position.y - 10, other.transform.position.z), 1f));
        }
    }
    IEnumerator MoveTOHole(Vector3 Pos, float Duration)
    {
        float t = 0;
        while (t < Duration)
        {
            transform.position = Vector3.Lerp(transform.position, Pos, t / Duration);
            yield return null;
            t += Time.deltaTime;
        }
        Invoke("ReloadScene", 2f);
    }
    IEnumerator MoveTOPortal(Portal portal, Vector3 Pos, float Duration)
    {
        portal.PortalParticleEffect.Play();
        float t = 0;
        while (t < Duration)
        {
            transform.position = Vector3.Lerp(transform.position, Pos, t / Duration);
            yield return null;
            t += Time.deltaTime;
        }

        transform.position = portal.PortalCamera.transform.position;
        playerRigidbody.isKinematic = false;
        playerCollider.isTrigger = false;
        fallenInHole = false;
        isGrounded = false;
    }
    IEnumerator MoveTOGoal(GoalPoint Goal, Vector3 Pos, float Duration)
    {
        Goal.GoalParticle.Play();
        float t = 0;
        while (t < Duration)
        {
            transform.position = Vector3.Lerp(transform.position, Pos, t / Duration);
            yield return null;
            t += Time.deltaTime;
        }
        PlayerPrefs.SetInt("LevelID", (PlayerPrefs.GetInt("LevelID", 0) + 1) % 5);
        ReloadScene();

    }
    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            ReloadScene();
            Destroy(gameObject);
        }
    }
}