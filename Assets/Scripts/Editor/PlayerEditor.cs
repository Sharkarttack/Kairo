using UnityEditor;

[CustomEditor(typeof(PlayerMainSet), true)]
public class PlayerEditor : Editor
{
    SerializedProperty Rigidbody;
    SerializedProperty OUT_DAMAGE_TRIGGER;
    SerializedProperty IN_DAMAGE_TRIGGER;
    SerializedProperty Enviroment;
    SerializedProperty VIDA_MAX;
    SerializedProperty vida_actual;
    SerializedProperty WALK_SPEED;
    SerializedProperty RUN_SPEED;
    SerializedProperty TIME_TO_START_RUNING;
    SerializedProperty TIME_BETWEN_MOVE_INPUTS;
    SerializedProperty speed_actual;
    SerializedProperty isRunging;
    SerializedProperty DASH_FORCE;
    SerializedProperty DASH_COOLDOWN;
    SerializedProperty DASH_TIME;
    SerializedProperty isDashing;
    SerializedProperty rechargingDash;
    SerializedProperty BASE_DAMAGE;
    SerializedProperty damage_actual;
    SerializedProperty INMUNITY_DASH_HABILITY;
    SerializedProperty DAMAGING_DASH_HABILITY;
    SerializedProperty CHARGED_ATACK_HABILITY;
    SerializedProperty COMBO_ATACK_HABILITY;
    SerializedProperty VENENO_ATACK_HABILITY;
    SerializedProperty LEECHLIVE_ATACK_HABILITY;
    SerializedProperty LOCKON_ATACK_HABILITY;

    private void OnEnable()
    {
        Rigidbody = serializedObject.FindProperty("rb");
        OUT_DAMAGE_TRIGGER = serializedObject.FindProperty("OUT_DAMAGE_TRIGGER");
        IN_DAMAGE_TRIGGER = serializedObject.FindProperty("IN_DAMAGE_TRIGGER");
        Enviroment = serializedObject.FindProperty("enviroment");
        VIDA_MAX = serializedObject.FindProperty("VIDA_MAX");
        vida_actual = serializedObject.FindProperty("vida_actual");
        WALK_SPEED = serializedObject.FindProperty("WALK_SPEED");
        RUN_SPEED = serializedObject.FindProperty("RUN_SPEED");
        TIME_TO_START_RUNING = serializedObject.FindProperty("TIME_TO_START_RUNING");
        TIME_BETWEN_MOVE_INPUTS = serializedObject.FindProperty("TIME_BETWEN_MOVE_INPUTS");
        speed_actual = serializedObject.FindProperty("speed_actual");
        isRunging = serializedObject.FindProperty("isRunging");
        DASH_FORCE = serializedObject.FindProperty("DASH_FORCE");
        DASH_COOLDOWN = serializedObject.FindProperty("DASH_COOLDOWN");
        DASH_TIME = serializedObject.FindProperty("DASH_TIME");
        isDashing = serializedObject.FindProperty("isDashing");
        rechargingDash = serializedObject.FindProperty("rechargingDash");
        BASE_DAMAGE = serializedObject.FindProperty("BASE_DAMAGE");
        damage_actual = serializedObject.FindProperty("damage_actual");
        INMUNITY_DASH_HABILITY = serializedObject.FindProperty("INMUNITY_DASH_HABILITY");
        DAMAGING_DASH_HABILITY = serializedObject.FindProperty("DAMAGING_DASH_HABILITY");
        CHARGED_ATACK_HABILITY = serializedObject.FindProperty("CHARGED_ATACK_HABILITY");
        COMBO_ATACK_HABILITY = serializedObject.FindProperty("COMBO_ATACK_HABILITY");
        VENENO_ATACK_HABILITY = serializedObject.FindProperty("VENENO_ATACK_HABILITY");
        LEECHLIVE_ATACK_HABILITY = serializedObject.FindProperty("LEECHLIVE_ATACK_HABILITY");
        LOCKON_ATACK_HABILITY = serializedObject.FindProperty("LOCKON_ATACK_HABILITY");

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(Rigidbody);
        EditorGUILayout.PropertyField(OUT_DAMAGE_TRIGGER);
        EditorGUILayout.PropertyField(IN_DAMAGE_TRIGGER);
        EditorGUILayout.PropertyField(Enviroment);
        EditorGUILayout.PropertyField(VIDA_MAX);
        EditorGUILayout.PropertyField(vida_actual);
        EditorGUILayout.PropertyField(WALK_SPEED);
        EditorGUILayout.PropertyField(RUN_SPEED);
        EditorGUILayout.PropertyField(TIME_TO_START_RUNING);
        EditorGUILayout.PropertyField(TIME_BETWEN_MOVE_INPUTS);
        EditorGUILayout.PropertyField(speed_actual);
        EditorGUILayout.PropertyField(isRunging);
        EditorGUILayout.PropertyField(DASH_FORCE);
        EditorGUILayout.PropertyField(DASH_COOLDOWN);
        EditorGUILayout.PropertyField(DASH_TIME);
        EditorGUILayout.PropertyField(isDashing);
        EditorGUILayout.PropertyField(rechargingDash);
        EditorGUILayout.PropertyField(BASE_DAMAGE);
        EditorGUILayout.PropertyField(damage_actual);
        EditorGUILayout.PropertyField(INMUNITY_DASH_HABILITY);
        EditorGUILayout.PropertyField(DAMAGING_DASH_HABILITY);
        EditorGUILayout.PropertyField(CHARGED_ATACK_HABILITY);
        EditorGUILayout.PropertyField(COMBO_ATACK_HABILITY);
        EditorGUILayout.PropertyField(VENENO_ATACK_HABILITY);
        EditorGUILayout.PropertyField(LEECHLIVE_ATACK_HABILITY);
        EditorGUILayout.PropertyField(LOCKON_ATACK_HABILITY);

        serializedObject.ApplyModifiedProperties();
    }
}
