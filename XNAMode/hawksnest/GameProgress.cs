using System;

public class LevelData
{
    private static LevelData instance;

    public static int[] humanHeart;
    public static int[] soul;
    public static int score;


    private LevelData() 
    { 
    }

    public static LevelData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LevelData();
            }
            return instance;
        }
    }
    public static void reset()
    {

    }
}