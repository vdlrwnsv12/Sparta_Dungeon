using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiInven : MonoBehaviour
{
    public ItemSlot[] slots;
    public Transform slotPanel;
    public GameObject inventoryWindow;

    [Header("Select Name")]
    public TextMeshProUGUI SelectedItemName;
    public TextMeshProUGUI SelectedItemDescription;
    public TextMeshProUGUI SelectedStatName;
    public TextMeshProUGUI SelectedStatValue;
    public GameObject useBtn;
    public GameObject equipBtn;
    public GameObject unEquipBtn;
    public GameObject dropBtn;

    private PlayerController controller;
    private PlayerCondition condition;

    // Start is called before the first frame update
    void Start()
    {
        controller = CharacterManager.instance.player.controller;
        condition = CharacterManager.instance.player.condition;

        inventoryWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }
        ClearSelectedItemWindow();
    }

    // Update is called once per frame
    void ClearSelectedItemWindow()
    {
        SelectedItemName.text = string.Empty;
        SelectedItemDescription.text = string.Empty;
        SelectedStatName.text = string.Empty;
        SelectedStatValue.text = string.Empty;

        
        useBtn.SetActive(false);
        equipBtn.SetActive(false);
        unEquipBtn.SetActive(false);
        dropBtn.SetActive(false);
    }
}
