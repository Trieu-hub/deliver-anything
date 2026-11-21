using System;

namespace DeliverAnything.Core
{
    /// <summary>
    /// Deterministic order generator: the same (tier, seed) always yields the same order.
    /// Tier 1: S/M items, no alive/guarded/anchored, no hazards.
    /// Tier 2: up to L, alive and hazards unlocked.
    /// Tier 3+: everything, including Container, guarded and anchored.
    /// </summary>
    public static class OrderGenerator
    {
        public const int MinTier = 1;

        public static OrderData Generate(int tier, int seed)
        {
            if (tier < MinTier)
                throw new ArgumentOutOfRangeException(nameof(tier), tier, "Tier must be >= 1.");

            var rng = new Random(seed);

            var maxSize = tier == 1 ? ItemSize.M : tier == 2 ? ItemSize.L : ItemSize.Container;
            var size = (ItemSize)rng.Next(0, (int)maxSize + 1);
            var mass = RollMass(rng, size);
            var fragility = rng.Next(ItemDefinition.MinFragility, ItemDefinition.MaxFragility + 1);

            var isAlive = tier >= 2 && rng.NextDouble() < 0.25;
            var hazards = tier >= 2 ? RollHazards(rng, isAlive) : HazardFlags.None;
            var isGuarded = tier >= 3 && rng.NextDouble() < 0.2;
            var isAnchored = tier >= 3 && rng.NextDouble() < 0.15;

            var item = new ItemDefinition(size, mass, fragility, isAlive, isGuarded, hazards, isAnchored);
            return new OrderData(tier, seed, item, ComputeBasePay(tier, item));
        }

        private static float RollMass(Random rng, ItemSize size)
        {
            double min, max;
            switch (size)
            {
                case ItemSize.S: min = 0.1; max = 2; break;
                case ItemSize.M: min = 2; max = 15; break;
                case ItemSize.L: min = 15; max = 120; break;
                default: min = 120; max = 5000; break;
            }
            return (float)Math.Round(min + rng.NextDouble() * (max - min), 1);
        }

        private static HazardFlags RollHazards(Random rng, bool isAlive)
        {
            var hazards = HazardFlags.None;
            if (rng.NextDouble() < 0.1) hazards |= HazardFlags.Fire;
            if (rng.NextDouble() < 0.05) hazards |= HazardFlags.Explosive;
            if (isAlive && rng.NextDouble() < 0.4) hazards |= HazardFlags.Bite;
            return hazards;
        }

        private static int ComputeBasePay(int tier, ItemDefinition item)
        {
            var pay = 20 * tier + 10 * (int)item.Size + 5 * item.Fragility;
            if (item.IsAlive) pay += 15;
            if (item.IsGuarded) pay += 25;
            if (item.IsAnchored) pay += 30;
            if (item.Hazards != HazardFlags.None) pay += 20;
            return pay;
        }
    }
}
