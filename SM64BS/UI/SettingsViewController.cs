using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using IPA.Utilities;
using LibSM64;
using SM64BS.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SM64BS.UI
{
    [HotReload(RelativePathToLayout = @"Views\settingsView.bsml")]
    [ViewDefinition("SM64BS.UI.Views.settingsView.bsml")]
    internal class SettingsViewController : BSMLAutomaticViewController
    {
        private MenuMarioManager _marioManager;
        private BasicUIAudioManager _basicUIAudioManager;

        [UIComponent("close-button")]
        private Transform _closeButtonTransform;

        [UIComponent("header-bar")]
        private ImageView _headerBarImageView;
        [UIComponent("main-modal")]
        private ModalView _modal;

        public Action modalHidden;

        #region UIValues

        [UIValue("name")]
        public string Name
        { 
            get { return Plugin.Settings.MarioName; }
            set
            { 
                Plugin.Settings.MarioName = value;
                _marioManager.namePlate.SetNamePlateText(value);
            }
        }
        [UIValue("show-nameplate")]
        public bool ShowNamePlate
        {
            get { return Plugin.Settings.ShowNamePlate; }
            set
            {
                Plugin.Settings.ShowNamePlate = value;
                _marioManager.namePlate.gameObject.SetActive(value);
            }
        }
        [UIValue("miss-mario")]
        public bool SpawnMarioOnMiss
        {
            get { return Plugin.Settings.SpawnMarioOnMiss; }
            set { Plugin.Settings.SpawnMarioOnMiss = value; }
        }
        [UIValue("max-marios")]
        public int MaxMarios
        {
            get { return Plugin.Settings.MaxMarios; }
            set { Plugin.Settings.MaxMarios = value; }
        }
        [UIValue("shade-blue")]
        public Color ShadeBlue
        {
            get { return Plugin.Settings.MarioColors[0]; }
            set { ApplyColor(0, value); }
        }
        [UIValue("blue")]
        public Color Blue
        { 
            get { return Plugin.Settings.MarioColors[1]; }
            set { ApplyColor(1, value); }
        }
        [UIValue("shade-red")]
        public Color ShadeRed
        {
            get { return Plugin.Settings.MarioColors[2]; }
            set { ApplyColor(2, value); }
        }
        [UIValue("red")]
        public Color Red
        {
            get { return Plugin.Settings.MarioColors[3]; }
            set { ApplyColor(3, value); }
        }
        [UIValue("shade-white")]
        public Color ShadeWhite
        {
            get { return Plugin.Settings.MarioColors[4]; }
            set { ApplyColor(4, value); }
        }
        [UIValue("white")]
        public Color White
        {
            get { return Plugin.Settings.MarioColors[5]; }
            set { ApplyColor(5, value); }
        }
        [UIValue("shade-brown1")]
        public Color ShadeBrown1
        {
            get { return Plugin.Settings.MarioColors[6]; }
            set { ApplyColor(6, value); }
        }
        [UIValue("brown1")]
        public Color Brown1
        {
            get { return Plugin.Settings.MarioColors[7]; }
            set { ApplyColor(7, value); }
        }
        [UIValue("shade-beige")]
        public Color ShadeBeige
        {
            get { return Plugin.Settings.MarioColors[8]; }
            set { ApplyColor(8, value); }
        }
        [UIValue("beige")]
        public Color Beige
        {
            get { return Plugin.Settings.MarioColors[9]; }
            set { ApplyColor(9, value); }
        }
        [UIValue("shade-brown2")]
        public Color ShadeBrown2
        {
            get { return Plugin.Settings.MarioColors[10]; }
            set { ApplyColor(10, value); }
        }
        [UIValue("brown2")]
        public Color Brown2
        {
            get { return Plugin.Settings.MarioColors[11]; }
            set { ApplyColor(11, value); }
        }

        #endregion

        public void Initialize(MenuMarioManager marioManager)
        {
            _marioManager = marioManager;
        }

        [UIAction("#post-parse")]
        internal void PostParse()
        {
            _headerBarImageView.SetField("_skew", 0.0f);
            _closeButtonTransform.Find("BG").GetComponent<ImageView>().SetField("_skew", 0.0f);
            _closeButtonTransform.Find("Underline").GetComponent<ImageView>().SetField("_skew", 0.0f);
            _modal.transform.localPosition = Vector3.up * 30f;
            _modal.blockerClickedEvent += BlockerClickedEventHandler;
            _basicUIAudioManager = Resources.FindObjectsOfTypeAll<BasicUIAudioManager>().First(x => x.GetComponent<AudioSource>().enabled && x.isActiveAndEnabled);
        }

        [UIAction("close-modal")]
        private void HideModalActionHandler()
        {
            HideModal(true);
        }

        [UIAction("reset-colors")]
        private void ResetColors()
        {
            Plugin.Settings.MarioColors = SM64Types.defaultMarioColors.ToList();
            _marioManager.marioColorManager.SetMarioColors(SM64Types.defaultMarioColors);
            _marioManager.marioSpecialEffects.SpawnPopParticles();
        }

        [UIAction("reset-position")]
        private void ResetPosition()
        {
            HideModal(true);
            _marioManager.namePlate.gameObject.SetActive(false);
            _marioManager.marioSpecialEffects.TeleportOut(() =>
            {
                Plugin.Settings.MarioPosition = new Vector3(2f, 0f, 3f);
                _marioManager.marioGO.GetComponent<SM64Mario>().SetPosition(Plugin.Settings.MarioPosition);
                _marioManager.marioSpecialEffects.TeleportIn(() =>
                {
                    _marioManager.namePlate.gameObject.SetActive(true);
                }, 0f);
            }, 0.5f);
        }

        private void ApplyColor(int index, Color32 value)
        {
            Plugin.Settings.MarioColors[index] = value;
            _marioManager.marioColorManager.SetMarioColors(Plugin.Settings.MarioColors.ToArray());
            _marioManager.marioSpecialEffects.SpawnPopParticles();
        }

        private void BlockerClickedEventHandler()
        {
            modalHidden.Invoke();
        }

        public void ShowModal(bool animated)
        {
            _modal.Show(true);
            transform.Find("Blocker").localScale = new Vector3(1.5f, 2.0f, 1.0f);
            _basicUIAudioManager.HandleButtonClickEvent();
        }

        public void HideModal(bool animated)
        {
            _modal.Hide(true);
            modalHidden.Invoke();
        }
    }
}
