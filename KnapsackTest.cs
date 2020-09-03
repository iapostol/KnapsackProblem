using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace KnapsackProblem
{
    public class KnapsackTest
    {
        private const int SIZE = 15;
        private const string ITEM_1 = "Item1";
        private const string ITEM_2 = "Item2";
        private const string ITEM_3 = "Item3";
        private readonly Knapsack knapsack;

        public KnapsackTest()
        {
            knapsack = new Knapsack(SIZE);
        }

        private IEnumerable<char> Packed(List<Item> items)
        {
            return string.Join(",", items.Where(i => i.IsPacked).Select(p => p.Name));
        }

        private int PackedValue(List<Item> items)
        {
            return items.Where(i => i.IsPacked).Sum(p => p.Value);
        }

        [Fact]
        public void ThrowArgumentOutOfRangeExceptionIfCapacityIsZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Knapsack(0));
        }

        [Fact]
        public void ThrowArgumentNullExceptionIfNoItemsToPacked()
        {
            Assert.Throws<ArgumentNullException>(() => knapsack.TryPack(null));
        }

        [Fact]
        public void TestNoItemsToPack()
        {
            var items = new List<Item>();

            knapsack.TryPack(items);

            Assert.Equal(0, PackedValue(items));
        }

        [Fact]
        public void TestItemWeightExceedsCapacity()
        {
            var items = new List<Item>
            {
                new Item { Weight = SIZE + 1, Value = 1 }
            };

            knapsack.TryPack(items);

            Assert.Equal(0, PackedValue(items));
        }

        [Fact]
        public void TestPackTwoItemsWhereOnlyOneCanFit()
        {
            var items = new List<Item>
            {
                new Item { Name = ITEM_1, Weight = 10, Value = 1 },
                new Item { Name = ITEM_2, Weight = 6, Value = 1 }
            };

            knapsack.TryPack(items);

            Assert.Equal(ITEM_1, Packed(items));
            Assert.Equal(1, PackedValue(items));
        }

        [Fact]
        public void TestPackThreeItemsWhereOnlyOnCanFit()
        {
            var items = new List<Item>
            {
                new Item { Name = ITEM_1, Weight = 2, Value = 10 },
                new Item { Name = ITEM_2, Weight = 14, Value = 16 },
                new Item { Name = ITEM_3, Weight = 13, Value =  5}
            };

            knapsack.TryPack(items);

            Assert.Equal(ITEM_2, Packed(items));
            Assert.Equal(16, PackedValue(items));
        }

        [Fact]
        public void TestPackThreeItemsWhereOnlyTwoCanFit()
        {
            var items = new List<Item>
            {
                new Item { Name = ITEM_1, Weight = 8, Value = 10 },
                new Item { Name = ITEM_2, Weight = 12, Value = 3 },
                new Item { Name = ITEM_3, Weight = 3, Value =  5}
            };

            knapsack.TryPack(items);

            Assert.Equal(string.Format("{0},{1}", ITEM_1, ITEM_3), Packed(items));
            Assert.Equal(15, PackedValue(items));
        }
    }
}
