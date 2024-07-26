using UnityEngine;

public static class LevelCurrency
{
    public static int LevelExperience(int level)
    {
        if (level <= 0)
        {
            return 0; // 잘못된 레벨 입력 방지
        }

        return level * 100 + (level - 1) * 50;
    }
}
