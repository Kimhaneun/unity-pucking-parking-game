using System;
using Unity.Netcode;
using UnityEngine;

public class LevelManager : NetworkBehaviour
{
    public static LevelManager Instance;
    [field: SerializeField] public LevelListData LevelListData { get; private set; }
    [SerializeField] private GameObject _levelSelect;
    public CarSpawner _currentLevel;

    [SerializeField] private Timer _timer;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetTimer()
    {
        _timer.SetTimer();
        _timer.StartTimer(_currentLevel.LevelData.timer);
    }

    [ClientRpc]
    public void SetTimerClientRpc()
    {
        SetTimer();
    }

    public void LoadLevel(int index)
    {
        if (!NetworkManager.Singleton.IsServer)
        {
            return;
        }

        if (_currentLevel != null)
        {
            
            Destroy(_currentLevel.gameObject);
        }

        if (IsServer) {
            NetworkGameManager.Instance.currentMap = Instantiate(LevelListData.levelList[index - 1].levelPrefab, Vector3.zero, Quaternion.identity);
            NetworkGameManager.Instance.currentMap.NetworkObject.Spawn();
        }
        SpawnMapClientRpc();
        SetTimerClientRpc();

        StarManager.Instance.SetStarData();

        OffPanelClientRpc();

        NetworkGameManager.Instance.isRunning = true;
    }
    [ClientRpc]
    private void SpawnMapClientRpc() {
        _currentLevel = NetworkGameManager.Instance.currentMap;
    }
    [ClientRpc]
    private void OffPanelClientRpc()
    {
        _levelSelect.SetActive(false);
    }

    public void ClearGame()
    {
        if (IsServer)
        {
            NetworkGameManager.Instance.isRunning = false;
            StarManager.Instance.IsClear = true;
            ClearGameClientRpc(NetworkGameManager.Instance.currentTime.Value);
        }
    }

    [ClientRpc]
    public void ClearGameClientRpc(float timer)
    {
        Debug.Log("clear stage");
        NetworkGameManager.Instance.isRunning = false;
        StarManager.Instance.IsClear = true;
        CarSpawner map = NetworkGameManager.Instance.currentMap;
        if (map != null) {
            map.DeleteCar();
            map.resultPanel.ShowResultPanel(timer, StarManager.Instance.IsClear, StarManager.Instance.IsTimeOut, StarManager.Instance.IsGetRareItem);
        }
        _timer.EndTimer();
    }

    public void LoadNextLevel()
    {
        if (!IsServer)
            return;

        int nextLevel = _currentLevel.LevelData.level + 1;

        if (nextLevel <= LevelListData.levelList.Count)
            LoadLevel(nextLevel);
    }

    public void ReloadLevel()
    {
        if (!IsServer)
            return;

        if (_currentLevel != null)
        {
            int currentLevelIndex = _currentLevel.GetComponent<CarSpawner>().LevelData.level;
            LoadLevel(currentLevelIndex);
        }
    }

    public void SelectLevel()
    {
        if (!IsServer)
            return;

        _levelSelect.SetActive(true);

        if (_currentLevel != null)
            Destroy(_currentLevel.gameObject);
    }
}
