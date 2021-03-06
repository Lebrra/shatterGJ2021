﻿using System.Collections;
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

    [Header("End Screen")]
    public GameObject endPanel;
    public TextMeshProUGUI endTxt;
    public TextMeshProUGUI gradeTxt;
    public GameObject nextButton;

    void Start()
    {
        inst = this;

        player.canMove = false;
        Invoke("StartPlayer", startDelay);
    }

    void Update()
    {
        if (counting) gameTime += Time.deltaTime;
    }

    public void StartPlayer()
    {
        player.canMove = true;
        counting = true;
    }

    public void EndLevel(bool success)
    {
        counting = false;

        endPanel.SetActive(true);

        GameData data = SaveSystem.LoadGame();
        int levelIndex = GameManager.instance.currLevelIndex;

        if (collectableCounter.Total > data.levels[levelIndex].collectibleCount)
            data.levels[levelIndex].collectibleCount = collectableCounter.Total;

        if (success)
        {
            if(gameTime < data.levels[levelIndex].finishTime)
                data.levels[levelIndex].finishTime = gameTime;
            
            endTxt.text = "Level Complete!";
            
            if (data.levels.Length > levelIndex + 1)
            {
                data.levels[levelIndex + 1].unlocked = true;
                Debug.Log("next level unlocked");
            }
            else nextButton.SetActive(false);
        }
        else
        {
            data.levels[levelIndex].finishTime = float.MaxValue;
            endTxt.text = "Level Failed!";
            nextButton.SetActive(false);
        }

        char grade = GameManager.instance.levelGrade(gameTime, collectableCounter.Total);
        gradeTxt.text = grade.ToString();
        gradeTxt.color = GameManager.instance.gradeToColor(grade);

        SaveSystem.SaveGame(data);
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
}