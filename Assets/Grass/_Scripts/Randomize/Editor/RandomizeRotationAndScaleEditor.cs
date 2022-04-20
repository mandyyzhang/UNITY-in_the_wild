using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(RandomizeRotationAndScale))]
public class RandomizeRotationAndScaleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Randomize Transform"))
        {
            if (targets.Length == 1)
            {
                RandomizeRotationAndScale rt = (RandomizeRotationAndScale)target;
                rt.RandomizeTransform();
            }
            else
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    RandomizeRotationAndScale rt = (RandomizeRotationAndScale)targets[i];

                    rt.RandomizeTransform();
                }
            }
        }
    }
}