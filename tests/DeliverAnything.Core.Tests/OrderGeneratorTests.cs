using System;
using DeliverAnything.Core;
using NUnit.Framework;

namespace DeliverAnything.Core.Tests
{
    public class OrderGeneratorTests
    {
        private const int SeedCount = 1000;

        [TestCase(1, 42)]
        [TestCase(2, 7)]
        [TestCase(3, -123)]
        [TestCase(5, int.MaxValue)]
        public void SameSeedAndTier_ReturnsSameOrder(int tier, int seed)
        {
            var a = OrderGenerator.Generate(tier, seed);
            var b = OrderGenerator.Generate(tier, seed);

            Assert.That(b, Is.EqualTo(a));
        }

        [Test]
        public void Tier1_NeverReturnsAliveGuardedOrAnchoredItems()
        {
            for (var seed = 0; seed < SeedCount; seed++)
            {
                var item = OrderGenerator.Generate(1, seed).Item;

                Assert.That(item.IsAlive, Is.False, $"seed {seed}");
                Assert.That(item.IsGuarded, Is.False, $"seed {seed}");
                Assert.That(item.IsAnchored, Is.False, $"seed {seed}");
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(10)]
        public void Fragility_AlwaysWithin0To3(int tier)
        {
            for (var seed = 0; seed < SeedCount; seed++)
            {
                var fragility = OrderGenerator.Generate(tier, seed).Item.Fragility;

                Assert.That(fragility, Is.InRange(0, 3), $"tier {tier}, seed {seed}");
            }
        }

        [Test]
        public void InvalidTier_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => OrderGenerator.Generate(0, 1));
        }
    }
}
