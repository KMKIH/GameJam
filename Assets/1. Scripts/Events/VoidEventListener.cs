using UnityEngine;
using UnityEngine.Events;

public class VoidEventListener : MonoBehaviour
{
	[SerializeField] private VoidEventChannelSO m_Channel = default;

	public UnityEvent m_Response;

	private void OnEnable()
	{
		if (m_Channel != null)
			m_Channel.OnEventRaised += OnEventRaised;
	}

	private void OnDisable()
	{
		if (m_Channel != null)
			m_Channel.OnEventRaised -= OnEventRaised;
	}

	private void OnEventRaised()
	{
		if (m_Response != null)
			m_Response.Invoke();
	}
}