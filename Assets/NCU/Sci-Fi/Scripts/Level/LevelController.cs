using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    static string path;
    private void Start()
    {
        LoadLevel(PlayerPrefs.GetInt("LevelID", 0));
    }
    public static void LoadLevel(int levelId)
    {
        path = Application.streamingAssetsPath + "/level.txt";
        string data = File.ReadAllText(path);
        Debug.Log("Level " + data);
        LevelList levelList = JsonUtility.FromJson<LevelList>(data);
        Level level = levelList.levels[levelId];
        GameObject levelParent = GameObject.FindGameObjectWithTag("Level");
        Debug.Log(level.parentTransform.GetPath());
        GameObject ground = Instantiate(Resources.Load(level.parentTransform.GetPath()), levelParent.transform) as GameObject;
        level.parentTransform.SetTransform(ground.transform);
        for (int i = 0; i < level.children.Length; i++)
        {
            GameObject wall = Instantiate(Resources.Load(level.children[i].GetPath()), ground.transform) as GameObject;
            level.children[i].SetTransform(wall.transform);
        }
        level.parentTransform.SetTransform(ground.transform);
        for (int i = 0; i < level.children.Length; i++)
        {
            GameObject wall = Instantiate(Resources.Load(level.children[i].GetPath()), ground.transform) as GameObject;
            level.children[i].SetTransform(wall.transform);
        }
    }
}
[System.Serializable]
public class LevelList
{
    public Level[] levels;
}

[System.Serializable]
public class Level
{
    public LTransform parentTransform;
    public LTransform[] children;
}

[System.Serializable]
public class LTransform
{
    public Vector3 position;
    public Vector3 eulerAngles;
    public Vector3 localScale;
    //public PrimitiveType myType;
    public string pathToPrefab;

    public LTransform()
    {

    }
    #if UNITY_EDITOR
    public LTransform(Transform original)
    {
        position = original.position;
        eulerAngles = original.localEulerAngles;
        localScale = original.localScale;
        GameObject originalPrefab;
        originalPrefab = PrefabUtility.GetCorrespondingObjectFromSource(original.gameObject);
        pathToPrefab = AssetDatabase.GetAssetPath(originalPrefab);
        Debug.Log(pathToPrefab);
    }
    #endif
    public string GetPath()
    {
        string trimmedPath = pathToPrefab.Replace("Assets/Resources/", "");
        trimmedPath = trimmedPath.Replace(".prefab", ""); 
        return trimmedPath;
    }

    public void SetTransform(Transform trans)
    {
        trans.position = position;
        trans.localEulerAngles = eulerAngles;
        trans.localScale = localScale;
    }
}
