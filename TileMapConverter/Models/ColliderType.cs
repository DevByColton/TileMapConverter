namespace TileMapConverter.Models;

public enum ColliderType
{
    Vertical,
    Horizontal,
    Box,
    None
}

public static class ColliderTypeMapper
{
    public static ColliderType Get(string direction)
    {
        if (string.IsNullOrEmpty(direction))
            throw new Exception("Collider group direction string is empty!");
            
        switch (direction)
        {
            case "v": return ColliderType.Vertical;
            case "h": return ColliderType.Horizontal;
            case "b": return ColliderType.Box;
            case "n": return ColliderType.None;
            default: throw new Exception($"Collider group direction of {direction} is not properly mapped!");
        }
    }
}