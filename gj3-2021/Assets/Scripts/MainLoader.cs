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

    private void Update()
    {
        //temp data reset
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("reset gamedata, reloading...");
            SaveSystem.SaveGame(new GameData(levelBtns.Length));
            GameManager.instance.BackToMain();
        }
    }

    public void LoadLevelButtons(GameData data)
    {
        for (int i = 0; i < levelBtns.Length; i++)
        {
            if (data.levels[i].unlocked)
            {
                Debug.Log("level " + i + " unlocked");

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
        mainPanel.SetActive(!enable);
    }

    public void LoadLevel(int level)
    {
        GameManager.instance.LoadLevel(level);
    }
}
