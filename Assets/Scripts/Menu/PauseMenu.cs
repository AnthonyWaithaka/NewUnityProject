using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    [SerializeField] Button ResumeButton;
    [SerializeField] Button QuitButton;

    private void Start() {
        ResumeButton.onClick.AddListener(HandleResumeClicked);
        QuitButton.onClick.AddListener(HandleQuitClicked);
    }

    void HandleResumeClicked() {
        GameManager.Instance.TogglePause();
    }

    void HandleQuitClicked() {
        GameManager.Instance.QuitToMainMenu();
    }
}
