using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace PNLib.Attributes
{
	public class Stats
	{
		private float maximum = float.MaxValue;

		public float Maximum
		{
			get => maximum;
			private set
			{
				hasChanged = true;
				maximum = value;
			}
		}

		private float minimum;

		private float Minimum
		{
			get => minimum;
			set
			{
				hasChanged = true;
				minimum = value;
			}
		}

		public float NormalizedValue => Value / Maximum;

		private float RawValue { get; set; }

		private readonly List<StatsModifier> statsModifiers;

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

				if (System.Math.Abs(lastValue - RawValue) > float.Epsilon)
				{
					hasChanged = true;
					OnChangedEvent?.Invoke();
				}
			}
		}

		public event Action OnChangedEvent;

		private readonly ReadOnlyCollection<StatsModifier> statsModifiersReadOnly;
		private bool hasChanged = true;
		private float value;

		public Stats(float value, float maximum = float.MaxValue, float minimum = 0)
		{
			Maximum = maximum;
			Minimum = minimum;
			Value = value;
			statsModifiers = new List<StatsModifier>();
			statsModifiersReadOnly = statsModifiers.AsReadOnly();
		}

		public void AddModifier(StatsModifier modifier)
		{
			hasChanged = true;
			statsModifiers.Add(modifier);
		}

		public void RemoveModifier(StatsModifier modifier)
		{
			if (statsModifiers.Remove(modifier))
				hasChanged = true;
		}

		public void RemoveAllModifiersFromSource([NotNull] object source)
		{
			if (statsModifiers.RemoveAll(x => x.Source == source) > 0)
				hasChanged = true;
		}

		public void RemoveAllModifiers()
		{
			if (statsModifiers.Count <= 0)
				return;

			hasChanged = true;
			statsModifiers.Clear();
		}

		private float CalculateModifiedValue()
		{
			float modifiedValue = RawValue;
			modifiedValue += statsModifiers.Where(x => x.Type == StatsModifierType.Flat).Sum(x => x.Value);
			modifiedValue *= 1f + statsModifiers.Where(x => x.Type == StatsModifierType.Percentage).Sum(x => x.Value);
			modifiedValue = Mathf.Clamp(modifiedValue, minimum, Maximum);
			return (float) System.Math.Round(modifiedValue, 3);
		}
	}
}