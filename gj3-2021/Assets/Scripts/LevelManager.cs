using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager inst;

    public float startDelay;
    public CharacterMovement player;
    public CollectableCount collectableCounter;

    bool counting = false;
    public float gameTime = 0;

    public bool paused = false;
    public GameObject pauseScreen;

    [Header("End Screen")]
    public GameObject endPanel;
    public TextMeshProUGUI endTxt;
    public TextMeshProUGUI collectTxt;
    public TextMeshProUGUI timeTxt;
    public TextMeshProUGUI gradeTxt;
    public GameObject nextButton;

    void Start()
    {
        inst = this;

        player.canMove = false;
        Invoke("StartPlayer", startDelay);

        Time.timeScale = 1;
    }

    void Update()
    {
        if (counting) gameTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            PauseGame(paused);
        }
    }

    public void PauseGame(bool pause)
    {
        if (pause) Time.timeScale = 0;
        else Time.timeScale = 1;
        pauseScreen.SetActive(pause);
    }

    public void StartPlayer()
    {
        player.startMovement();
        counting = true;
    }

    public void EndLevel(bool success)
    {
        counting = false;

        endPanel.SetActive(true);

        GameData data = SaveSystem.LoadGame();
        int levelIndex = GameManager.instance.currLevelIndex;

        collectTxt.text = collectableCounter.Total.ToString();

        float endTime = Mathf.Round(gameTime * 100F) / 100F;
        timeTxt.text = endTime.ToString() + "s";

        char grade;

        if (success)
        {
            endTxt.text = "Level Complete!";
            
            if (data.levels.Length > levelIndex + 1)
            {
                data.levels[levelIndex + 1].unlocked = true;
                Debug.Log("next level unlocked");
            }
            else nextButton.SetActive(false);

            grade = GameManager.instance.levelGrade(gameTime, data.levels[levelIndex].sGradeTime, collectableCounter.Total);
            if(CompareGrade(data.levels[levelIndex].grade, grade))
            {
                // new grade is better, save data
                data.levels[levelIndex].collectibleCount = collectableCounter.Total;
                data.levels[levelIndex].finishTime = endTime;
                data.levels[levelIndex].grade = grade;

                SaveSystem.SaveGame(data);
            }
        }
        else
        {
            grade = 'F';
            endTxt.text = "Level Failed!";
            nextButton.SetActive(false);
        }

        gradeTxt.text = grade.ToString();
        gradeTxt.color = GameManager.instance.gradeToColor(grade);
    }

    public void NextLevel()
    {
        GameManager.instance.LoadLevel(GameManager.instance.currLevelIndex + 2);
    }

    public void RetryLevel()
    {
        GameManager.instance.LoadLevel(GameManager.instance.currLevelIndex + 1);
    }

    public void MainMenu()
    {
        GameManager.instance.BackToMain();
    }

    bool CompareGrade(char initial, char update)
    {
        switch (update)
        {
            case 'F': return false;
            case 'C':
                if (initial == 'S' || initial == 'A' || initial == 'B') return false;
                else return true;

            case 'B':
                if (initial == 'S' || initial == 'A') return false;
                else return true;
            case 'A':
                if (initial == 'S') return false;
                else return true;
            default: return true;
        }
    }
}
