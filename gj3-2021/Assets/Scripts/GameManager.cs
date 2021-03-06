using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Color[] gradeColors;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public char levelGrade(float time, int collectibles)
    {
        return 'F';
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
