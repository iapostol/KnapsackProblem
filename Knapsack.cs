using System;
using System.Collections.Generic;
using System.Linq;

namespace KnapsackProblem
{
    public class Knapsack
    {
        private readonly int capacity;

        private int[,] solutions;

        private IEnumerable<Item> items;

        public Knapsack(int capacity)
        {
            if (capacity == 0)
                throw new ArgumentOutOfRangeException();

            this.capacity = capacity;
        }

        private void InitSolutions()
        {
            solutions = new int[items.Count() + 1, capacity + 1];

            for (int i = 0; i <= items.Count(); i++)
                for (int j = 0; j <= capacity; j++)
                    solutions[i, j] = 0;
        }

        private void FindSolutions()
        {
            for (int i = 1; i <= items.Count(); i++)
                for (int j = 0; j <= capacity; j++)
                    Solve(i, j);
        }

        private void Solve(int i, int j)
        {
            var previous = i - 1;

            var item = ItemAt(previous);

            if (j >= item.Weight)
            {
                int candidate = solutions[previous, j - item.Weight] + item.Value;

                solutions[i, j] = solutions[previous, j] < candidate
                                ? candidate
                                : solutions[previous, j];
            }
        }

        private Item ItemAt(int index)
        {
            return items.ElementAt(index);
        }

        private void MarkPacked()
        {
            var count = items.Count();

            var remainingCapacity = capacity;

            while (count > 0)
            {
                if (solutions[count, remainingCapacity] != solutions[count - 1, remainingCapacity])
                {
                    var item = ItemAt(count - 1);

                    item.IsPacked = true;

                    remainingCapacity -= item.Weight;
                }

                count--;
            }
        }

        public void TryPack(IEnumerable<Item> items)
        {
            this.items = items;

            InitSolutions();

            FindSolutions();

            MarkPacked();
        }
    }
}