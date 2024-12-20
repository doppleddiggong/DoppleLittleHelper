using System.IO;
using UnityEditor;
using UnityEngine;

namespace DoppleLittleHelper
{
    public class DataManager
    {
        public static T LoadOrCreateUserData<T>(string asset_name) where T : ScriptableObject
        {
            var user_path = DefineData.LocalAssetPath(asset_name);
            var local_data = AssetDatabase.LoadAssetAtPath<T>(user_path);

            // 사용자 경로에서 에셋 로드
            if (local_data != null)
            {
                //Debug.Log($"{DefineData.DLH} Successfully loaded user asset at: {user_path}");
                return local_data;
            }

            // Packages 경로의 기본 데이터 복사
            string packagePath = DefineData.PackageAssetPath(asset_name);
            var defaultData = AssetDatabase.LoadAssetAtPath<T>(packagePath);

            if (defaultData != null)
            {
                string dirPath = Path.GetDirectoryName(user_path);
                if ( Directory.Exists(dirPath) == false )
                {
                    Directory.CreateDirectory(dirPath);
                    Debug.Log($"{DefineData.DLH}Directory created: {dirPath}");
                }

                var newData = Object.Instantiate(defaultData);
                AssetDatabase.CreateAsset(newData, user_path);
                AssetDatabase.SaveAssets();
                Debug.Log($"{DefineData.DLH}Created New Asset at: {user_path}");
                return newData;
            }

            Debug.LogWarning($"{DefineData.DLH}Could not find asset at both user path ({user_path}) and package path ({packagePath}).");
            return null;
        }
    }
}