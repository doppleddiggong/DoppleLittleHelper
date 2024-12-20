using UnityEngine;

namespace DoppleLittleHelper
{
    public class BackgroundColorScope : GUI.Scope
    {
        private readonly Color origin_color;

        public static Color green = ColorHelper.Green;  // #50C850
        public static Color brown = ColorHelper.Brown;  // #FA805C

        public BackgroundColorScope(Color color) 
        {
            this.origin_color = GUI.backgroundColor; 
            GUI.backgroundColor = color; 
        }
        
        protected override void CloseScope() => GUI.backgroundColor = origin_color;
    }
}
