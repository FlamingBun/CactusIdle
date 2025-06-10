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
                // instance.AddComponent<UIManager>();
                // instance.AddComponent<Inventory>();
                if (instance is GameManager gameManager)
                {
                    gameManager.Initialize();
                }
            }
            return instance;
        }
    }

    public Player Player { get; private set; }
    
    public DataManager DataManager { get; private set; }
    public EnemyManager EnemyManager { get; private set; }

    public CinemachineVirtualCamera virtualCamera;
    // public UIManager UIManager { get { return uiManager; } }
    // public Inventory Inventory { get { return inventory; } }
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        DontDestroyOnLoad(gameObject);
        DataManager = GetComponent<DataManager>();
        EnemyManager = GetComponent<EnemyManager>();
        
        DataManager.Init();
        
        // uiManager = GetComponent<UIManager>();
        // characterManager = GetComponent<CharacterManager>();
        // inventory = GetComponent<Inventory>();
        // characterManager.Player = Instantiate(player).GetComponent<Player>();
    }

    private void Start()
    {
        GameStart();
    }

    private void GameStart()
    {
        Player= Instantiate(DataManager.PlayerData.player).GetComponent<Player>();
        Player.Initialize(DataManager.PlayerData);
        
        virtualCamera.Follow = Player.CameraPoint;
        virtualCamera.LookAt = Player.CameraPoint;
        
        EnemyManager.Init();
        //StartCoroutine(OpenConditionUI());
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