using UnityEngine;

public class Stage3MiniGame2 : MiniGameManager
{
    [SerializeField] private VoidEventChannelSO m_collided;

    private void OnEnable()
    {
        m_collided.OnEventRaised -= OnGameClear;
        m_collided.OnEventRaised += OnGameClear;
    }
    private void OnDisable()
    {
        m_collided.OnEventRaised -= OnGameClear;
    }

    public void OnGameClear()
    {
        OnSuccessMiniGame();
    }
}
