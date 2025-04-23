using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoSingleton<Level>
{
    [field: SerializeField] public LevelData LevelData { get; private set; }

    private List<Item> _items;
    private int _itemCount;

    public event Action LevelStartEvent;
    public event Action LevelOverEvent;

    private void OnEnable()
    {
        LevelStartEvent += HandheldeLevelStart;
        LevelOverEvent += HandheldeGameOver;
    }

    private void OnDisable()
    {
        LevelStartEvent -= HandheldeLevelStart;
        LevelOverEvent -= HandheldeGameOver;
    }

    private void SetUpItem()
    {
        Transform itemParentTransform = transform.Find("Items");
        _items = itemParentTransform.GetComponentsInChildren<Item>().ToList();

        _itemCount = _items.Count;
    }

    private void SetOutItem()
    {
        Transform itemParentTransform = transform.Find("Items");
        _items = itemParentTransform.GetComponentsInChildren<Item>().ToList();

        _itemCount = _items.Count;
    }

    private void HandheldeLevelCleared()
    {
        ShowLevelClearPanel();
    }

    private void HandheldeItemCollectd(Item item)
    {
        _itemCount--;
    }
}
