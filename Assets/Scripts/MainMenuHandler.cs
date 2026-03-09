using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    public void QuitButtonClick()
    {
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
