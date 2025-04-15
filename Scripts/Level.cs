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
        // SetUpItem();

        LevelStartEvent += HandheldeLevelStart;
        // LevelClearedEvent += HandheldeLevelCleared;
        LevelOverEvent += HandheldeGameOver;
    }

    private void OnDisable()
    {
        // SetOutItem();

        LevelStartEvent -= HandheldeLevelStart;
        // LevelClearedEvent -= HandheldeLevelCleared;
        LevelOverEvent -= HandheldeGameOver;
    }

    private void SetUpItem()
    {
        Transform itemParentTransform = transform.Find("Items");
        _items = itemParentTransform.GetComponentsInChildren<Item>().ToList();

        _itemCount = _items.Count;
        // _items.ForEach(item => item.OnItemCollected += HandheldeItemCollectd);
    }

    private void SetOutItem()
    {
        Transform itemParentTransform = transform.Find("Items");
        _items = itemParentTransform.GetComponentsInChildren<Item>().ToList();

        _itemCount = _items.Count;
        // _items.ForEach(item => item.OnItemCollected -= HandheldeItemCollectd);
    }


    private void HandheldeLevelStart()
    {
        // DataManager.Instance.SetStarData(); // 별 데이터 초기화
    }

    private void HandheldeLevelCleared()
    {
        ShowLevelClearPanel();
    }

    private void HandheldeGameOver()
    {
    }

    private void HandheldeItemCollectd(Item item)
    {
        _itemCount--;
    }

    // 임시로 제작한 메서드 
    // 매개변수로 UI... 등을 넣을 수 있겠지
    private void ShowLevelClearPanel()
    {
        // 여기서 각각 가져오고 true라면 별을 얻은 것
        //StarManager.Instance.IsClear();
        //StarManager.Instance.IsGetRareItem();
        //StarManager.Instance.IsTimeOut();
    }
}
