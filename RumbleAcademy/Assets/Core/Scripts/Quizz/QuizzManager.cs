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
    public Text RepGr1;
    public Text RepGr2;

    private int goodResp;
    private bool goodRespGr1;
    private bool goodRespGr2;
    private bool haveRepGr1;
    private bool haveRepGr2;

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
        
        getQuestion(0);

        goodRespGr1 = false;
        goodRespGr2 = false;
        
    }


    void getQuestion(int numQuestion)
    {
        haveRepGr1 = false;
        haveRepGr2 = false;
        RepGr1.gameObject.SetActive(false);
        RepGr2.gameObject.SetActive(false);
        goodResp = Int32.Parse(myQuizz[numQuestion][6]);

        questionText.text = myQuizz[numQuestion][1];
        reponseA.text = myQuizz[numQuestion][2];
        reponseB.text = myQuizz[numQuestion][3];
        reponseC.text = myQuizz[numQuestion][4];
        reponseD.text = myQuizz[numQuestion][5];
    }

    private void Update()
    {
        if(haveRepGr1 == false)
        {
            if (Input.GetButtonDown("Player1_ButtonX"))
            {
                RepGr1.gameObject.SetActive(true);
                haveRepGr1 = true;
                if (goodResp == 1)
                {
                    goodRespGr1 = true;
                    RepGr1.text = "Vrai";
                }
                else
                {
                    RepGr1.text = "Faux";
                }
            }
            if (Input.GetButtonDown("Player1_ButtonY"))
            {
                RepGr1.gameObject.SetActive(true);
                haveRepGr1 = true;
                if (goodResp == 2)
                {
                    goodRespGr1 = true;
                    RepGr1.text = "Vrai";
                }
                else
                {
                    RepGr1.text = "Faux";
                }
            }
            if (Input.GetButtonDown("Player1_ButtonA"))
            {
                RepGr1.gameObject.SetActive(true);
                haveRepGr1 = true;
                if (goodResp == 3)
                {
                    goodRespGr1 = true;
                    RepGr1.text = "Vrai";
                }
                else
                {
                    RepGr1.text = "Faux";
                }
            }
            if (Input.GetButtonDown("Player1_ButtonB"))
            {
                RepGr1.gameObject.SetActive(true);
                haveRepGr1 = true;
                if (goodResp == 4)
                {
                    goodRespGr1 = true;
                    RepGr1.text = "Vrai";
                }
                else
                {
                    RepGr1.text = "Faux";
                }
            }
        }
        if (haveRepGr2 == false)
        {
            if (Input.GetButtonDown("Player3_ButtonX"))
            {
                RepGr2.gameObject.SetActive(true);
                haveRepGr2 = true;
                if (goodResp == 1)
                {
                    goodRespGr2 = true;
                    RepGr2.text = "Vrai";
                }
                else
                {
                    RepGr1.text = "Faux";
                }
            }
            if (Input.GetButtonDown("Player3_ButtonY"))
            {
                RepGr2.gameObject.SetActive(true);
                haveRepGr2 = true;
                if (goodResp == 2)
                {
                    goodRespGr2 = true;
                    RepGr2.text = "Vrai";
                }
                else
                {
                    RepGr1.text = "Faux";
                }
            }
            if (Input.GetButtonDown("Player3_ButtonA"))
            {
                RepGr2.gameObject.SetActive(true);
                haveRepGr2 = true;
                if (goodResp == 3)
                {
                    goodRespGr2 = true;
                    RepGr2.text = "Vrai";
                }
                else
                {
                    RepGr1.text = "Faux";
                }
            }
            if (Input.GetButtonDown("Player3_ButtonB"))
            {
                RepGr2.gameObject.SetActive(true);
                haveRepGr2 = true;
                if (goodResp == 4)
                {
                    goodRespGr2 = true;
                    RepGr2.text = "Vrai";
                }
                else
                {
                    RepGr1.text = "Faux";
                }
            }
        }
    }
}