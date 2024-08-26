using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject[] lifes;

    [SerializeField]
    Button openInventoryButton;

    private void Start()
    {
        openInventoryButton.onClick.AddListener(OpenInventory);
    }

    private void OpenInventory()
    {
        GameManager.Instance.ToggleInventory(true);
    }

    public void UpdateLifeStatus(int activeLifes)
    {
        for (int i = 0; i < lifes.Length; i++)
        {
            float alpha = i < activeLifes ? 1f : 0.5f;
            lifes[i].GetComponent<Image>().color = new Color(1, 1, 1, alpha);
        }
    }
}
