namespace TreasureHunt.Interactions
{
    public enum KeyType
    {
        None,
        KeyType1,
        KeyType2,
        KeyType3,
        KeyType4,
        TreasureKey,
    }
}

namespace TreasureHunt.Enemy
{
    public enum EnemyType
    {
        None,
        Enemy1,
        Enemy2,
        Enemy3,
    }
}

namespace TreasureHunt.Sounds
{
    public enum MusicType
    {
        None,
        MainMenu,
        PauseMenu,
        GameOver,
        GameWon,
        Bg_Seaside_Waves,
    }

    public enum SfxType
    {
        None,
        DoorOpen,
        DoorClosed,
        DoorLocked,
        DoorUnlocked,
        Footsteps,
    }
}

namespace TreasureHunt.UI
{
    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns,
    }
}

namespace TreasureHunt.MainMenu
{
    public enum Level
    {
        MainMenu = 0,
        TestLevel = 2,
    }
}
