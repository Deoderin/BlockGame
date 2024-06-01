using System;

[Serializable]
public class PlayerProgress
{
    public PlayerProgress(int initialLevel)
    {
        worldData = new WorldData(initialLevel);
    }

    public WorldData worldData;
}

[Serializable]
public class WorldData
{
    public WorldData(int initializeLevel)
    {
        level = initializeLevel;
    }

    public int level;
}
