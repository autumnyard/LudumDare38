﻿using UnityEngine;
using UnityEngine.SceneManagement;


public class Director : MonoBehaviour
{

    #region Variables
    public GameManager gameManager;
    //public CameraManager cameraManager;
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

        //Debug.Log("Change scene to: " + currentScene);

        switch (currentScene)
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
                managerMap.SummonMap();
                managerEntity.SummonPlayer();

                /*
                if (managerEntity.playerScript1 != null)
                {
                    managerEntity.playerScript1.OnDie += GameEnd;
                }
                */

                managerInput.SetEvents();
                managerUI.SetPanels();
                break;

            case Structs.GameScene.GameEnd:
                //managerEntity.playerScript1.OnDie -= GameEnd;

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

    public void GameEnd()
    {
        ChangeScene(Structs.GameScene.GameEnd);
    }

    public void Exit()
    {
        Debug.Log("Exit game!");
        ChangeScene(Structs.GameScene.Exit);
    }
    #endregion


    #region DEBUG
    /*
    public void DebugHurtPlayer1()
    {
        managerEntity.playerScript1.Hurt();
    }

    public void DebugHurtPlayer2()
    {
        managerEntity.playerScript2.Hurt();
    }
    */
    #endregion
}
