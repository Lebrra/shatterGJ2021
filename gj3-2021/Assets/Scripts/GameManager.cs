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

        if(collectibles == 3)
        {
            if (time < 15F) return 'S';
            else if (time < 20F) return 'A';
            else return 'B';
        }
        else if(collectibles == 2)
        {
            if (time < 15F) return 'A';
            else return 'B';
        }

        if (time < 15F) return 'B';
        else return 'C';
    }

    public Color gradeToColor(char grade)
    {
        switch (grade)
        {
            default:
                return Color.white;

            case 'S':
                return gradeColors[0];

            case 'A':
                return gradeColors[1];

            case 'B':
                return gradeColors[2];

            case 'C':
                return gradeColors[3];

            case 'F':
                return gradeColors[4];
        }
    }
}
