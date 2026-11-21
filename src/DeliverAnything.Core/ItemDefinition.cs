using System;

namespace DeliverAnything.Core
{
    public sealed class ItemDefinition : IEquatable<ItemDefinition>
    {
        public const int MinFragility = 0;
        public const int MaxFragility = 3;

        public ItemSize Size { get; }
        public float Mass { get; }
        public int Fragility { get; }
        public bool IsAlive { get; }
        public bool IsGuarded { get; }
        public HazardFlags Hazards { get; }
        public bool IsAnchored { get; }

        public ItemDefinition(
            ItemSize size,
            float mass,
            int fragility,
            bool isAlive,
            bool isGuarded,
            HazardFlags hazards,
            bool isAnchored)
        {
            if (mass <= 0f)
                throw new ArgumentOutOfRangeException(nameof(mass), mass, "Mass must be positive.");
            if (fragility < MinFragility || fragility > MaxFragility)
                throw new ArgumentOutOfRangeException(nameof(fragility), fragility, "Fragility must be 0-3.");

            Size = size;
            Mass = mass;
            Fragility = fragility;
            IsAlive = isAlive;
            IsGuarded = isGuarded;
            Hazards = hazards;
            IsAnchored = isAnchored;
        }

        public bool HasHazard(HazardFlags hazard) => (Hazards & hazard) == hazard;

        public bool Equals(ItemDefinition other) =>
            other != null &&
            Size == other.Size &&
            Mass.Equals(other.Mass) &&
            Fragility == other.Fragility &&
            IsAlive == other.IsAlive &&
            IsGuarded == other.IsGuarded &&
            Hazards == other.Hazards &&
            IsAnchored == other.IsAnchored;

        public override bool Equals(object obj) => Equals(obj as ItemDefinition);

        public override int GetHashCode() =>
            HashCode.Combine(Size, Mass, Fragility, IsAlive, IsGuarded, Hazards, IsAnchored);

        public override string ToString() =>
            $"{Size} {Mass}kg frag={Fragility} alive={IsAlive} guarded={IsGuarded} hazards={Hazards} anchored={IsAnchored}";
    }
}
