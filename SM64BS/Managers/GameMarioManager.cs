using Zenject;
using UnityEngine;
using LibSM64;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using SM64BS.Behaviours;
using static BeatmapSaveData;
using SM64BS.Utils;
using IPA.Utilities;

namespace SM64BS.Managers
{
    internal class GameMarioManager : IInitializable, IDisposable, ILateDisposable
    {
        private readonly BundleLoader _bundleLoader;
        private readonly AppMarioManager _appMarioManager;
        private readonly SaberManager _saberManager;

        private List<GameObject> _marios;
        private GameObject _bundleGO;
        private EventObjects _eventObjects;

        public GameMarioManager(BundleLoader bundleLoader, AppMarioManager appMarioManager, SaberManager saberManager, ScoreController scoreController, GameEnergyCounter gameEnergyCounter)
{
            _bundleLoader = bundleLoader;
            _appMarioManager = appMarioManager;
            _saberManager = saberManager;

            _eventObjects.scoreController = scoreController;
            _eventObjects.gameEnergyCounter = gameEnergyCounter;
        }

        public void Initialize()
        {

            if (Plugin.Settings.SelectedBundle != "default")
            {
                _bundleLoader.Reload();
                MarioBundle marioBundle = _bundleLoader.GetBundleById(Plugin.Settings.SelectedBundle);
                InstantiateMarioBundle(marioBundle);
            }

            _appMarioManager.CreateMenuBufferPlatform();
            _appMarioManager.SetMenuTerrainsEnabled(false);

            RemoveColliders(_bundleGO);

            _marios = new List<GameObject>();
        }

        public void Dispose()
        {
            foreach (GameObject marioGO in _marios)
            {
                UnityEngine.Object.DestroyImmediate(marioGO);
            }
        }

        public void LateDispose()
        {
            UnityEngine.Object.DestroyImmediate(_bundleGO);
            _appMarioManager.SetMenuTerrainsEnabled(true);
        }

        private void InstantiateMarioBundle(MarioBundle marioBundle)
        {
            GameObject prefab = marioBundle.rootPrefab;
            _bundleGO = UnityEngine.Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            SanitizeBundle(_bundleGO);
            ParentSpecialTransforms(_bundleGO);
            AddManagers(_bundleGO);
        }

        private void AddManagers(GameObject go)
        {
            go.GetComponentInChildren<ActionManager>().Initialize(_appMarioManager);
            if (go.GetComponentInChildren<EventManager>() != null)
            {
                foreach (EventManager em in go.GetComponentsInChildren<EventManager>(true))
                {
                    GameEventManager gem = em.gameObject.AddComponent<GameEventManager>();
                    gem.Initialize(em, _eventObjects);
                }
            }
        }

        private void SanitizeBundle(GameObject go)
        {
            foreach (Camera component in go.GetComponentsInChildren<Camera>(true))
            {
                UnityEngine.Object.DestroyImmediate(component);
            }
            foreach (Light component in go.GetComponentsInChildren<Light>(true))
            {
                UnityEngine.Object.DestroyImmediate(component);
            }
        }

        private void ParentSpecialTransforms(GameObject go)
        {
            Transform transformsTransform = go.transform.Find("Transforms");
            transformsTransform.Find("Camera").SetParent(Camera.main.transform);
            transformsTransform.Find("SaberR").SetParent(_saberManager.rightSaber.GetField<Transform, Saber>("_handleTransform"));
            transformsTransform.Find("SaberL").SetParent(_saberManager.leftSaber.GetField<Transform, Saber>("_handleTransform"));
        }

        private void RemoveColliders(GameObject go)
        {
            foreach (Collider component in go.GetComponentsInChildren<Collider>(true))
            {
                UnityEngine.Object.DestroyImmediate(component);
            }
        }
    }
}
