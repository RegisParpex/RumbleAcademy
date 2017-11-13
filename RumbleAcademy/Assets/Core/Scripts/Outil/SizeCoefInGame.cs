using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeCoefInGame : MonoBehaviour {

    public static SizeCoefInGame Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Vector2 screenSizeRef = new Vector2(1920f, 1080f);

    public float originalRatio = 1920f / 1080f;

    public float GetSizeCoeff()
    {
        float currentScreenRatio = (float)Screen.width / (float)Screen.height;
        float ratio = currentScreenRatio / originalRatio;
        
        return ratio;
    }
}
