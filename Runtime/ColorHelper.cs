using UnityEngine;

namespace DoppleLittleHelper
{
    public class ColorHelper
    {
        // #RRGGBB 형태의 Hex 색상 코드를 Color로 변환
        public static Color HexToColor(string hex)
        {
            // '#' 제거
            hex = hex.Replace("#", "");

            // 색상 코드가 잘못된 경우 예외 처리
            if (hex.Length != 6)
                throw new System.ArgumentException("Hex color must be in #RRGGBB format");

            // 각 색상 값 추출
            float r = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
            float g = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
            float b = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / 255f;

            // Color 객체 반환
            return new Color(r, g, b);
        }

        // 예시: #50C850는 녹색에 가까운 색
        public static readonly Color Green = HexToColor("#50C850");

        // 예시: #FA805C는 갈색에 가까운 색
        public static readonly Color Brown = HexToColor("#FA805C");
    }
}