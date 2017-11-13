using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDownScript : MonoBehaviour {

    private int depth = -10;
    public float speedMovementCamera = 10;

    private GameObject player;
    private GameObject player2;

    void Update()
    {
        transform.Translate(speedMovementCamera * Time.deltaTime, 0, 0);

        /*
        if(player.transform.localPosition.x <= transform.localPosition.x - ((transform.localPosition.x / 100) * 25))
        {

        }
        */
    }
}