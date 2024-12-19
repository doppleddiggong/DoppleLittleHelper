using UnityEditor;
using UnityEngine;

namespace DoppleLittleHelper
{
    public static class ToolbarStyles
    {
        public static readonly GUIStyle commandButtonStyle;
        private const string IconPath = "Packages/com.dopple.dopple-little-helper/Texture/Icon_Shape/";

        static ToolbarStyles()
        {
            commandButtonStyle = new GUIStyle("Command")
            {
                fontSize = 10,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Bold
            };
        }

        /// <summary>
        /// 에디터 아이콘을 로드합니다.
        /// </summary>
        /// <param name="fileName">아이콘 파일 이름 (확장자 제외)</param>
        /// <returns>로드된 Texture 객체 (없으면 null)</returns>
        public static Texture GetEditorIcon(string fileName)
        {
            string fullPath = $"{IconPath}{fileName}.png";

            if (System.IO.File.Exists(fullPath) == false)
            {
                //Debug.LogWarning($"Icon file not found at path:{fullPath}");
                return EditorGUIUtility.IconContent("d_UnityEditor.SceneView").image;
            }

            return EditorGUIUtility.Load(fullPath) as Texture;
        }
    }
}