using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Diagnostics;
using LibSM64;
using SM64BS.Utils;
#if MOD
using SM64BS.Managers;
#elif PLUGIN
using System.Reflection;
#endif

namespace SM64BS.Behaviours
{
    [System.Serializable]
    public enum InputProviderType
    { 
        VRControls,
        RandomControls
    }

#if MOD // The Beat Saber mod's version of this class
    internal class ActionManager : MonoBehaviour
    {
        private AppMarioManager _appMarioManager;

        private List<GameObject> _marios = new List<GameObject>();
        private SM64Mario _lastMario;

        public void Initialize(AppMarioManager appMarioManager)
        {
            _appMarioManager = appMarioManager;
            _marios = new List<GameObject>();
        }

        public void SpawnMario(Transform spawnPoint)
        {
            if (_marios.Count < Plugin.Settings.MaxMarios)
            {
                GameObject marioGO = _appMarioManager.SpawnMario(spawnPoint.position, spawnPoint.rotation, false);
                _marios.Add(marioGO);
                _appMarioManager.InitializeMario(marioGO);
                _lastMario = marioGO.GetComponent<SM64Mario>();
            }
        }

        public void AttachInputProvider(int type)
        {
            switch ((InputProviderType)type)
            {
                case InputProviderType.VRControls:
                    _lastMario.gameObject.AddComponent<InputProvider>();
                    break;
                case InputProviderType.RandomControls:
                    _lastMario.gameObject.AddComponent<RandomInputProvider>();
                    break;
            }
            _lastMario.RefreshInputProvider();
        }

        public void SetMarioAction(string actionName)
        {
            _lastMario.SetAction((SM64MarioAction)Enum.Parse(typeof(SM64MarioAction), actionName));
        }

        public void DebugLog(string msg)
        { 
            Plugin.Log.Info(msg);
        }
    }
#elif PLUGIN // The Unity plugin's version of this class
    public class ActionManager : MonoBehaviour
    {
        private static ActionManager _instance;

        private List<GameObject> _marios = new List<GameObject>();
        private SM64Mario _lastMario;
        private Shader _marioShader;
        private Material _marioMaterial;

        private void Awake()
        {
            if (_instance != null)
            { 
                DestroyImmediate(_instance);
            }
            _instance = this;

            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "baserom.us.z64")))
            {
                new GameObject("SM64Context").AddComponent<SM64Context>();
            }
            else
            { 
                Debug.LogError("Could not find US SM64 ROM at: \"" + Path.Combine(Environment.CurrentDirectory, "baserom.us.z64") + "\"");
                Destroy(this);
            }
            _marios = new List<GameObject>();
        }

        private void Start()
        {
            SM64Context.Initialize(Path.Combine(Environment.CurrentDirectory, "baserom.us.z64"));
            SM64Context.SetScaleFactor(2.0f);
        }

        public void SpawnMario(Transform spawnPoint)
        {
            if (_marios.Count < 5)
            {
                GameObject marioGO = SpawnMarioInEditor(spawnPoint.position, spawnPoint.rotation, false);
                _marios.Add(marioGO);
                InitializeMarioInEditor(marioGO);
                _lastMario = marioGO.GetComponent<SM64Mario>();
            }
        }

        [EnumAction(typeof(InputProviderType))]
        public void AttachInputProvider(int type)
        {
            switch ((InputProviderType)type)
            {
                case InputProviderType.VRControls:
                    _lastMario.gameObject.AddComponent<InputProvider>();
                    break;
                case InputProviderType.RandomControls:
                    _lastMario.gameObject.AddComponent<RandomInputProvider>();
                    break;
            }
            _lastMario.RefreshInputProvider();
        }

        // SM64MarioAction values are too high for an int enum, but this is cleaner in the editor anyway
        public void SetMarioAction(string actionName)
        {
            _lastMario.SetAction((SM64MarioAction)Enum.Parse(typeof(SM64MarioAction), actionName));
        }

        public void DebugLog(string msg)
        { 
            Debug.Log(msg);
        }

        private GameObject SpawnMarioInEditor(Vector3 position, Quaternion rotation, bool initialize = true)
        {
            GameObject marioGO = new GameObject("Mario");

            Transform marioTransform = marioGO.transform;
            marioTransform.position = position;
            marioTransform.rotation = rotation;

            SM64Mario sm64Mario = marioGO.AddComponent<SM64Mario>();

            if (_marioShader == null)
{
                _marioShader = Shader.Find("BeatSaber/SM64BS");
            }

            if (_marioMaterial == null)
            {
                _marioMaterial = new Material(_marioShader);
                _marioMaterial.SetFloat("_Ambient", 0.25f);
            }

            FieldInfo fields = sm64Mario.GetType().GetField("material", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            fields.SetValue(sm64Mario, _marioMaterial);

            if (initialize) sm64Mario.Initialize();

            return marioGO;
        }

        private void InitializeMarioInEditor(GameObject marioGO)
        {
            marioGO.GetComponent<SM64Mario>().Initialize();
        }
    }
#endif
}
