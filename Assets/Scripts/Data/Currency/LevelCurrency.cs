using UnityEngine;

public static class LevelCurrency
{
    public static int LevelExperience(int level)
    {
        if (level <= 0)
        {
            return 0; // �߸��� ���� �Է� ����
        }

        return level * 100 + (level - 1) * 50;
    }
}
