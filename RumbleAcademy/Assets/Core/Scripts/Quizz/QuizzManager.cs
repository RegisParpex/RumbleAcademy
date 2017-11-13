using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class QuizzManager : MonoBehaviour {

    public List<List<string>> myQuizz = new List<List<string>>();
    private string line;
    private string[] parts;

    public Text questionText;
    public Text reponseA;
    public Text reponseB;
    public Text reponseC;
    public Text reponseD;
    public Text GoodGr1;
    public Text GoodGr2;

    private int goodResp;
    private int respGr1;
    private int respGr2;
    private bool goodRespGr1;
    private bool goodRespGr2;
    private bool oneResp;

    private int numActualQuestion;

    // Use this for initialization
    void Start()
    {
        TextAsset PrnFile = Resources.Load("quizz") as TextAsset; // ATTENTION ! Ne pas mettre l'extension du fichier !!
        string text = PrnFile.text;
        StringReader strReader = new StringReader(text);

        while ((line = strReader.ReadLine()) != null)
        {
            if (!line.StartsWith("#"))
            {
                parts = line.Split(new[] { '|' });

                myQuizz.Add(new List<string>
                {
                    parts[0].Trim(), // Categorie
                    parts[1].Trim(), // Question
                    parts[2].Trim(), // Réponse A
                    parts[3].Trim(), // Réponse B
                    parts[4].Trim(), // Réponse C
                    parts[5].Trim(), // Réponse D
                    parts[6].Trim()  // Réponse Juste
                });
            }
        }

        strReader.Close();

        numActualQuestion = 0;
        getQuestion(numActualQuestion);
    }


    void getQuestion(int numQuestion)
    {
        
        GoodGr1.gameObject.SetActive(false);
        GoodGr2.gameObject.SetActive(false);
        goodResp = Int32.Parse(myQuizz[numQuestion][6]);

        questionText.text = myQuizz[numQuestion][1];
        reponseA.text = myQuizz[numQuestion][2];
        reponseB.text = myQuizz[numQuestion][3];
        reponseC.text = myQuizz[numQuestion][4];
        reponseD.text = myQuizz[numQuestion][5];
    }

    void nextQuestion()
    {
        GoodGr1.gameObject.SetActive(false);
        GoodGr2.gameObject.SetActive(false);

        respGr1 = 0;
        respGr2 = 0;
        numActualQuestion++;
        getQuestion(numActualQuestion);
    }

    void Update()
    {
        if(respGr1 == 0 && !oneResp)
        {
            if (InputManager.Player1_A())
            {
                respGr1 = 1;
                oneResp = true;
            }
            if (InputManager.Player1_B())
            {
                respGr1 = 2;
                oneResp = true;
            }
            if (InputManager.Player1_X())
            {
                respGr1 = 3;
                oneResp = true;
            }
            if (InputManager.Player1_Y())
            {
                respGr1 = 4;
                oneResp = true;
            }
        }
           

        if (respGr2 == 0 && !oneResp)
        {
            if (InputManager.Player2_A())
            {
                respGr2 = 1;
                oneResp = true;
            }
            if (InputManager.Player2_B())
            {
                respGr2 = 2;
                oneResp = true;
            }
            if (InputManager.Player2_X())
            {
                respGr2 = 3;
                oneResp = true;
            }
            if (InputManager.Player2_Y())
            {
                respGr2 = 4;
                oneResp = true;
            }
        }

        if(respGr1 != 0)
        {
            if (respGr1 == goodResp && oneResp)
            {
                oneResp = false;
                StartCoroutine(GoodResponse(1));
            }
        }


        if (respGr2 != 0)
        {
            if (respGr2 == goodResp && oneResp)
            {
                oneResp = false;
                StartCoroutine(GoodResponse(2));
            }
        }
    }


    IEnumerator GoodResponse(int GrGoodResponse)
    {
        if (GrGoodResponse == 1)
        {
            GoodGr1.gameObject.SetActive(true);
        }
        else
        {
            GoodGr2.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(2);

        nextQuestion();
    }
}