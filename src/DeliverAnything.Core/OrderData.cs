using System;

namespace DeliverAnything.Core
{
    public sealed class OrderData : IEquatable<OrderData>
    {
        public int Tier { get; }
        public int Seed { get; }
        public ItemDefinition Item { get; }
        public int BasePay { get; }

        public OrderData(int tier, int seed, ItemDefinition item, int basePay)
        {
            Tier = tier;
            Seed = seed;
            Item = item ?? throw new ArgumentNullException(nameof(item));
            BasePay = basePay;
        }

        public bool Equals(OrderData other) =>
            other != null &&
            Tier == other.Tier &&
            Seed == other.Seed &&
            BasePay == other.BasePay &&
            Item.Equals(other.Item);

        public override bool Equals(object obj) => Equals(obj as OrderData);

        public override int GetHashCode() => HashCode.Combine(Tier, Seed, Item, BasePay);

        public override string ToString() => $"Order(tier={Tier}, seed={Seed}, pay={BasePay}, item=[{Item}])";
    }
}
