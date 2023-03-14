using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeHolder : MonoBehaviour
{
    public static EnemyTypeHolder instance;

    private void Awake()
    {
        instance = this;
    }

    [System.Serializable]
    public class EnemyList
    {
        public List<EnemyStats> list = new List<EnemyStats>();
    }

    public List<EnemyList> enemiesByLevel = new List<EnemyList>();

    public List<EnemyStats> GetEnemyList(int level)
    {
        return enemiesByLevel[level].list;
    }
}
