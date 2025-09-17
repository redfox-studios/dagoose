using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HealthWithVisuals))]
public class HealthWithVisualsEditor : Editor
{
    SerializedProperty maxHealth;
    SerializedProperty destroyOnDeath;
    SerializedProperty OnDeath;
    SerializedProperty changeSpriteOnDamage;
    SerializedProperty spriteRenderer;
    SerializedProperty healthSprites;

    void OnEnable()
    {
        maxHealth = serializedObject.FindProperty("maxHealth");
        destroyOnDeath = serializedObject.FindProperty("destroyOnDeath");
        OnDeath = serializedObject.FindProperty("OnDeath");
        changeSpriteOnDamage = serializedObject.FindProperty("changeSpriteOnDamage");
        healthSprites = serializedObject.FindProperty("healthSprites");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.PropertyField(maxHealth);
        EditorGUILayout.PropertyField(destroyOnDeath);
        EditorGUILayout.PropertyField(OnDeath);
        EditorGUILayout.PropertyField(changeSpriteOnDamage);

        if (changeSpriteOnDamage.boolValue)
        {
            EditorGUILayout.PropertyField(healthSprites, true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
