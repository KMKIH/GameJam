using UnityEngine;

public class Stage2MiniGame3 : MiniGameManager
{
    [SerializeField] private VoidEventChannelSO m_gameClear;

    private void OnEnable()
    {
        m_gameClear.OnEventRaised -= OnGameClear;
        m_gameClear.OnEventRaised += OnGameClear;
    }
    private void OnDisable()
    {
        m_gameClear.OnEventRaised -= OnGameClear;
    }

    public void OnGameClear()
    {
        OnSuccessMiniGame();
    }
}
