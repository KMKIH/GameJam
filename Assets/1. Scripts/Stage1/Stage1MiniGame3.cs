using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1MiniGame3 : MiniGameManager
{
    [SerializeField] private Stage1MiniGame3StateSO _miniGame3State;
    [SerializeField] private VoidEventChannelSO m_foodEat;
    private GameObject _miniGame3StateObj;

    void Start()
    {
        _miniGame3State.ResetState();
        _miniGame3StateObj = GameObject.FindGameObjectsWithTag("Mini Game")[0];
        Instantiate(_miniGame3State.foods[_miniGame3State.index], _miniGame3StateObj.transform);
    }
    void Update()
    {
        if (_miniGame3State.life >= 3)
        {
            OnSuccessMiniGame();
        }
    }

    void OnEnable()
    {
        m_foodEat.OnEventRaised += OnFoodEatEvent;
    }
    void OnDisable()
    {
        m_foodEat.OnEventRaised -= OnFoodEatEvent;
    }

    public void OnFoodEatEvent()
    {
        _miniGame3State.isBabyActive = false;
        _miniGame3State.isReverse = false;
        StartCoroutine(InstantiateNewFood());
    }
    
    IEnumerator InstantiateNewFood()
    {
        yield return new WaitForSeconds(1f);
        _miniGame3State.isBabyActive = true;
        Instantiate(_miniGame3State.foods[_miniGame3State.index], _miniGame3StateObj.transform);

    }
}
