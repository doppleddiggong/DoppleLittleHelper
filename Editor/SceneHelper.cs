using UnityEditor;
using UnityEditor.SceneManagement;

namespace DoppleLittleHelper.Editor
{
    public class SceneHelper
    {
        static string sceneToOpen;

        public static void StartScene(string scene)
        {
            if (EditorApplication.isPlaying)
                EditorApplication.isPlaying = false;

            sceneToOpen = scene;
            EditorApplication.update += OnUpdate;
        }

        public static void OpenScene(string scene) => EditorSceneManager.OpenScene(scene);

        static void OnUpdate()
        {
            if (sceneToOpen == null ||
                EditorApplication.isPlaying || EditorApplication.isPaused ||
                EditorApplication.isCompiling || EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return;
            }

            EditorApplication.update -= OnUpdate;

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(sceneToOpen);
                EditorApplication.isPlaying = true;
            }

            sceneToOpen = null;
        }
    }
}