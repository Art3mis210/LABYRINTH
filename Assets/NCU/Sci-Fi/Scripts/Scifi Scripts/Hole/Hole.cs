using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hole : MonoBehaviour
{
    bool FallenInHole;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && FallenInHole == false)
        {
            FallenInHole = true;
            PlayerInHole(other.gameObject.transform, true);
        }
    }
    IEnumerator MoveTOHole(Transform Player, Vector3 Pos, float Duration)
    {
        float t = 0;
        while (t < Duration)
        {
            Player.position = Vector3.Lerp(transform.position, Pos, t / Duration);
            yield return null;
            t += Time.deltaTime;
        }
        PlayerInHole(Player, false);
    }
    void PlayerInHole(Transform Player, bool Status)
    {
        if (Status == true)
        {
            Player.GetComponent<Rigidbody>().isKinematic = true;
            Player.GetComponent<Collider>().isTrigger = true;
            Player.GetComponent<Player>().enabled = false;
            StartCoroutine(MoveTOHole(Player, transform.position - new Vector3(0, 10, 0), 1f));
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
