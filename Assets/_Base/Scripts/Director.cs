using UnityEngine;
using UnityEngine.SceneManagement;


public class Director : MonoBehaviour
{

    #region Variables
    public GameManager gameManager;
    //public CameraManager cameraManager;
    //public Player player;
    //public MapManager mapManager;
    //public EntityManager entityManager;
    //public WavesManager waveManager;
    //public InputManager inputManager;
    public ManagerUI managerUI;
    //public ScoreManager scoreManager;

    //public Transform playerTransform;

    public Structs.GameMode currentGameMode { private set; get; }
    public Structs.GameDifficulty currentGameDifficulty { private set; get; }
    public Structs.GameView currentGameView { private set; get; }
    public Structs.GameScene currentScene;
    #endregion


    #region Singleton
    private static Director instance;

    public static Director Instance
    {
        get { return instance; }
    }

    static Director()
    {
        GameObject obj = GameObject.Find("Director");

        if (obj == null)
        {
            obj = new GameObject("Director", typeof(Director));
        }

        instance = obj.GetComponent<Director>();
    }
    #endregion


    #region Monobehaviour
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion


    #region Scene management
    private void ChangeScene(Structs.GameScene to)
    {
        currentScene = to;
        Debug.Log("Change scene to: " + currentScene);

        switch (currentScene)
        {
            case Structs.GameScene.Initialization:
                SwitchToMenu();
                break;

            case Structs.GameScene.Menu:
                managerUI.UpdateUI();
                break;

            case Structs.GameScene.LoadingGame:
                //mapManager.Init();
                //InitMap();
                //entityManager.Init();
                //GameInitialize();
                //InitPlayer();
                //InitCamera();
                //SetCameraOnPlayer();
                //GameStart();
                SwitchToIngame();
                break;


            case Structs.GameScene.Ingame:
                //inputManager.SetEvents();
                //uiManager.UpdateUI();
                managerUI.UpdateUI();
                break;

            case Structs.GameScene.GameEnd:
                //entityManager.Reset();
                managerUI.UpdateUI();
                SwitchToMenu();
                break;

            case Structs.GameScene.Exit:
                Application.Quit();
                break;
        }

    }
    #endregion


    #region Game settings
    public void SetGameSettings(Structs.GameMode gameMode, Structs.GameDifficulty gameDifficulty, Structs.GameView viewMode)
    {
        currentGameMode = gameMode;
        currentGameDifficulty = gameDifficulty;
        currentGameView = viewMode;
    }
    #endregion


    #region Game cycle
    // This is the first thing that begins the whole game
    public void EverythingBeginsHere()
    {
        ChangeScene(Structs.GameScene.Initialization);
    }

    // This is automatic
    private void SwitchToMenu()
    {
        ChangeScene(Structs.GameScene.Menu);
    }

    public void GameBegin()
    {
        ChangeScene(Structs.GameScene.LoadingGame);
    }

    // This is automatic
    private void SwitchToIngame()
    {
        ChangeScene(Structs.GameScene.Ingame);
    }

    private void GameEnd()
    {
        ChangeScene(Structs.GameScene.GameEnd);
    }

    public void Exit()
    {
        ChangeScene(Structs.GameScene.Exit);
    }
    #endregion
}
