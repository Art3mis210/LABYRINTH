using System.Collections;
using UnityEditor;
using UnityEngine;
using System.IO;

public class LevelReader : MonoBehaviour
{
    static string path;
    [MenuItem("Window/Save Level")]
    public static void SaveLevel()
    {
        //Accessing the Level.
        //Level is a child of Level Parent with tag "Level"
        GameObject levelParent = GameObject.FindGameObjectWithTag("Level");
        //Recording all transformation under level parent
        Transform firstChild = levelParent.transform.GetChild(0);
        Level levelToSave = new Level();
        levelToSave.parentTransform = new LTransform(firstChild);
        levelToSave.children = new LTransform[firstChild.childCount];
        for (int i = 0; i < levelToSave.children.Length; i++)
        {
            levelToSave.children[i] = new LTransform(firstChild.GetChild(i));
        }
        Debug.Log("level " + JsonUtility.ToJson(levelToSave));

        Debug.Log(Application.streamingAssetsPath);
        path = Application.streamingAssetsPath + "/level.txt";
        if (!File.Exists(path))
            File.Create(path);
        File.WriteAllText(path, JsonUtility.ToJson(levelToSave));
    }
    [MenuItem("Window/Load Level")]
    public static void LoadLevel()
    {
        path = Application.streamingAssetsPath + "/level.txt";
        string data = File.ReadAllText(path);
        Debug.Log("Level " + data);
        Level level = JsonUtility.FromJson<Level>(data);
        GameObject levelParent = GameObject.FindGameObjectWithTag("Level");
        Debug.Log(level.parentTransform.GetPath());
        //GameObject ground = Instantiate(Resources.Load(level.parentTransform.GetPath()), levelParent.transform) as GameObject;
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
        //GameObject firstChild = GameObject.CreatePrimitive(PrimitiveType.Plane);
        //level.parentTransform.SetTransform(firstChild.transform);
    }
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

    public LTransform(Transform original)
    {
        position = original.position;
        eulerAngles = original.localEulerAngles;
        localScale = original.localScale;
        GameObject originalPrefab = PrefabUtility.GetCorrespondingObjectFromSource(original.gameObject);
        pathToPrefab = AssetDatabase.GetAssetPath(originalPrefab);
        Debug.Log(pathToPrefab);
    }

    public string GetPath()
    {
        string trimmedPath = pathToPrefab.Replace("Assets/Resources/", "");
        trimmedPath = trimmedPath.Replace(".prefab", "");;
        return trimmedPath;
    }

    public void SetTransform(Transform trans)
    {
        trans.position = position;
        trans.localEulerAngles = eulerAngles;
        trans.localScale = localScale;
    }
}