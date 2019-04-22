using BepInEx;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.ComponentModel;


namespace HideHInterface {

    [BepInProcess("Koikatu")]
    [BepInPlugin("HideHInterface", "Hide H Interface", "1.0")]
    public class HideHInterface : BaseUnityPlugin {
        [DisplayName("Hide H UI toggle")]
        [Description("The hotkey that toggles the H UI on or off.")]
        public static SavedKeyboardShortcut ToggleKey { get; private set; }


        HideHInterface() {
            ToggleKey = new SavedKeyboardShortcut("Hide H UI", this, new KeyboardShortcut(KeyCode.Space));
        }

        void Awake() {
            SceneManager.sceneLoaded += Act;
        }

        void OnDestroy() {
            SceneManager.sceneLoaded -= Act;
        }

        void Act(Scene scene, LoadSceneMode load) {
            var check = gameObject.GetComponent<HideMB>();

            if (FindObjectOfType<HScene>()) {
                if (!check) {
                    gameObject.AddComponent<HideMB>();
                }
            } else if (check) {
                Destroy(check);
            }
        }
    }


    public class HideMB : MonoBehaviour {

        private void Update() {
            if (HideHInterface.ToggleKey.IsDown()) {
                foreach (Canvas ui in FindObjectsOfType<Canvas>()) {
                    if (ui.name == "Canvas") {
                        ui.enabled = !ui.enabled;
                    }
                }
            }
        }
    }
}


