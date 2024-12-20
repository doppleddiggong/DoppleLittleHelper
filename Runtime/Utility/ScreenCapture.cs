using System.IO;
using System.Collections;
using UnityEngine;
using System;

namespace DoppleLittleHelper
{
    public class ScreenCapture
    {
        static string folder_dir = "ScreenShot";

        static string prefix = "capture_";
        static bool use_date_time = true;
        static string file_format = "yyyy-MM-dd_HH-mm-ss";
        static float capture_size = 0.5f;

        public static void InitData(DoppleLittleHelperSO baseSO) => InitData(baseSO.folder_dir, baseSO.prefix, baseSO.file_format, baseSO.use_date_time, baseSO.capture_size);

        public static void InitData(string _folder_dir, string _prefix, string _file_format, bool _use_date_time, float _capture_size)
        {
            folder_dir = _folder_dir;
            prefix = _prefix;
            file_format = _file_format;
            use_date_time = _use_date_time;
            capture_size = _capture_size;
        }

        /// <summary>
        /// 현재 화면을 캡쳐하여 저장한다
        /// </summary>
        /// <returns></returns>
        public static IEnumerator Capture()
        {
            yield return new WaitForEndOfFrame();

            var curShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
            curShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, true);
            curShot.Apply();

            var saveTexture = ScaleTexture(curShot, (int)(Screen.width * capture_size), (int)(Screen.height * capture_size));
            string captureName = GetCaptureName();
            File.WriteAllBytes(captureName, saveTexture.EncodeToPNG());

            Debug.Log($"<color=#ffca50>>[DLH]</color> Capture Success : {captureName}");

            static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
            {
                Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);

                float incX = ((float)1 / source.width) * ((float)source.width / targetWidth);
                float incY = ((float)1 / source.height) * ((float)source.height / targetHeight);

                Color[] rpixels = result.GetPixels(0);
                for (int px = 0; px < rpixels.Length; px++)
                {
                    rpixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth),
                                        incY * ((float)Mathf.Floor(px / targetWidth)));
                }
                result.SetPixels(rpixels, 0);
                result.Apply();
                return result;
            }
        }

        /// <summary>
        /// 저장 경로 폴더를 열어준다
        /// </summary>
        public static void OpenDirPath()
        {
            System.Diagnostics.Process.Start(GetDirPath());
        }


        /// <summary>
        /// 저장 경로를 리턴해준다
        /// </summary>
        /// <returns></returns>
        static string GetDirPath()
        {
            string dirPath = Path.Combine(Application.dataPath, "..", folder_dir);

            if (Directory.Exists(dirPath) == false)
            {
                Directory.CreateDirectory(dirPath);
                Debug.Log($"{DefineData.DLH}Directory created : {dirPath}");
            }

            return dirPath;
        }

        /// <summary>
        /// 신규 파일 명을 리턴해준다
        /// </summary>
        /// <returns>신규 파일 네임</returns>
        static string GetCaptureName()
        {
            string dirPath = GetDirPath();
            int index = 1;

            string baseFileName = use_date_time ? $"{prefix}_{DateTime.Now.ToString(file_format)}.png" : $"{prefix}_{index}.png";
            string filePath = Path.Combine(dirPath, baseFileName);

            // 파일 중첩 방지: 파일이 존재하면 인덱스 추가
            while (File.Exists(filePath))
            {
                string indexedFileName = use_date_time
                    ? $"{prefix}_{DateTime.Now.ToString(file_format)}_{index}.png"
                    : $"{prefix}_{index}.png";

                filePath = Path.Combine(dirPath, indexedFileName);
                index++;
            }

            return filePath;
        }
    }
}