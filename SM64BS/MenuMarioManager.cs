using Zenject;
using UnityEngine;
using LibSM64;
using System;
using SM64BS.Behaviours;
using SM64BS.Utils;
using SM64BS.UI;
using BeatSaberMarkupLanguage.FloatingScreen;

namespace SM64BS
{
    internal class MenuMarioManager : IInitializable, IDisposable
    {
        private readonly MarioSpawner _marioSpawner;
        private readonly ResourceUtilities _utils;

        internal GameObject marioGO;
        internal NamePlate namePlate;
        internal VRPointerListener vrPointerListener;
        internal SettingsUIManager settingsUIManager;
        internal MarioColorManager marioColorManager;
        internal MarioSpecialEffects marioSpecialEffects;

        public MenuMarioManager(MarioSpawner marioSpawner, ResourceUtilities utils) {
            _marioSpawner = marioSpawner;
            _utils = utils;
        }

        public void Initialize() {
            SM64Context.Terminate();
            SM64Context.Initialize(Application.dataPath + "/../baserom.us.z64");
            

            GameObject groundGO = GameObject.CreatePrimitive(PrimitiveType.Plane);
            groundGO.AddComponent<SM64StaticTerrain>();
            groundGO.GetComponent<MeshRenderer>().enabled = false;
            groundGO.transform.localScale = Vector3.one * 5f;

            AddSM64Collision(GameObject.Find("MenuEnvironmentManager/DefaultMenuEnvironment/Notes"));
            AddSM64Collision(GameObject.Find("MenuEnvironmentManager/DefaultMenuEnvironment/PileOfNotes"));
            SM64Context.SetScaleFactor(2.0f);

            Vector3 spawnPos = Plugin.Settings.MarioPosition;

            marioGO = _marioSpawner.SpawnMario(spawnPos, Quaternion.LookRotation(new Vector3(0, spawnPos.y, 0) - spawnPos));
            marioGO.AddComponent<MarioBehaviour>();
            marioColorManager = marioGO.AddComponent<MarioColorManager>();
            settingsUIManager = marioGO.AddComponent<SettingsUIManager>();
            marioSpecialEffects = marioGO.AddComponent<MarioSpecialEffects>();

            if (Plugin.Settings.ShowNamePlate)
            {
                namePlate = new GameObject("NamePlate").AddComponent<NamePlate>();
                namePlate.transform.SetParent(marioGO.transform);
                namePlate.transform.localPosition = new Vector3(0f, 0.9f, 0f);
                namePlate.Initialize(_utils);
            }

            vrPointerListener = new GameObject("VRPointerListener").AddComponent<VRPointerListener>();
            vrPointerListener.Initialize(namePlate, settingsUIManager);
            vrPointerListener.transform.SetParent(marioGO.transform);
            vrPointerListener.transform.localPosition = new Vector3(0f, 0.45f, 0f);

            settingsUIManager.Initialize(this);
            settingsUIManager.CreateFloatingScreen();

            marioSpecialEffects.Initialize(_utils);
        }

        public void Dispose()
        {
            
        }

        private void AddSM64Collision(GameObject root)
        {
            foreach (BoxCollider bc in root.GetComponentsInChildren<BoxCollider>())
            {
                if (bc.transform.parent.GetComponent<Animation>())
                {
                    bc.gameObject.AddComponent<SM64DynamicTerrain>();
                }
                else
                {
                    bc.gameObject.AddComponent<SM64StaticTerrain>();
                }
            }
        }
    }
}
