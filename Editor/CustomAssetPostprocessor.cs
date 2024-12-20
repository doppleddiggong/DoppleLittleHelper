using System;
using UnityEditor;
using UnityEngine;

namespace DoppleLittleHelper.Editor
{
    public class CustomAssetPostprocessor : AssetPostprocessor
    {
        // 모든 에셋이 처리된 후 호출되는 메서드
        static void OnPostprocessAllAssets(
            string[] importedAssets, string[] deletedAssets,
            string[] movedAssets, string[] movedFromAssetPaths)
        {
            {
                // SO 파일 수정시 
                string assetPath = DefineData.LocalAssetPath(DefineData.DLH_SO);
                
                // target asset이 변경되었는지 확인
                if (Array.Exists(importedAssets, asset => asset == assetPath))
                {
                    ToolbarCustom.ReloadData();
                    Debug.Log($"{DefineData.DLH}{DefineData.DLH_SO}.asset has been updated. Reloading ToolbarCustom data.");
                }
            }
        }
    }
}