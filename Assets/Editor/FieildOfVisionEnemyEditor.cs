using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Enemy))]
public class FieildOfVisionEnemyEditor : Editor
{
    private void OnSceneGUI(SceneView sceneView)
    {
        VisionEnemy();
    }

    private void VisionEnemy()
    {
        Enemy fieldOfVision = (Enemy)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(fieldOfVision.transform.position, Vector3.up, Vector3.forward, 360, fieldOfVision.AttackRadius);

        if (fieldOfVision.IsAttackTarget)
        {
            Handles.color = Color.red;
            Handles.DrawLine(fieldOfVision.transform.position, fieldOfVision.Target.transform.position);
        }
    }

    private void OnEnable() => SceneView.duringSceneGui += OnSceneGUI;

    private void OnDisable() => SceneView.duringSceneGui -= OnSceneGUI;
}
