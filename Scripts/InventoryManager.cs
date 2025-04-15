using System;
using UnityEngine;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    public event Action ItemChangedEvent;

    private int currentCars;
    private int currentCoin;

    public int Cars
    {
        get => currentCars;
        set
        {
            if (currentCars != value)
            {
                currentCars = value;
                ItemChangedEvent?.Invoke();
            }
        }
    }

    public int Coin
    {
        get => currentCoin;
        set
        {
            if (currentCoin != value)
            {
                currentCoin = value;
                ItemChangedEvent?.Invoke();
            }
        }
    }

    [SerializeField] private ItemCollector _itemCollector;

    private void OnEnable()
    {
        ItemChangedEvent += HandheldeItemChangedEvent;
    }

    private void Load()
    {
        
        currentCoin = _itemCollector.CurrentCoinCount;
    }

    private void HandheldeItemChangedEvent()
    {
        Debug.Log($"Current Cars: {currentCars}, CurrnetCoin{currentCoin}");
    }
}
