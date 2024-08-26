using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetails : MonoBehaviour
{
    public Image image;

    public TextMeshProUGUI itemName;

    public TextMeshProUGUI description;

    public Button useButton;

    protected Item _item;
    public Item Item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                itemName.text = _item.itemName;
                description.text = _item.description;
                image.sprite = _item.icon;
                image.color = new Color(1, 1, 1, 1);
                useButton.gameObject.SetActive(_item.itemType == ItemType.Usable); ;

            } else
            {
                itemName.text = null;
                description.text = null;
                image.sprite = null;
                image.color = new Color(1, 1, 1, 0);
                useButton.gameObject.SetActive(false);
            }

        }
    }

    private void Start()
    {
        useButton.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        GameManager.Instance.UseItem(Item);
    }

}
