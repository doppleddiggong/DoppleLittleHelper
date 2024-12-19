using System.IO;
using System.Collections;
using UnityEngine;

namespace DoppleLittleHelper
{
    public class ScreenCapture
    {
        // 저장 폴더 이름
        static string _saveDir = "ScreenShot";
        // 스케일 비율
        static float _resize = 0.5f;

        /// <summary>
        /// 현재 화면을 캡쳐하여 저장한다
        /// </summary>
        /// <returns></returns>
        public static IEnumerator Capture()
        {
            yield return new WaitForEndOfFrame();
            _resize = Mathf.Max(_resize, 1.0f);

            Texture2D curShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true); // texture formoat setting
            curShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, true); // readPixel
            curShot.Apply();

            Texture2D saveTexture = ScaleTexture(curShot, (int)(Screen.width * _resize), (int)(Screen.height * _resize));
            byte[] bytes = saveTexture.EncodeToPNG();
            File.WriteAllBytes(GetCaptureName(), bytes);

            Debug.Log($"<color=#52CC29> Capture Success : {GetCaptureName()}</color>");
        }

        /// <summary>
        /// 저장 경로 폴더를 열어준다
        /// </summary>
        public static void OpenDirPath()
        {
            System.Diagnostics.Process.Start(GetDirPath());
        }

        static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
        {
            Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
            Color[] rpixels = result.GetPixels(0);
            float incX = ((float)1 / source.width) * ((float)source.width / targetWidth);
            float incY = ((float)1 / source.height) * ((float)source.height / targetHeight);
            for (int px = 0; px < rpixels.Length; px++)
            {
                rpixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth),
                                    incY * ((float)Mathf.Floor(px / targetWidth)));
            }
            result.SetPixels(rpixels, 0);
            result.Apply();
            return result;
        }

        /// <summary>
        /// 신규 파일 명을 리턴해준다
        /// </summary>
        /// <returns>신규 파일 네임</returns>
        static string GetCaptureName()
        {
            string dirPath = GetDirPath();
            if ( Directory.Exists(dirPath) == false )
                Directory.CreateDirectory(dirPath);

            return $"{dirPath}/capture_{System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.png";
        }

        /// <summary>
        /// 저장 경로를 리턴해준다
        /// </summary>
        /// <returns></returns>
        static string GetDirPath()
        {
            string dirPath = $"{Application.dataPath}/../{_saveDir}";

            if (Directory.Exists(dirPath) == false)
                Directory.CreateDirectory(dirPath);

            return dirPath;
        }
    }
}