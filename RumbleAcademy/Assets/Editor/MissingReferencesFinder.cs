using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Linq;

public static class CheckMissingReferences
{
    [MenuItem("Custom/Show Missing Object References in all scenes")]
    public static void MissingSpritesInAllScenes()
    {
        var valid = true;
        foreach (var scene in EditorBuildSettings.scenes.Where(s => s.enabled))
        {
            EditorSceneManager.OpenScene(scene.path);
            var objects = Object.FindObjectsOfType<GameObject>();
            var validFMR = FindMissingReferences(scene.path, objects);
            if (validFMR == false)
            {
                valid = false;
            }
        }
        if (valid == true)
        {
            //Debug.Log("All object references in all scene are OK".Colored(Colors.green));
            Debug.Log("All object references in all scene are OK");
        }
    }

    [MenuItem("Custom/Show Missing Object References in scene")]
    public static void FindMissingReferencesInCurrentScene()
    {
        var objects = Object.FindObjectsOfType<GameObject>();
        var valid = FindMissingReferences(EditorSceneManager.GetActiveScene().name, objects);
        if(valid == true)
        {
            //Debug.Log("All object references in scene are OK".Colored(Colors.green));
            Debug.Log("All object references in scene are OK");
        }
    }

    [MenuItem("Custom/Show Missing Object References in assets")]
    public static void MissingSpritesInAssets()
    {
        var allAssets = AssetDatabase.GetAllAssetPaths();
        var objs = allAssets.Select(a => AssetDatabase.LoadAssetAtPath(a, typeof(GameObject)) as GameObject).Where(a => a != null).ToArray();

        var valid = FindMissingReferences("Project", objs);
        if (valid == true)
        {
            //Debug.Log("All object references in assets are OK".Colored(Colors.green));
            Debug.Log("All object references in assets are OK");
        }
    }

    public static bool FindMissingReferences(string sceneName, GameObject[] objects)
    {
        var allValid = true;
        foreach (var go in objects)
        {
            var components = go.GetComponents<Component>();
            

            foreach (var c in components)
            {
                var so = new SerializedObject(c);
                var sp = so.GetIterator();

                while (sp.NextVisible(true))
                {
                    if (sp.propertyType == SerializedPropertyType.ObjectReference)
                    {
                        if (sp.objectReferenceValue == null && sp.objectReferenceInstanceIDValue != 0)
                        {
                            allValid = false;
                            ShowError(FullObjectPath(go), c, sp.name, sceneName);
                        }
                    }
                }
                var animator = c as Animator;
                if (animator != null)
                {
                    CheckAnimatorReferences(animator);
                }
            }
        }
        return allValid;
    }

    public static void CheckAnimatorReferences(Animator component)
    {
        if (component.runtimeAnimatorController == null)
        {
            return;
        }
        foreach (AnimationClip ac in component.runtimeAnimatorController.animationClips)
        {
            var so = new SerializedObject(ac);
            var sp = so.GetIterator();

            while (sp.NextVisible(true))
            {
                if (sp.propertyType == SerializedPropertyType.ObjectReference)
                {
                    if (sp.objectReferenceValue == null && sp.objectReferenceInstanceIDValue != 0)
                    {
                        Debug.LogError("Missing reference found in: " + FullObjectPath(component.gameObject) + "Animation: " + ac.name + ", Property : " + sp.name + ", Scene: " + EditorSceneManager.GetActiveScene().name);
                    }
                }
            }
        }
    }

    static void ShowError(string objectName, Component comp, string propertyName, string sceneName)
    {
        Debug.LogError("Missing reference found in: " + objectName + ", Component : " + comp +  ", Property : " + propertyName + ", Scene: " + sceneName);
    }

    static string FullObjectPath(GameObject go)
    {
        return go.transform.parent == null ? go.name : FullObjectPath(go.transform.parent.gameObject) + "/" + go.name;
    }
}