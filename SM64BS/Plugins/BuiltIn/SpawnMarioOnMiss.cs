using BeatSaberMarkupLanguage;
using LibSM64;
using SM64BS.Attributes;
using SM64BS.Behaviours;
using SM64BS.Plugins.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;
namespace SM64BS.Plugins.BuiltIn
{
    [PluginMetadata(Name = "Spawn Mario On Miss", Author = "ckosmic", Description = "Spawns a Mario at your head when you miss a note.", PluginId = "com.christiankosman.spawnmarioonmiss")]
    internal class SpawnMarioOnMiss : SM64BSPlugin, IScoreEventHandler
    {

        public override void PluginInitialize()
        {
            SM64Context.SetScaleFactor(0.5f);

            GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.AddComponent<SM64StaticTerrain>();
            ground.GetComponent<MeshRenderer>().enabled = false;
        }

        public override void PluginDispose()
        {

        }

        public void MultiplierDidChange(int multiplier, float progress)
        {
            
        }

        public void ScoreDidChange(int score, int scoreAfterModifier)
        {
            
        }

        public void ComboDidChange(int combo)
        {
            
        }

        public void NoteWasCut(NoteData noteData, in NoteCutInfo noteCutInfo, int multiplier)
        {
            
        }

        public void NoteWasMissed(NoteData noteData, int multiplier)
        {
            SM64Mario sm64Mario = GameScene.SpawnMario(GameScene.cameraTransform.position + GameScene.cameraTransform.forward, GameScene.cameraTransform.rotation);
            sm64Mario?.gameObject.AddComponent<RandomInputProvider>();
            sm64Mario?.RefreshInputProvider();
        }
    }
}
