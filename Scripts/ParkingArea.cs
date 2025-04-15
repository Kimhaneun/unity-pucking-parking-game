using DG.Tweening;
using System;
using Unity.Netcode;
using UnityEngine;

public class ParkingArea : NetworkBehaviour
{
    public event Action LevelClearedEvent;

    private Collider _collider;
    private const string PLAYER_TAG = "Player";

    [SerializeField] private MeshRenderer _parkingArea;
    private Material _outlineMaterial;
    private readonly int _colorChangeAmountHashValue = Shader.PropertyToID("_ColorChangeAmount");
    private readonly int _BlinkHashValue = Shader.PropertyToID("_Blink");
    [SerializeField] private float _parkingCoolTime = 3;

    private float _startTime = 0;
    [SerializeField] private float _coolTime = 3;
    private bool _isInsideBounds = false;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        _outlineMaterial = _parkingArea.material;
        _outlineMaterial.SetFloat(_colorChangeAmountHashValue, 1);
        _outlineMaterial.SetFloat(_BlinkHashValue, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PLAYER_TAG) || !IsServer) return;
        _startTime = Time.time;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(PLAYER_TAG) || !IsServer) return;

        // 플레이어가 경계 내에 있는지 확인
        _isInsideBounds = _collider.bounds.Contains(other.bounds.min) && _collider.bounds.Contains(other.bounds.max);

        // _outlineMaterial의 현재 _colorChangeAmountHashValue 값을 가져오기
        float currentValue = _outlineMaterial.GetFloat(_colorChangeAmountHashValue);

        if (_isInsideBounds)
        {
            // 경계 안에 있으면 1에서 0으로 점진적으로 감소
            float elapsedTime = (Time.time - _startTime) / _coolTime;
            float t = Mathf.Clamp01(elapsedTime);

            float targetValue = Mathf.Lerp(1f, 0f, t);
            _outlineMaterial.SetFloat(_colorChangeAmountHashValue, targetValue);
            SetFadeClientRpc(targetValue);

            // Clear 조건을 만족했을 경우
            if (!StarManager.Instance.IsClear && t >= 1f)
            {
                _outlineMaterial.DOFloat(1, _BlinkHashValue, 0)
                    .OnComplete(() => _outlineMaterial.DOFloat(0, _BlinkHashValue, 0.3f))
                    .OnComplete(() =>
                    {
                        LevelManager.Instance.ClearGame();
                    });
            }
        }
        else
        {
            // 경계 밖에 있을 때 현재 값에서 1로 증가
            _startTime = Time.time;  // 타이머 초기화
            float elapsedTime = (Time.time - _startTime) / _coolTime;
            float t = Mathf.Clamp01(elapsedTime);

            // 현재 값에서 1로 서서히 증가
            float targetValue = Mathf.Lerp(currentValue, 1f, t);
            _outlineMaterial.SetFloat(_colorChangeAmountHashValue, targetValue);
        }
    }

    [ClientRpc]
    private void SetFadeClientRpc(float fadeValue)
    {
        if (IsServer)
            return;
        _outlineMaterial.SetFloat(_colorChangeAmountHashValue, fadeValue);
    }
}
