using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PNLib.Random
{
	public abstract class PseudoRandomDistribution<T> : MonoBehaviour
	{
		[SerializeField]
		protected ChanceTable[] ItemChances;

		private List<T> history = new List<T>();

		protected void Reset()
		{
			history = new List<T>();

			foreach (ChanceTable item in ItemChances)
			{
				item.WeightedChance = item.BaseChance;
			}
		}

		public T GetRandomItem()
		{
			UpdateWeights();
			float totalWeight = ItemChances.Select(x => x.WeightedChance).Where(x => x > 0).Sum(x => x);
			float roll = UnityEngine.Random.Range(0, totalWeight);
			List<ChanceTable> orderedItems = ItemChances.OrderBy(x => x.WeightedChance).ToList();

			foreach (ChanceTable itemWeight in orderedItems.Where(x => x.WeightedChance >= 0))
			{
				if (roll < itemWeight.WeightedChance)
				{
					return Save(itemWeight.Item);
				}

				roll -= itemWeight.WeightedChance;
			}

			return ResetGetRandomItem();
		}

		private T ResetGetRandomItem()
		{
			Reset();
			return GetRandomItem();
		}

		private int CalculateHistorySize()
		{
			IEnumerable<int> baseChances = ItemChances.Select(x => x.BaseChance).ToArray();
			int size = baseChances.Sum(x => x) / GreatestCommonDivisor(baseChances);
			return size;
		}

		private void UpdateWeights()
		{
			if (history.Count <= 0)
			{
				Reset();
				return;
			}

			float totalBaseWeight = ItemChances.Select(x => x.BaseChance).Sum(x => x);

			for (int i = 0; i < ItemChances.Length; i++)
			{
				float baseDropPercentage = ItemChances[i].BaseChance / totalBaseWeight;

				float currentDropPercentage =
					history.Count(x => Equals(x, ItemChances[i].Item)) / (float) history.Count;

				float deltaPercentage = baseDropPercentage - currentDropPercentage;
				ItemChances[i].WeightedChance = deltaPercentage * totalBaseWeight;
			}
		}

		private T Save(T item)
		{
			int historySize = CalculateHistorySize();

			if (history.Count >= historySize)
			{
				history.RemoveRange(0, (history.Count - historySize) + 1);
			}

			history.Add(item);
			return item;
		}

		private static int GreatestCommonDivisor(IEnumerable<int> numbers)
		{
			return numbers.Aggregate(GreatestCommonDivisor);
		}

		private static int GreatestCommonDivisor(int a, int b)
		{
			while (true)
			{
				if (b == 0)
				{
					return a;
				}

				int aux = a;
				a = b;
				b = aux % b;
			}
		}

		[Serializable]
		public class ChanceTable
		{
			[SerializeField]
			private int baseChance;

			public int BaseChance => baseChance;

			[SerializeField]
			private T item;

			public T Item => item;

			[SerializeField]
			private float weightedChance;

			public float WeightedChance
			{
				get => weightedChance;
				set => weightedChance = value;
			}
		}
	}
}