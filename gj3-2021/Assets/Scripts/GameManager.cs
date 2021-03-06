using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currLevelIndex = -1;

    public Color[] gradeColors;

    void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(int level)
    {
        currLevelIndex = level - 1;
        SceneManager.LoadScene(level);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }

    public char levelGrade(float time, int collectibles)
    {
        if (time == float.MaxValue) return 'F';

        return 'C';
    }

    public Color gradeToColor(char grade)
    {
        switch (grade)
        {
            default:
                return Color.white;

            case 'S':
                return Color.yellow;

            case 'A':
                return Color.green;

            case 'B':
                return Color.cyan;

            case 'C':
                return Color.blue;

            case 'F':
                return Color.red;
        }
    }
}
