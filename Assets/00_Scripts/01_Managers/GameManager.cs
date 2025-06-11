using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManager").AddComponent<GameManager>();
                instance.AddComponent<DataManager>();
                instance.AddComponent<EnemyManager>();
                instance.AddComponent<StageManager>();
                // instance.AddComponent<UIManager>();
                // instance.AddComponent<Inventory>();
                if (instance is GameManager gameManager)
                {
                    gameManager.AwakeInitialize();
                }
            }
            return instance;
        }
    }

    public Player Player { get; private set; }
    
    public DataManager DataManager { get; private set; }
    public EnemyManager EnemyManager { get; private set; }
    public StageManager StageManager { get; private set; }

    public CinemachineVirtualCamera virtualCamera;
    // public UIManager UIManager { get { return uiManager; } }
    // public Inventory Inventory { get { return inventory; } }
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            AwakeInitialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void AwakeInitialize()
    {
        DontDestroyOnLoad(gameObject);
        DataManager = GetComponent<DataManager>();
        EnemyManager = GetComponent<EnemyManager>();
        StageManager = GetComponent<StageManager>();
        
        DataManager.Init();
        
        // uiManager = GetComponent<UIManager>();
        // characterManager = GetComponent<CharacterManager>();
        // inventory = GetComponent<Inventory>();
        // characterManager.Player = Instantiate(player).GetComponent<Player>();
    }

    private void Start()
    {
        StartInitialize();
        StartGame();
    }

    private void StartInitialize()
    {
        StageManager.Init();
        EnemyManager.Init();
        //StartCoroutine(OpenConditionUI());
    }

    private void StartGame()
    {
        StageManager.StartStage(0);
        
        Player= Instantiate(DataManager.PlayerData.player).GetComponent<Player>();
        Player.Initialize(DataManager.PlayerData);
        
        virtualCamera.Follow = Player.CameraPoint;
        virtualCamera.LookAt = Player.CameraPoint;
    }

    public void ClearStage()
    {
        Logger.Log("Clear Stage");
    }

    public void ClearAndNextStage()
    {
        Logger.Log("Clear And Next Stage");
    }



    // private IEnumerator OpenConditionUI()
    // {
    //     while(!UIManager.UIDictionary.ContainsKey(UIKey.ConditionUI))
    //     {
    //         yield return null;
    //     }
    //     
    //     UIManager.OpenUI(UIKey.ConditionUI);
    // }
    //
    // public void GameOver()
    // {
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    // }
}