using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Void Event Channel", fileName = "Void Event Channel")]
public class VoidEventChannelSO : ScriptableObject
{
	public UnityAction OnEventRaised;

	public void RaiseEvent()
	{
		Debug.Log("RaiseEvent called");
		if (OnEventRaised != null)
			OnEventRaised.Invoke();
	}
}

