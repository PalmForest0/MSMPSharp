namespace MSMPSharp.Models.Game;

public sealed class Player : IEquatable<Player>
{
    public string Name { get; set; }
    public string Id { get; set; }

    public Player(string name, string id)
    {
        Name = name;
        Id = id;
    }

    public bool Equals(Player? other) => other is not null && Id == other.Id;

    public override int GetHashCode() => Id?.GetHashCode() ?? 0;

    public static bool operator ==(Player? left, Player? right)
    {
        if (left is null)
            return right is null;
        
        return left.Equals(right);
    }

    public static bool operator !=(Player? left, Player? right) => !(left == right);

    public override bool Equals(object? obj)
    {
        if (obj is not Player other)
            return false;

        return Equals(other);
    }
}