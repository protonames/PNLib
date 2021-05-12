using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace PNLib.Attributes
{
	[Serializable]
	public class Stats : MonoBehaviour
	{
		public float Maximum
		{
			get => maximum;
			private set
			{
				if (Mathf.Abs(value - float.Epsilon) < 0f)
					throw new Exception("Stats Maximum can not be zero.");

				hasChanged = true;
				maximum = value;
			}
		}

		public float NormalizedValue => Value / Maximum;

		public float RawValue { get; private set; }

		public float Value
		{
			get
			{
				if (hasChanged)
				{
					value = CalculateModifiedValue();
					hasChanged = false;
				}

				return value;
			}
			set
			{
				float lastValue = RawValue;
				RawValue = Mathf.Clamp(value, Minimum, Maximum);

				if (Mathf.Abs(lastValue - RawValue) > 0f)
				{
					hasChanged = true;
					OnChangedEvent?.Invoke();
				}
			}
		}

		public event Action OnChangedEvent;

		private float Minimum
		{
			get => minimum;
			set
			{
				hasChanged = true;
				minimum = value;
			}
		}

		private bool hasChanged;
		private float maximum;
		private float minimum;
		private List<StatsModifier> statsModifiers;
		private float value;

		public Stats(float value, float maximum, float minimum)
			: this(value)
		{
			Maximum = maximum;
			Minimum = minimum;
			Value = value;
		}

		public Stats(float value)
		{
			statsModifiers = new List<StatsModifier>();
			Maximum = float.MaxValue;
			Minimum = 0f;
			Value = value;
		}

		public void AddModifier(StatsModifier modifier, bool hasLowPriority = false)
		{
			hasChanged = true;

			if (hasLowPriority)
				statsModifiers.Insert(0, modifier);
			else
				statsModifiers.Add(modifier);
		}

		public void RemoveModifier(StatsModifier modifier)
		{
			if (statsModifiers.Remove(modifier))
				hasChanged = true;
		}

		public void RemoveAllModifiersFromSource([NotNull] object source)
		{
			if (statsModifiers.RemoveAll(x => x.Source.Equals(source)) > 0)
				hasChanged = true;
		}

		public void RemoveAllModifiers()
		{
			if (statsModifiers.Count <= 0)
				return;

			hasChanged = true;
			statsModifiers.Clear();
		}

		public bool HasModifierFromSource([NotNull] object source)
		{
			return statsModifiers.Any(x => x.Source == source);
		}

		public Stats DeepCopy()
		{
			Stats stats = this;
			StatsModifier[] array = new StatsModifier[statsModifiers.Count];
			statsModifiers.CopyTo(array, 0);
			stats.statsModifiers = new List<StatsModifier>(array);
			return stats;
		}

		private float CalculateModifiedValue()
		{
			float modifiedValue = RawValue;

			foreach (StatsModifier item in statsModifiers.Where(x => x.Type == StatsModifierType.Addition))
			{
				modifiedValue += item.Value;
			}

			float totalPercentage = 0f;

			foreach (StatsModifier item in statsModifiers.Where(x => x.Type == StatsModifierType.Multiplier))
			{
				totalPercentage += item.Value;
			}

			modifiedValue *= 1f + totalPercentage;

			if (statsModifiers.Any(x => x.Type == StatsModifierType.Override))
			{
				float? overrideValue = statsModifiers.LastOrDefault(x => x.Type == StatsModifierType.Override)?.Value;

				if (overrideValue != null)
					modifiedValue = (float) overrideValue;
			}

			modifiedValue = Mathf.Clamp(modifiedValue, Minimum, Maximum);
			return (float) System.Math.Round(modifiedValue, 3);
		}

#region Operators
		public static float operator +(Stats stats, float value)
		{
			return stats.Value + value;
		}

		public static float operator +(float value, Stats stats)
		{
			return value + stats.Value;
		}

		public static float operator +(Stats stats, int value)
		{
			return stats.Value + value;
		}

		public static float operator +(int value, Stats stats)
		{
			return value + stats.Value;
		}

		public static float operator /(Stats stats, float value)
		{
			return stats.Value / value;
		}

		public static float operator /(float value, Stats stats)
		{
			return value / stats.Value;
		}

		public static float operator /(Stats stats, int value)
		{
			return stats.Value / value;
		}

		public static float operator /(int value, Stats stats)
		{
			return value / stats.Value;
		}

		public static implicit operator float(Stats stats)
		{
			return stats.Value;
		}

		public static float operator *(Stats stats, float value)
		{
			return stats.Value * value;
		}

		public static float operator *(float value, Stats stats)
		{
			return value * stats.Value;
		}

		public static float operator *(Stats stats, int value)
		{
			return stats.Value * value;
		}

		public static float operator *(int value, Stats stats)
		{
			return value * stats.Value;
		}

		public static float operator -(Stats stats, float value)
		{
			return stats.Value - value;
		}

		public static float operator -(float value, Stats stats)
		{
			return value - stats.Value;
		}

		public static float operator -(Stats stats, int value)
		{
			return stats.Value - value;
		}

		public static float operator -(int value, Stats stats)
		{
			return value - stats.Value;
		}
#endregion
	}
}