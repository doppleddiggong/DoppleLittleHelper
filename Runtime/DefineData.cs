namespace DoppleLittleHelper
{
    public class DefineData
    {
        public static string DLH = "<color=#ffca50>[DLH]</color>";

        public static string PROJECT_NAME = "DoppleLittleHelper";
        public static string PACKAGE_NAME = "com.dopple.dopple-little-helper";
        public static string DLH_SO = "DoppleLittleHelperSO";

        public static string LocalAssetPath(string asset_name) => $"Assets/{PROJECT_NAME}/{asset_name}.asset";
        public static string PackageAssetPath(string asset_name) => $"Packages/{PACKAGE_NAME}/{asset_name}.asset";
        public static string ToolbarIconPath(string file_name) => $"Packages/{PACKAGE_NAME}/Texture/Icon_Shape/{file_name}.png";
    }
}
