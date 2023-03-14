using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyTypeHolder))]
public class EnemyTypeHolderEditor : Editor
{
    private EnemyTypeHolder enemyTypeHolder;

    private void OnEnable()
    {
        enemyTypeHolder = (EnemyTypeHolder)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Display a button to add a new level
        if (GUILayout.Button("Add Level"))
        {
            enemyTypeHolder.enemiesByLevel.Add(new EnemyTypeHolder.EnemyList());
        }

        // Display a list of levels and the EnemyStats associated with each level
        for (int i = 0; i < enemyTypeHolder.enemiesByLevel.Count; i++)
        {
            EnemyTypeHolder.EnemyList enemyList = enemyTypeHolder.enemiesByLevel[i];

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Level " + i.ToString(), GUILayout.Width(60));
            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                enemyTypeHolder.enemiesByLevel.RemoveAt(i);
                break;
            }
            EditorGUILayout.EndHorizontal();

            // Display a button to add a new EnemyStats to this level
            if (GUILayout.Button("Add EnemyStats"))
            {
                enemyList.list.Add(null);
            }

            // Display the EnemyStats associated with this level
            for (int j = 0; j < enemyList.list.Count; j++)
            {
                EnemyStats enemyStats = enemyList.list[j];

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("EnemyStats " + j.ToString(), GUILayout.Width(90));
                enemyStats = (EnemyStats)EditorGUILayout.ObjectField(enemyStats, typeof(EnemyStats), false);
                enemyList.list[j] = enemyStats;
                if (GUILayout.Button("Remove", GUILayout.Width(60)))
                {
                    enemyList.list.RemoveAt(j);
                    break;
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
