using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int _currentCoinCount = 0;
    public int CurrentCoinCount { get => _currentCoinCount; set => _currentCoinCount = value; }

    private void OnEnable()
    {
        InventoryManager.Instance.ItemChangedEvent += HandleItemChangedEvent;
    }

    private void HandleItemChangedEvent()
    {
        InventoryManager.Instance.Coin = _currentCoinCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            item.GetItem(this);
        }
    }
}