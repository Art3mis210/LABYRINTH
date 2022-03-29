using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoalPoint : MonoBehaviour
{
    public ParticleSystem GoalParticle;
    public SpriteRenderer GoalSprite;
    bool ReachedGoal;
    void Update()
    {
        GoalSprite.transform.Rotate(0, 0, 60 * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && ReachedGoal == false)
        {
            ReachedGoal = true;
            PlayerInGoal(other.gameObject.transform, true);
        }
    }
    IEnumerator MoveTOPortal(Transform Player, Vector3 Pos, float Duration)
    {
        GoalParticle.Play();
        float t = 0;
        while (t < Duration)
        {
            Player.position = Vector3.Lerp(transform.position, Pos, t / Duration);
            yield return null;
            t += Time.deltaTime;
        }
        PlayerInGoal(Player, false);
        ReachedGoal = false;
    }
    void PlayerInGoal(Transform Player, bool Status)
    {
        if (Status == true)
        {
            Player.GetComponent<Rigidbody>().isKinematic = true;
            Player.GetComponent<Collider>().isTrigger = true;
            Player.GetComponent<Player>().enabled = false;
            StartCoroutine(MoveTOPortal(Player, transform.position - new Vector3(0, 10, 0), 1f));
        }
        else
        {
            PlayerPrefs.SetInt("LevelID", (PlayerPrefs.GetInt("LevelID", 0) + 1) % 5);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
}
