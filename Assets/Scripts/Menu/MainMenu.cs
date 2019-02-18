using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    // 1. Track the animation component
    // 2. Track the animation clips for Fade In and Fade Out
    // 3. Receive animation events
    // 4. Play the animations

    [SerializeField] Animator _mainMenuAnimator;
    [SerializeField] AnimationClip _fadeOutAnimation;
    [SerializeField] AnimationClip _fadeInAnimation;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    private void Start() {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    public void OnFadeOutComplete() {
        Debug.Log("[MainMenu]: Fade out complete");
        OnMainMenuFadeComplete.Invoke(true);
    }

    public void OnFadeInComplete() {
        Debug.Log("[MainMenu]: Fade in complete");
        OnMainMenuFadeComplete.Invoke(false);
        UIManager.Instance.SetDummyCameraActive(true);
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState) {
        if(previousState == GameManager.GameState.PREGAME && currentState == GameManager.GameState.RUNNING) {
            FadeOut();
        }

        if(previousState != GameManager.GameState.PREGAME && currentState == GameManager.GameState.PREGAME) {
            FadeIn();
        }
    }

    public void FadeIn() {
        _mainMenuAnimator.SetTrigger("FadeIn");
    }

    public void FadeOut() {
        UIManager.Instance.SetDummyCameraActive(false);
        _mainMenuAnimator.SetTrigger("FadeOut");
    }
}
