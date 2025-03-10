using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string GetInteractPromp();
    public void OnInteract();
}
public class ItemObject : MonoBehaviour , IInteractable
{
    public ItemData  data;

    public string GetInteractPromp()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }
    public void OnInteract()
    {
        CharacterManager.instance.player.itemData = data;
        CharacterManager.instance.player.addItem?.Invoke();
        Destroy(gameObject);
    }
}
