using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private int levelIndex = 1;
    [SerializeField]
    private int modifier = 4;
    [SerializeField]
    private List<Color32> colours = new List<Color32>();
    [SerializeField]
    private Color32 winColour;
    [SerializeField]
    private bool colourSelected = false;
    [SerializeField]
    private float timePerCycle = 2.0f;
    [SerializeField]
    private int colourIndex = 0;
    [SerializeField]
    private int correctColourIndex = 0;
    [SerializeField]
    private int score = 0;
    [SerializeField]
    private int numColours = 0;
    // UI for colourPanel
    public Image colourPanel;
    
    // UI for HUD
    public Image colourToSelectImage;
    public TextMeshProUGUI colourHexText;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;

    // UI for Game Over
    public GameObject gameOverPanel;

    // UI for Main Menu

    private IEnumerator CycleColour()
    {
        while (!colourSelected)
        {
            if(Math.Floor(UnityEngine.Random.Range(0.0f,10)) <= 1) {

                colourIndex = correctColourIndex;
            }
            else
            {
                colourIndex++;
                if (colourIndex >= numColours)
                {
                    colourIndex = 0;
                }
            }
            colourPanel.color = colours[colourIndex];
            yield return new WaitForSeconds(timePerCycle);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Load HighScore First

        // Then run 
        StartRun();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            StopAllCoroutines();
            if(colourIndex == correctColourIndex)
            {
                score++;
                timePerCycle = timePerCycle * 0.85f;
                currentScoreText.text = "Score:" + score.ToString();
                Run();
                levelIndex++;
            }
            else
            {
                gameOverPanel.SetActive(true);
            }
        }
    }
    public void StartRun()
    {
        levelIndex = 1;
        score = 0;
        currentScoreText.text = "Score" + score;
        Run();
    }

    public void Run()
    {

        colours = new List<Color32>();

        numColours = levelIndex * modifier;

        for(int i = 0; i < numColours; i++)
        {
            colours.Add(new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 255));
        }

        correctColourIndex = (int)Math.Floor(UnityEngine.Random.Range(0.0f, numColours - 1));
        Debug.Log("numColours: " + numColours + "correctColourIndex: " + correctColourIndex);
        
        // Choose a winning colour
        winColour = colours[correctColourIndex];
        colourIndex = (int)Math.Floor(correctColourIndex / 2.0f);

        // Correct UI
        colourToSelectImage.color = winColour;


        StopAllCoroutines();
        StartCoroutine("CycleColour");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
