using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DoppleLittleHelper;
using DoppleLittleHelper.Editor;

public class ToolbarButtonData
{
    public string IconPath { get; }
    public string Tooltip { get; }
    public System.Action Action { get; }

    public ToolbarButtonData(string iconPath, string tooltip, System.Action action)
    {
        IconPath = iconPath;
        Tooltip = tooltip;
        Action = action;
    }
}

[InitializeOnLoad]
public class ToolbarCustom
{
    static readonly List<ToolbarButtonData> right_button;

    static ToolbarCustom()
	{
        right_button = new List<ToolbarButtonData> {
            new ToolbarButtonData( "shape_001", "Start FirstScene",
                () =>
                {
                    // EditorBuildSettings에서 첫 번째 씬의 경로 가져오기
                    if (EditorBuildSettings.scenes.Length > 0)
                        SceneHelper.StartScene( EditorBuildSettings.scenes[0].path );
                    else
                        Debug.LogError("No scenes are available in the build settings.");
                }
            ),

            new ToolbarButtonData( "shape_016", "Screen Capture",
                // 화면 캡쳐하기
                () => StaticCoroutine.DoCoroutine(DoppleLittleHelper.ScreenCapture.Capture())
            ),

            new ToolbarButtonData( "shape_017", "Open Capture Folder",
                () => DoppleLittleHelper.ScreenCapture.OpenDirPath()
            ),

            new ToolbarButtonData( "shape_091", "Open UserData Folder",
                () => System.Diagnostics.Process.Start(Application.persistentDataPath)
            ),

            new ToolbarButtonData( "shape_046", "Open Editor.Log",
                () => {
                    string editorLogPath = GetEditorLogPath();
                    if (string.IsNullOrEmpty(editorLogPath))
                        Debug.LogError("Editor.log path is empty or invalid.");
                    else
                        Debug.Log("Path is empty");

                    // Editor log 경로를 반환하는 메서드
                    static string GetEditorLogPath()
                    {
                        #if UNITY_EDITOR_WIN
                            return FileHelper.CombinePaths( 
                                System.Environment.GetEnvironmentVariable("AppData"), "..", "Local", "Unity", "Editor", "Editor.log");
                        #else
                            Debug.Log("Editor.Log path is not supported on this platform.");
                            return string.Empty;
                        #endif
                    }
                }
            )
        };

        UnityToolbarExtender.ToolbarExtender.LeftToolbarGUI.Add(OnToolbarLeftGUI);
        UnityToolbarExtender.ToolbarExtender.RightToolbarGUI.Add(OnToolbarRightGUI);
	}


    static float timeScaleMin = 1.0f;
    static float timeScaleMax = 5.0f;
    static float timeScaleAdd = 0.5f;

    static void OnToolbarLeftGUI()
    {
        GUILayout.FlexibleSpace();

        using (new BackgroundColorScope(BackgroundColorScope.green))
        {
            if (GUILayout.Button(new GUIContent($"x{Time.timeScale}", $"timeScale {timeScaleMin}~{timeScaleMax}"),
                ToolbarStyles.commandButtonStyle))
            {
                Time.timeScale += timeScaleAdd;

                if (Time.timeScale > timeScaleMax ||
                    Time.timeScale == 1.1f )
                {
                    Time.timeScale = timeScaleMin;
                }
            }
        }
    }

    static void OnToolbarRightGUI()
	{
        using (new BackgroundColorScope(BackgroundColorScope.brown))
        {
            foreach (var button in right_button)
            {
                if (GUILayout.Button(new GUIContent(ToolbarStyles.GetEditorIcon(button.IconPath), button.Tooltip), 
                    EditorStyles.toolbarButton, GUILayout.Width(32), GUILayout.Height(32)))
                {
                    button.Action.Invoke();
                }
            }
        }
    }
}