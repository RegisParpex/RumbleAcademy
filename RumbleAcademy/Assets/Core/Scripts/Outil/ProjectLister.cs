using System;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class ProjectLister : MonoBehaviour
{
    private GameObject[] m_allGameObjects;
    
    void Start()
    {
        #if (UNITY_EDITOR)
        DateTime dateNow = DateTime.Now;

        if (!File.Exists("ProjectLister"))
        {
            Directory.CreateDirectory("ProjectLister");
        }
        //Pass the filepath and filename to the StreamWriter Constructor
        //StreamWriter myFile = new StreamWriter("ProjectLister/ProjectLister_" + dateNow.Day + dateNow.Month.ToString("d2") + ".txt");
        StreamWriter myFile = new StreamWriter("ProjectLister/ProjectLister.txt");

        m_allGameObjects = FindObjectsOfType<GameObject>();
        LookForParents(myFile, m_allGameObjects);
        
        //Close the file
        myFile.Close();
        #endif
    }

    private void LookForParents(StreamWriter file, GameObject[] allGameObjects)
    {
        foreach (GameObject go in allGameObjects)
        {
            if (go.transform.parent == null)
            {
                file.WriteLine(go.name);
                LookForComponents(file, go.transform);
                LookForChildren(file, go.transform);
            }
        }
    }

    private void LookForChildren(StreamWriter file, Transform _trans, string _str = "    ")
    {
        for (int i = 0; i < _trans.childCount; i++)
        {
            file.WriteLine(_str + "> " + _trans.GetChild(i) + Environment.NewLine + _str + "Components :");
            LookForComponents(file, _trans.GetChild(i));
            _str += "    ";
            LookForChildren(file, _trans.GetChild(i));
            _str = "    ";
        }
    }

    private void LookForComponents(StreamWriter file, Transform _trans, string _str = "    ")
    {
        foreach (Component compo in _trans.GetComponents<Component>())
        {
            if (compo != null)
            {
                Type type = compo.GetType();
                
                if (type.BaseType == typeof(MonoBehaviour))
                {
                    //MonoBehaviour compoScript = (MonoBehaviour)compo;
                    file.WriteLine(_str + "- " + compo + " - Script");
                    // TODO : Identifier le path des script. Problème -> Je n'arrive pas a identifié que le component est un Script
                }
                else
                {
                    file.WriteLine(_str + "- " + compo);
                }
                switch (type.ToString())
                {
                    case "UnityEngine.SpriteRenderer":
                        SpriteRenderer compoSprite = (SpriteRenderer)compo;
                        if (compoSprite.sprite != null)
                        {
                            file.WriteLine(_str + _str + "- " + AssetDatabase.GetAssetPath(compoSprite.sprite));
                        }
                        break;
                    case "UnityEngine.AudioSource":
                        AudioSource compoAudio = (AudioSource)compo;
                        if (compoAudio.clip != null)
                        {
                            file.WriteLine(_str + _str + "- " + AssetDatabase.GetAssetPath(compoAudio.clip));
                        }
                        break;
                    case "UnityEngine.UI.Image":
                        Image imageUI = (Image)compo;
                        if (imageUI != null)
                        {
                            file.WriteLine(_str + _str + "- " + AssetDatabase.GetAssetPath(imageUI.sprite));
                        }
                        break;
                }
                LookForProperties(file, compo);
            }
            else
            {
                file.WriteLine(_str + " ! " + "Missing Component");
            }

        }
    }


    private void LookForProperties(StreamWriter file, Component comp, string _str = "    ")
    {
        foreach (FieldInfo fi in comp.GetType().GetFields())
        {
            System.Object obj = (System.Object)comp;
            file.WriteLine(_str + _str + "- " + "Property : " + fi.Name + " - Value :  " + fi.GetValue(obj));
            if(fi.GetValue(obj) != null)
            {
                Type type = fi.GetValue(obj).GetType();

                if (fi.GetValue(obj) is IList && type.IsGenericType)
                {
                        IList collection = (IList)fi.GetValue(obj);
                        //Type typeData = collection.GetType().GetProperty("Item").PropertyType;
                        foreach (var element in collection)
                        {
                            foreach (FieldInfo fieldI in element.GetType().GetFields())
                            {
                                file.WriteLine(_str + _str + _str + "- " + "Property : " + fieldI.Name + " - Value :  " + fieldI.GetValue(element));
                                string _strFI = "     ";
                                GetGameObjectOrAssetPath(file, _strFI, fieldI, element, fieldI.GetValue(element).GetType()); // Revoir système _str - ici manque 1 espace
                            }
                        }
                }

                GetGameObjectOrAssetPath(file, _str, fi, obj, type);
            }
        }
    }

    private static void GetGameObjectOrAssetPath(StreamWriter file, string _str, FieldInfo fi, object obj, Type type)
    {
        switch (type.ToString())
        {
            case "UnityEngine.UI.Text":
                Text textUI = (Text)fi.GetValue(obj);
                if (textUI != null)
                {
                    file.WriteLine(_str + _str + _str + "- " + GetGameObjectPath(textUI.gameObject));
                }
                break;
            case "UnityEngine.UI.Image":
                Image imageUI = (Image)fi.GetValue(obj);
                if (imageUI != null)
                {
                    file.WriteLine(_str + _str + _str + "- " + GetGameObjectPath(imageUI.gameObject));
                }
                break;
            case "UnityEngine.GameObject":
                GameObject gameObjectElem = (GameObject)fi.GetValue(obj);
                if (gameObjectElem != null)
                {
                    if (gameObjectElem.activeInHierarchy)
                    {
                        file.WriteLine(_str + _str + _str + "- " + GetGameObjectPath(gameObjectElem));
                    }
                    else
                    {
                        file.WriteLine(_str + _str + _str + "- " + AssetDatabase.GetAssetPath(gameObjectElem));
                    }
                }
                break;
            case "UnityEngine.AudioClip":
                AudioClip audioObj = (AudioClip)fi.GetValue(obj);
                if (audioObj != null)
                {
                    file.WriteLine(_str + _str + _str + "- " + AssetDatabase.GetAssetPath(audioObj));
                }
                break;
            case "UnityEngine.AudioClip[]":
                foreach (AudioClip audioInObj in (AudioClip[])fi.GetValue(obj))
                {
                    file.WriteLine(_str + _str + _str + "- " + AssetDatabase.GetAssetPath(audioInObj));
                }
                break;
        }
    }

    public static string GetGameObjectPath(GameObject obj)
    {
        string path = "/" + obj.name;
        while (obj.transform.parent != null)
        {
            obj = obj.transform.parent.gameObject;
            path = "/" + obj.name + path;
        }
        return path;
    }


    //void LookForAttributes(/Component _comp/)
    //{
    //    // https://stackoverflow.com/questions/6637679/reflection-get-attribute-name-and-value-on-property
    //    //System.Type t = Bidon;
    //    ProjectLister b = new ProjectLister();
    //    PropertyInfo[] props = typeof(Bidon).GetProperties();

    //    //Debug.Log (typeof(Bidon).GetProperty("Name").GetCustomAttributes(false));

    //    Debug.Log("length : " + typeof(Bidon).GetProperties().Length);


    //    foreach (PropertyInfo pi in typeof(Bidon).GetProperties())
    //    {
    //        int i = 0;
    //        Debug.Log("pi : " + pi.Name);
    //        foreach (object mi in pi.GetCustomAttributes(true))
    //        {
    //            Debug.Log("         -> mi (" + i + ") : " + mi);
    //            i++;
    //        }
    //    }
    //}

}