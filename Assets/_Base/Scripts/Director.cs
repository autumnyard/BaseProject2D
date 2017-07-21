﻿using UnityEngine;
using UnityEngine.SceneManagement;


public class Director : MonoBehaviour
{

	#region Variables
	public GameManager gameManager;
	public ManagerCamera managerCamera;
	//public Player player;
	public ManagerMap managerMap;
	public ManagerEntity managerEntity;
	//public WavesManager waveManager;
	public ManagerInput managerInput;
	public ManagerUI managerUI;
	//public ScoreManager scoreManager;

	public Structs.GameMode currentGameMode { private set; get; }
	public Structs.GameDifficulty currentGameDifficulty { private set; get; }
	public Structs.GameView currentGameView { private set; get; }
	public Structs.GameScene currentScene;

	public bool isPaused;
	#endregion


	#region Singleton
	private static Director instance;

	public static Director Instance
	{
		get { return instance; }
	}

	static Director()
	{
		GameObject obj = GameObject.Find( "Director" );

		if( obj == null )
		{
			obj = new GameObject( "Director", typeof( Director ) );
		}

		instance = obj.GetComponent<Director>();
	}
	#endregion


	#region Monobehaviour
	private void Awake()
	{
		DontDestroyOnLoad( this.gameObject );
	}

	#endregion


	#region Scene management
	private void ChangeScene( Structs.GameScene to )
	{
		currentScene = to;

		//Debug.Log("Change scene to: " + currentScene);

		switch( currentScene )
		{
			case Structs.GameScene.Initialization:
				SwitchToMenu();
				break;

			case Structs.GameScene.Menu:
				managerInput.SetEvents();
				managerUI.SetPanels();
				break;

			case Structs.GameScene.LoadingGame:
				//InitMap();
				//entityManager.Init();
				//GameInitialize();
				//InitPlayer();
				//InitCamera();
				//SetCameraOnPlayer();
				//GameStart();
				managerUI.SetPanels();
				SwitchToIngame();
				break;

			case Structs.GameScene.Ingame:
				//inputManager.SetEvents();
				//uiManager.UpdateUI();
				// This loads the map, sets the player and the camera
				LoadLevel( 0 );

				if( managerEntity.playersScript[0] != null )
				{
					managerEntity.playersScript[0].OnDie += GameEnd;
				}

				managerInput.SetEvents();
				managerUI.SetPanels();
				break;

			case Structs.GameScene.GameEnd:
				managerEntity.playersScript[0].OnDie -= GameEnd;

				managerEntity.Reset();
				managerMap.Reset();
				managerInput.SetEvents();
				managerUI.SetPanels();
				SwitchToMenu();
				break;

			case Structs.GameScene.Exit:
				Application.Quit();
				break;
		}

	}
	#endregion


	#region Game settings
	public void SetGameSettings( Structs.GameMode gameMode, Structs.GameDifficulty gameDifficulty, Structs.GameView viewMode )
	{
		currentGameMode = gameMode;
		currentGameDifficulty = gameDifficulty;
		currentGameView = viewMode;
	}

	private void LoadLevel( int levelNumber )
	{

		managerMap.LoadMap( levelNumber );
		var newMap = managerMap.mapScript;

		// Load player init position from map
		// If there is a player init pos in the map data
		Vector2 playerInitPos;
		if( newMap.players.Count > 0 )
		{
			playerInitPos = managerMap.mapScript.players[0];
		}
		else
		{
			playerInitPos = Vector2.zero;
		}
		managerEntity.SummonPlayer( 0, playerInitPos );

		// Set camera
		Vector2 cameraInitPos = Vector2.zero;
		if( newMap.cameraGrabs.Count > 0 )
		{
			// If there is camera data, init camera to first cameragrab
			cameraInitPos = managerMap.mapScript.cameraGrabs[0];
			managerCamera.cameras[0].SetFixedPoint( cameraInitPos );
		}
		else
		{
			// If there is no camera data, set camera to follow the player
			cameraInitPos = Vector2.zero;
			managerCamera.cameras[0].SetFollow( managerEntity.playersScript[0].transform );
		}
	}

	#endregion


	#region Game cycle
	// This is the first thing that begins the whole game
	public void EverythingBeginsHere()
	{
		ChangeScene( Structs.GameScene.Initialization );
	}

	// This is automatic
	private void SwitchToMenu()
	{
		ChangeScene( Structs.GameScene.Menu );
	}

	public void GameBegin()
	{
		ChangeScene( Structs.GameScene.LoadingGame );
	}

	// This is automatic
	private void SwitchToIngame()
	{
		ChangeScene( Structs.GameScene.Ingame );
	}

	public void GameEnd()
	{
		ChangeScene( Structs.GameScene.GameEnd );
	}

	public void Exit()
	{
		Debug.Log( "Exit game!" );
		ChangeScene( Structs.GameScene.Exit );
	}
	#endregion


	#region DEBUG
	//public void DebugHurtPlayer()
	//{
	//    managerEntity.playersScript[0].Hurt();
	//}
	#endregion
}
