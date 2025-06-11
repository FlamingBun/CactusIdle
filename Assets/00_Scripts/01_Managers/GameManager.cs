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
                instance.AddComponent<UIManager>();
                instance.AddComponent<ItemManager>();
                
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
    public UIManager UIManager { get; private set; }
    public ItemManager ItemManager { get; private set; }
    

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
        StageManager = GetComponent<StageManager>();
        EnemyManager = GetComponent<EnemyManager>();
        UIManager = GetComponent<UIManager>();
        ItemManager = GetComponent<ItemManager>();
        
        DataManager.Init();
        
        ItemManager.Init();
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