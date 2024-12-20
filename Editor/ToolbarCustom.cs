using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DoppleLittleHelper.Editor
{
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
    public static class ToolbarCustom
    {
        static List<float> timeScaleList = new();
        static int timeScaleIndex = 2;

        static readonly List<ToolbarButtonData> right_button;
        static Color color_left;
        static Color color_right;

        static ToolbarCustom()
	    {
            ReloadData();

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

        // 데이터 수정 후 호출하여 데이터를 갱신하는 메서드
        public static void ReloadData()
        {
            var baseSO = DataManager.LoadOrCreateUserData<DoppleLittleHelperSO>(DefineData.DLH_SO);
            if (baseSO == null)
            {
                Debug.LogWarning($"{DefineData.DLH}ToolbarCustom Load Data Failed : {DefineData.DLH_SO}");
                return;
            }

            color_left = baseSO.color_left_area;
            color_right = baseSO.color_right_area;

            timeScaleList = baseSO.time_scale_list;
            timeScaleIndex = baseSO.time_scale_start_index;

            ScreenCapture.InitData(baseSO);
        }

        static void OnToolbarLeftGUI()
        {
            GUILayout.FlexibleSpace();

            using (new BackgroundColorScope(color_left))
            {
                if(timeScaleList.Count > 1 )
                {
                    if (GUILayout.Button(new GUIContent($"x{Time.timeScale.ToString("f1")}",
                        $"timeScale {timeScaleList.First().ToString("f1")}~{timeScaleList.Last().ToString("f1")}"),
                        ToolbarStyles.commandButtonStyle))
                    {
                        // 다음 인덱스로 이동 (리스트를 순환)
                        timeScaleIndex = (timeScaleIndex + 1) % timeScaleList.Count;
                        Time.timeScale = timeScaleList[timeScaleIndex];
                    }
                }
            }
        }

        static void OnToolbarRightGUI()
	    {
            using (new BackgroundColorScope(color_right))
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
}