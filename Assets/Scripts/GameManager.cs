using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private PanelManager panelManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource bgmSource;

    private bool inventoryOpen;
    private int lives;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeAudio();
    }

    private void Start()
    {
        player.canMove = true;
        inventoryOpen = false;
        LoadPlayerData();
        UpdateLifeStatus();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
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
        else
        {
            Debug.LogWarning("AudioSource components are not assigned.");
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
            player.Die();
            FinishGame();
        }

        Debug.Log("Player life: " + lives);
    }

    public void FinishGame()
    {
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        player.canMove = false;
        InventoryManager.Instance.ClearInventory();
        bgmSource?.Stop();
        audioSource?.Stop();
        audioSource?.PlayOneShot(audioManager?.Win);
        yield return new WaitForSeconds(5);
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Game");
    }


    public void ToggleInventory(bool value)
    {
        inventoryOpen = value;
        player.canMove = !inventoryOpen;
        inventoryPanel?.SetActive(inventoryOpen);
    }

    public void UseItem(Item item)
    {
        if (item is UsableItem usableItem && CheckCapacitable(usableItem.effect)) {
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
        player.boost += 0.5f;
        yield return new WaitForSeconds(20);
        player.boost -= 0.5f;

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
