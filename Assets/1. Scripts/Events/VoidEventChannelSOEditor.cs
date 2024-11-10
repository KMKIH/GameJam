using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VoidEventChannelSO))]
public class VoidEventChannelSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        VoidEventChannelSO eventChannel = (VoidEventChannelSO)target;

        // 연결된 이벤트 리스너를 표시합니다.
        if (eventChannel.OnEventRaised != null)
        {
            foreach (var invocation in eventChannel.OnEventRaised.GetInvocationList())
            {
                EditorGUILayout.LabelField("Registered Listener:", invocation.Target.ToString());
                EditorGUILayout.LabelField("Method:", invocation.Method.Name);
            }
        }
        else
        {
            EditorGUILayout.HelpBox("No listeners registered.", MessageType.Info);
        }

        if (GUILayout.Button("Raise Event"))
        {
            eventChannel.RaiseEvent();
        }
    }
}

