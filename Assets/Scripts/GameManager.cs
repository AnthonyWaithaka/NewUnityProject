using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager> {

    // The GameManager should be able to:
    // 1. Keep track of what level the game is currently in
    // 2. Load and unload game levels
    // 3. Keep track of the game state
    // 4. Generate other persistent systems
    // 5. Enter and Exit pause mode

    // Keep track of prefabs that are supposed to persist at the Boot scene level
    public GameObject[] SystemPrefabs;

    public Events.EventGameState OnGameStateChanged;

    // Keep track of the created system prefabs
    public List<GameObject> _instancedSystemPrefabs;

    // A list of the active async operations
    List<AsyncOperation> _loadOperations;

    // Keep track of the game state
    public enum GameState {
        PREGAME,
        RUNNING,
        PAUSED
    }

    GameState _currentGameState = GameState.PREGAME;

    public GameState CurrentGameState {
        get { return _currentGameState; }
        private set { _currentGameState = value; }
    }

    private string _currentLevelName = string.Empty;


    private void Start() {
        // Exclude the attached GameObject from load/unload operations
        DontDestroyOnLoad(gameObject);

        // Initialize the list of async operation objects
        _loadOperations = new List<AsyncOperation>();

        // Instantiate the system level prefabs when the game starts
        InstantiateSystemPrefabs();

        UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
    }

    private void Update() {
        if (_currentGameState == GameState.PREGAME) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    void OnLoadOperationComplete(AsyncOperation ao) {

        // Remove from ao from list of active operations
        if (_loadOperations.Contains(ao)) {
            _loadOperations.Remove(ao);

            if(_loadOperations.Count == 0) {
                UpdateState(GameState.RUNNING);
            }
        }
        Debug.Log("[GameManager] Load Complete.");
    }

    void OnUnloadOperationComplete(AsyncOperation ao) {
        Debug.Log("[GameManager] Unload Complete");
    }

    void InstantiateSystemPrefabs() {
        GameObject prefabInstance;
        for(int i = 0; i < SystemPrefabs.Length; ++i) {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            _instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    void HandleMainMenuFadeComplete(bool fadeOut) {
        if (!fadeOut) {
            UnloadLevel(_currentLevelName);
        }
    }

    void UpdateState(GameState state) {

        GameState previousGameState = _currentGameState;
        _currentGameState = state;
        switch (_currentGameState) {
            case GameState.PREGAME:
                // Resume Simulations
                // Time.timeScale = 1.0f
                break;

            case GameState.RUNNING:
                // Resume Simulations
                // Time.timeScale = 1.0f
                if(previousGameState == GameState.PAUSED) {
                    Debug.Log("[GameManager] Game was resumed.");
                }
                break;

            case GameState.PAUSED:
                // Pause Simulations
                // Time.timeScale = 0.0f
                Debug.Log("[GameManager] Game was paused.");
                break;

            default:
                break;
        }

        // dispatch messages
        OnGameStateChanged.Invoke(_currentGameState, previousGameState);
        
        // transition between scenes, yadda yadda etc
    }

    public void LoadLevel(string levelName) {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        if(ao == null) {
            Debug.Log("[GameManager] Unable to load level " + levelName);
            return;
        }

        // ao.complete is a list of functions that are executed
        // only when the async operation is completed
        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);
        _currentLevelName = levelName;
    }

    public void UnloadLevel(string levelName) {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);

        if (ao == null) {
            Debug.Log("[GameManager] Unable to unload level " + levelName);
            return;
        }

        ao.completed += OnUnloadOperationComplete;
    }

    protected override void OnDestroy() {
        base.OnDestroy();

        for(int i = 0; i < _instancedSystemPrefabs.Count; ++i) {
            Destroy(_instancedSystemPrefabs[i]);
        }

        _instancedSystemPrefabs.Clear();
    }

    public void StartGame() {
        // Load the main scene
        LoadLevel("SampleScene");
    }

    public void TogglePause() {
        UpdateState(_currentGameState == GameState.RUNNING ? GameState.PAUSED : GameState.RUNNING);
    }

    public void QuitToMainMenu() {
        // Implement features for quitting the game e.g autosave
        Debug.Log("[GameManager] Exiting to Main Menu.");
        UpdateState(GameState.PREGAME);
    }

    public void ExitToSystem() {
        // Implement features for quitting the game e.g autosave
        Debug.Log("[GameManager] Exiting to System.");
        Application.Quit();
    }
}
