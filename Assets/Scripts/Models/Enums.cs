using System;

namespace Assets.Scripts.Models
{
    [Flags]
    public enum MoveDirection
    {
        None = 0x0,
        Right = 0x1,
        Up = 0x2,
        Left = 0x4,
        Down = 0x8,
        RightUp = Right | Up,
        LeftUp = Left | Up,
        LeftDown = Left | Down,
        RightDown = Right | Down
    }

    public enum SpaceChanging
    {
        Expansion,
        Narrowing
    }
}
