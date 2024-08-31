using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private UIInventory inventoryPanel;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private PanelManager panelManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource bgmSource;


    [SerializeField] private float impulseValue;

    private bool inventoryOpen;
    private int lives;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        InitGame();
    }

    private void InitGame()
    {
        player.InitGame();
        ToggleInventory(false);
        LoadPlayerData();
        UpdateLifeStatus();
        InitializeAudio();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void SetPlayer(PlayerMovement player)
    {
        this.player = player;
    }

    public void SetPanelManager(PanelManager panelManager)
    {
        this.panelManager = panelManager;
    }

    public void SetUIInventory(UIInventory inventoryPanel)
    {
        this.inventoryPanel = inventoryPanel;
    }

    private void InitializeAudio()
    {
        if (audioSource != null && bgmSource != null)
        {
            audioSource.playOnAwake = true;
            bgmSource.playOnAwake = true;
            audioSource.Play();
            bgmSource.Play();
        }
    }

    public void GainLife()
    {
        lives++;
        UpdateLifeStatus();
    }

    public void Hurt()
    {
        lives--;
        UpdateLifeStatus();
    }

    private void UpdateLifeStatus()
    {
        panelManager?.UpdateLifeStatus(lives);
        PlayerPrefs.SetInt("PlayerLives", lives);

        if (lives <= 0)
        {
            LoseGame();
        }
    }

    public void LoseGame()
    {
        player.Die();
        StartCoroutine(EndGame());
    }

    public void WinGame()
    {
        player.Win();
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        InventoryManager.Instance.ClearInventory();
        bgmSource?.Stop();
        audioSource?.Stop();
        lives = 3;
        yield return new WaitForSeconds(5);
        UpdateLifeStatus();
        SceneManager.LoadScene("Game");
        InitGame();
    }

    public void ToggleInventory(bool value)
    {
        inventoryOpen = value;
        player.canMove = !inventoryOpen;
        inventoryPanel?.gameObject.SetActive(inventoryOpen);
    }

    public void UseItem(Item item)
    {
        if (item is UsableItem usableItem && CheckCapacitable(usableItem.effect))
        {
            {
                InventoryManager.Instance.UseItem(usableItem);
                ApplyItemEffect(usableItem.effect);
            }
        }
    }

    private bool CheckCapacitable(Effect effect)
    {
        return effect switch
        {
            Effect.Life => lives < 3,
            Effect.Jump => player.boost <= 1,
            _ => false,
        };
    }

    private void ApplyItemEffect(Effect effect)
    {
        switch (effect)
        {
            case Effect.Life:
                GainLife();
                break;
            case Effect.Jump:
                StartCoroutine(BoostJump());
                break;
            default:
                break;
        }
    }

    private IEnumerator BoostJump()
    {
        player.boost += impulseValue;
        yield return new WaitForSeconds(20);
        player.boost -= impulseValue;

        yield break;
    }

    private void LoadPlayerData()
    {
        lives = PlayerPrefs.GetInt("PlayerLives", 3);
    }

    private void OnApplicationQuit()
    {
        SavePlayerData();
    }

    private void SavePlayerData()
    {
        PlayerPrefs.SetInt("PlayerLives", lives);
        PlayerPrefs.Save();
    }
}
