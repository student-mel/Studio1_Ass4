using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private Button tutLevelButton;
    [SerializeField] private Button levelSelectButton;
    [SerializeField] private Button level1Button;
    [SerializeField] private Button level2Button;
    [SerializeField] private Button level3Button;
    [SerializeField] private Button exitLevelSelectButton;
    [SerializeField] private Button exitButton;

    void Start()
    {
        mainMenuActivate(true);
        LevelSelectMenuActivate(false);
    }
    public void LevelSelectClicked()
    {
        mainMenuActivate(false);
        LevelSelectMenuActivate(true);
    }

    public void exitLevelSelect()
    {
        mainMenuActivate(true);
        LevelSelectMenuActivate(false);
    }

    public void tutLevelClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1); 
    }

    public void Level1Clicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
    public void Level2Clicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
    public void Level3Clicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(4);
    }
    public void QuitButtonClick()
    {
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void mainMenuActivate (bool activate)
    {
        tutLevelButton.gameObject.SetActive(activate);
        levelSelectButton.gameObject.SetActive(activate);
        exitButton.gameObject.SetActive(activate);
    }

    public void LevelSelectMenuActivate (bool activate)
    {
        level1Button.gameObject.SetActive(activate);
        level2Button.gameObject.SetActive(activate);
        level3Button.gameObject.SetActive(activate);
        exitLevelSelectButton.gameObject.SetActive(activate);
    }
}
