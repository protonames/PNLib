using System;
using UnityEngine;

namespace PNLib.Attributes
{
	[Serializable]
	public class StatsModifier : MonoBehaviour
	{
		public object Source { get; }
		public StatsModifierType Type { get; }
		public float Value { get; }

		public StatsModifier(float value, StatsModifierType type)
			: this(value, type, null) { }

		public StatsModifier(float value, StatsModifierType type, object source)
		{
			Value = value;
			Type = type;
			Source = source;
		}
	}
}