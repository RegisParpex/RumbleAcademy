using UnityEngine;
using UnityEditor;

public class ClearPlayerPrefs
{
    [MenuItem("Custom/Clear PlayerPrefs")]
    private static void OnClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}