namespace TileMapConverter.Models;

public enum ColliderGroupDirection
{
    Vertical,
    Horizontal,
    Box,
    None
}

public static class ColliderGroupDirectionMapper
{
    public static ColliderGroupDirection Get(string direction)
    {
        if (string.IsNullOrEmpty(direction))
            throw new Exception("Collider group direction string is empty!");
            
        switch (direction)
        {
            case "v": return ColliderGroupDirection.Vertical;
            case "h": return ColliderGroupDirection.Horizontal;
            case "b": return ColliderGroupDirection.Box;
            case "n": return ColliderGroupDirection.None;
            default: throw new Exception($"Collider group direction of {direction} is not properly mapped!");
        }
    }
}