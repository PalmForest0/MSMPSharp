namespace MSMPSharp.Models.Game;

public sealed class Player(string name, string id) : IEquatable<Player>
{
    public string Name { get; set; } = name;
    public string Id { get; set; } = id;

    public override int GetHashCode() => Id?.GetHashCode() ?? 0;

    public bool Equals(Player? other) => other is not null && Id == other.Id;

    public override bool Equals(object? obj)
    {
        if (obj is not Player other)
            return false;

        return Equals(other);
    }

    public static bool operator ==(Player? left, Player? right)
    {
        if (left is null)
            return right is null;

        return left.Equals(right);
    }

    public static bool operator !=(Player? left, Player? right) => !(left == right);
}