using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainLoader : MonoBehaviour
{
    public GameObject levelsPanel;
    public GameObject mainPanel;

    [Header("LevelButtons")]
    public Button[] levelBtns;

    void Start()
    {
        Time.timeScale = 1;

        GameData data = SaveSystem.LoadGame();
        if (data == null)
        {
            Debug.Log("Creating new save data...");
            data = new GameData(levelBtns.Length);
            SaveSystem.SaveGame(data);
        }
        else
        {
            Debug.Log("Loading save data...");
        }

        LoadLevelButtons(data);
    }

    public void ResetGameData()
    {
        Debug.Log("reset gamedata, reloading...");
        SaveSystem.SaveGame(new GameData(levelBtns.Length));
        GameManager.instance.BackToMain();
    }

    public void LoadLevelButtons(GameData data)
    {
        for (int i = 0; i < levelBtns.Length; i++)
        {
            if (data.levels[i].unlocked)
            {
                Debug.Log("level " + i + " unlocked");

                levelBtns[i].interactable = true;

                TextMeshProUGUI letterTxt = levelBtns[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                letterTxt.text = data.levels[i].grade.ToString();
                letterTxt.color = GameManager.instance.gradeToColor(data.levels[i].grade);
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
        mainPanel.SetActive(!enable);
    }

    public void LoadLevel(int level)
    {
        GameManager.instance.LoadLevel(level);
    }
}
