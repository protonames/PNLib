using System;

namespace PNLib.Attributes
{
	[Serializable]
	public class StatsModifier
	{
		public object Source;
		public StatsModifierType Type;
		public float Value;

		public StatsModifier(float value, StatsModifierType type, object source = null)
		{
			Value = value;
			Type = type;
			Source = source;
		}
	}
}