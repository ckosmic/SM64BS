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
    internal class SpawnMarioOnMiss : SM64BSPlugin, IBeatmapEventHandler
    {

        public override void PluginInitialize()
        {
            SM64Context.SetScaleFactor(1.25f);

            GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.AddComponent<SM64StaticTerrain>();
            ground.GetComponent<MeshRenderer>().enabled = false;
            ground.GetComponent<MeshCollider>().enabled = false;
        }

        public void NoteWasMissed(NoteData noteData)
        {
            if (noteData.colorType != ColorType.None && (noteData.gameplayType == NoteData.GameplayType.BurstSliderHead || noteData.gameplayType == NoteData.GameplayType.Normal))
            {
                SM64Mario sm64Mario = GameScene.SpawnMario(GameScene.CameraTransform.position + GameScene.CameraTransform.forward, GameScene.CameraTransform.rotation);
                sm64Mario?.gameObject.AddComponent<RandomInputProvider>();
                sm64Mario?.RefreshInputProvider();
            }
        }

        public override void PluginDispose()
        {

        }

        public void BeatmapEventTriggered(BeatmapEventData songEvent)
        {
            
        }

        public void NoteWasSpawned(NoteData noteData)
        {
            
        }

        public void NoteWasDespawned(NoteData noteData)
        {
            
        }

        public void NoteWasCut(NoteData noteData, NoteCutInfo noteCutInfo)
        {
            
        }

        public void NoteDidStartJump(NoteData noteData)
        {
            
        }

        public void ObstacleWasSpawned(ObstacleData obstacleData)
        {
            
        }

        public void ObstacleWasDespawned(ObstacleData obstacleData)
        {
            
        }

        public void ObstacleDidPassAvoidedMark(ObstacleData obstacleData)
        {
            
        }
    }
}
