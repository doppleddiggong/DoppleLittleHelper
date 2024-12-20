using System.Collections.Generic;
using UnityEngine;

namespace DoppleLittleHelper
{
    [CreateAssetMenu(fileName = "DoppleLittleHelperSO", menuName = "DoppleLittleHelper/DoppleLittleHelperSO")]
    public class DoppleLittleHelperSO : ScriptableObject
    {
        [Header("[툴바 버튼 색상 제어]")]
        public Color color_left_area = Color.red;
        public Color color_right_area = Color.green;
        
        [Header("[타임 스케일 제어]")] 
        public List<float> time_scale_list = new() { 1.0f, 2.0f, 3.0f, 0.1f, 0.5f };
        public int time_scale_start_index = 0;

        [Header("[스크린샷 제어]")]
        public string folder_dir = "ScreenShot";
        public string prefix = "capture_";
        public bool use_date_time = true;
        public string file_format = "yyyy-MM-dd_HH-mm-ss";
        public float capture_size = 1.0f;
    }
}