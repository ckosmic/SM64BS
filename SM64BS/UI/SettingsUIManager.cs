using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.FloatingScreen;
using HMUI;
using SM64BS.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SM64BS.UI
{
    internal class SettingsUIManager : MonoBehaviour
    {
        private FloatingScreen _floatingScreen;
        private Transform _floatingScreenTransform;
        private Transform _cameraTransform;

        private MenuMarioManager _marioManager;

        public SettingsViewController settingsViewController;
        public bool modalOpen = false;

        public void Initialize(MenuMarioManager marioManager)
        {
            _marioManager = marioManager;
        }

        public FloatingScreen CreateFloatingScreen()
        {
            if (settingsViewController == null)
            {
                settingsViewController = BeatSaberUI.CreateViewController<SettingsViewController>();
                settingsViewController.Initialize(_marioManager);
                settingsViewController.modalHidden += ModalHiddenHandler;
            }

            _cameraTransform = Camera.main.transform;

            _floatingScreen = FloatingScreen.CreateFloatingScreen(new Vector2(80f, 60f), false, Vector3.zero, Quaternion.identity);
            _floatingScreen.SetRootViewController(settingsViewController, ViewController.AnimationType.None);
            

            _floatingScreenTransform = _floatingScreen.transform;
            _floatingScreenTransform.SetParent(transform);
            _floatingScreenTransform.localPosition = Vector3.up * 1.75f;
            _floatingScreenTransform.name = "MarioSettingsScreen";

            return _floatingScreen;
        }

        public void ShowModal()
        {
            settingsViewController.ShowModal(true);
            modalOpen = true;
        }
        public void HideModal()
        {
            settingsViewController.HideModal(true);
            modalOpen = false;
        }

        private void ModalHiddenHandler()
        {
            modalOpen = false;
        }

        private void Update()
        {
            // Tried to avoid this with the nameplate's shader but I need dat UI collision :(
            Vector3 lookRotationFlat = _floatingScreenTransform.position - _cameraTransform.position;
            lookRotationFlat.y = 0;
            lookRotationFlat.Normalize();
            _floatingScreenTransform.rotation = Quaternion.LookRotation(lookRotationFlat);
        }
    }
}
