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

    public enum InteractionType
    {
        ClearData = -1,
        DoorLocked = 0,
        DoorOpen,
        DoorClose,
        CollectKey,
        TreasureLocked,
        TreasureCollect,
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

// root
namespace TreasureHunt
{
    public enum Level
    {
        MainMenu = 0,
        TestLevel = 1,
        Restart = 10,
    }
}
