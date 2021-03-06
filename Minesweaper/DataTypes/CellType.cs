namespace Minesweaper
{
    /// <summary>
    /// The type of the cell goes from 0 - 9
    /// </summary>
    internal enum CellType : byte
    {
        None = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Mine = 9,
    }
}