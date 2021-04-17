using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameEvents
{
	[CreateAssetMenu]
	public class IntEvent : ScriptableObject
	{
		private readonly List<IntEventListener> _listeners = new List<IntEventListener>();

		public void Raise(int arg)
		{
			for (int i = _listeners.Count - 1; i >= 0; i--)
				_listeners[i].OnEventRaised(arg);
		}

		public void RegisterListener(IntEventListener listener)
		{ _listeners.Add(listener); }

		public void UnregisterListener(IntEventListener listener)
		{ _listeners.Remove(listener); }
	}
}
