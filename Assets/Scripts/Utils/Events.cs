using UnityEngine.Events;

public class Events {

    // Global system events for:
    // 1. Animation fade complete
    // 2. Game state change
    
    [System.Serializable] public class EventFadeComplete : UnityEvent<bool> { }
    [System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }
}
