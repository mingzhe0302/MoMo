using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyAI)), CanEditMultipleObjects]
public class EnemyBehaviourEditor : Editor
{
    public SerializedProperty
        MovementBehaviourProperty,
        AttackBehaviourProperty;

    private void OnEnable()
    {
        MovementBehaviourProperty = serializedObject.FindProperty("movementBehaviour");
        AttackBehaviourProperty = serializedObject.FindProperty("attackBehaviour");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.UpdateIfRequiredOrScript();

        EditorGUILayout.PropertyField(MovementBehaviourProperty);
        EditorGUILayout.PropertyField(AttackBehaviourProperty);
        
        EditorGUILayout.PropertyField(AttackBehaviourProperty, new GUIContent("Max Health"));

        // PropertyHolder.Status st = (PropertyHolder.Status)state_Prop.enumValueIndex;
        //
        // switch( st ) {
        //     case PropertyHolder.Status.A:			
        //         EditorGUILayout.PropertyField( controllable_Prop, new GUIContent("controllable") );			
        //         EditorGUILayout.IntSlider ( valForA_Prop, 0, 10, new GUIContent("valForA") );
        //         EditorGUILayout.IntSlider ( valForAB_Prop, 0, 100, new GUIContent("valForAB") );
        //         break;
        //
        //     case PropertyHolder.Status.B:			
        //         EditorGUILayout.PropertyField( controllable_Prop, new GUIContent("controllable") );	
        //         EditorGUILayout.IntSlider ( valForAB_Prop, 0, 100, new GUIContent("valForAB") );
        //         break;
        //
        //     case PropertyHolder.Status.C:			
        //         EditorGUILayout.PropertyField( controllable_Prop, new GUIContent("controllable") );	
        //         EditorGUILayout.IntSlider ( valForC_Prop, 0, 100, new GUIContent("valForC") );
        //         break;
        //
        // }


        serializedObject.ApplyModifiedProperties();
    }
}
