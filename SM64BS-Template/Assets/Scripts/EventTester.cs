using SM64BS.Behaviours;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class EventTester : MonoBehaviour {

	private EventManager[] _eventManagers;

	private void Awake()
	{
		_eventManagers = GameObject.FindObjectsOfType<EventManager>();
	}

	public void InvokeUnityEvent(string eventName)
	{
		foreach (EventManager em in _eventManagers)
		{
			FieldInfo fields = em.GetType().GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			UnityEvent ue = fields.GetValue(em) as UnityEvent;
			ue.Invoke();
		}
	}
}
