using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainLoader : MonoBehaviour
{
    public GameObject levelsPanel;

    [Header("LevelButtons")]
    public Button[] levelBtns;

    void Start()
    {
        GameData data = SaveSystem.LoadGame();
        if (data == null)
        {
            Debug.Log("Creating new save data...");
            data = new GameData();
        }
        else
        {
            Debug.Log("Loading save data...");
        }

        LoadLevelButtons(data);
    }

    public void LoadLevelButtons(GameData data)
    {
        for (int i = 0; i < levelBtns.Length; i++)
        {
            if (data.levels[i].unlocked)
            {
                levelBtns[i].interactable = true;
                if (data.levels[i].finishTime > 0)
                {
                    TextMeshProUGUI letterTxt = levelBtns[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                    char grade = GameManager.instance.levelGrade(data.levels[i].finishTime, data.levels[i].collectibleCount);
                    letterTxt.text = grade.ToString();
                    letterTxt.color = GameManager.instance.gradeToColor(grade);
                }
            }
            else
            {
                levelBtns[i].interactable = false;
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EnableLevelSelect(bool enable)
    {
        levelsPanel.SetActive(enable);
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
