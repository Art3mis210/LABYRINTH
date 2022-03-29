using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPoint : MonoBehaviour
{
    public ParticleSystem GoalParticle;
    public SpriteRenderer GoalSprite;
    void Update()
    {
        GoalSprite.transform.Rotate(0, 0, 60 * Time.deltaTime);
    }
}
