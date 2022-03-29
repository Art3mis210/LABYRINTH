using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int levelId;
    Transform levelParent;
    GameObject player;
    private const string levelParentTag = "Level";
    private const string playerTag = "Player";
    void Start()
    {
        levelId = PlayerPrefs.GetInt("LevelID", 0);
        levelParent = GameObject.FindGameObjectWithTag(levelParentTag).transform;
        player = GameObject.FindGameObjectWithTag(playerTag);
        LoadLevel(levelId);
        player.SetActive(true);
    }

    private void LoadLevel(int levelId)
    {
        GameObject levelObject = Instantiate(Resources.Load<GameObject>("Levels/Level_" + levelId));
        levelObject.transform.SetParent(levelParent);
    }

}
