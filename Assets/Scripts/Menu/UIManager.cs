using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

    [SerializeField] MainMenu _mainMenu;
    [SerializeField] PauseMenu _pauseMenu;
    [SerializeField] Camera _dummyCamera;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    private void Start() {
        // Bubbled up event handling from main menu
        _mainMenu.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    void HandleMainMenuFadeComplete(bool fadeOut) {
        Debug.Log("[UIManager] Fade " + (fadeOut ? "out" : "in") + " complete");
        OnMainMenuFadeComplete.Invoke(fadeOut);
    }

    private void Update() {
        if(GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            GameManager.Instance.StartGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameManager.Instance.ExitToSystem();
        }
    }

    // Responsibility for handling the dummy camera used to display the main menu
    public void SetDummyCameraActive(bool active) {
        _dummyCamera.gameObject.SetActive(active);
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState) {
        // Reveal the pause menu when the game state is PAUSED and disable it otherwise
        _pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSED);
    }
}
