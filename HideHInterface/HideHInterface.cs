using BepInEx;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.ComponentModel;


namespace HideHInterface {

    [BepInProcess("Koikatu")]
    [BepInPlugin("HideHInterface", "Hide H Interface", "1.0.1")]
    public class HideHInterface : BaseUnityPlugin {
        private bool inHScene;

        [DisplayName("Hide H UI toggle")]
        [Description("The hotkey that toggles the H UI on or off.")]
        public static SavedKeyboardShortcut ToggleKey { get; private set; }


        void Start() {
            ToggleKey = new SavedKeyboardShortcut("Hide H UI", this, new KeyboardShortcut(KeyCode.Space));
            inHScene = false;
            SceneManager.sceneLoaded += Act;
        }

        void Update() {
            if (HideHInterface.ToggleKey.IsDown() && inHScene) {
                foreach (Canvas ui in FindObjectsOfType<Canvas>()) {
                    if (ui.name == "Canvas") {
                        ui.enabled = !ui.enabled;
                    }
                }
            }
        }

        void Act(Scene scene, LoadSceneMode load) {         
            inHScene = (FindObjectOfType<HScene>() != null);         
        }
    }
}





