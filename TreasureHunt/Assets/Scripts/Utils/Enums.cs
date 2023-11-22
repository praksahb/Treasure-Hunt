namespace TreasureHunt.Interactions
{
    public enum KeyType
    {
        None,
        KeyType1,
        KeyType2,
        KeyType3,
        TreasureKey,
    }
}

namespace TreasureHunt.Enemy
{
    public enum EnemyType
    {
        Enemy1 = 0,
        Enemy2 = 1,
        Enemy3 = 2,
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
        CollectKey,
        TakeDamage,
        Flamethrower,
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
