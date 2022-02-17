using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SM64BS.Behaviours;
using System.IO;
using UnityEditor.SceneManagement;

namespace SM64BS.TemplateEditor
{
    [CustomEditor(typeof(BundleDescriptor))]
    public class BundleDescriptorEditor : Editor
    {
        private BundleDescriptor _descriptor;

        private void OnEnable()
        {
            _descriptor = (BundleDescriptor)target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            if (EditorGUI.EndChangeCheck())
            {
                _descriptor.bundleId = _descriptor.author + "." + _descriptor.bundleName;
            }

			GUILayout.Space(10);
			if (GUILayout.Button("Export"))
			{
				string path = EditorUtility.SaveFilePanel("Save SM64BS Bundle", "", _descriptor.bundleName + ".sm64bs", "sm64bs");

				if (!string.IsNullOrWhiteSpace(path))
				{
					string fileName = Path.GetFileName(path);
					string folderPath = Path.GetDirectoryName(path);

					Selection.activeObject = _descriptor.gameObject;
					EditorUtility.SetDirty(_descriptor);
					EditorSceneManager.MarkSceneDirty(_descriptor.gameObject.scene);
					EditorSceneManager.SaveScene(_descriptor.gameObject.scene);

					PrefabUtility.CreatePrefab("Assets/_SaberMario64.prefab", _descriptor.gameObject as GameObject);
					AssetBundleBuild assetBundleBuild = default(AssetBundleBuild);
					assetBundleBuild.assetNames = new string[] {
						"Assets/_SaberMario64.prefab"
					};
					assetBundleBuild.assetBundleName = fileName;

					BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
					BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;

					BuildPipeline.BuildAssetBundles(Application.temporaryCachePath, new AssetBundleBuild[] { assetBundleBuild }, BuildAssetBundleOptions.ForceRebuildAssetBundle, buildTarget);
					EditorPrefs.SetString("currentBuildingAssetBundlePath", folderPath);
					EditorUserBuildSettings.SwitchActiveBuildTarget(buildTargetGroup, buildTarget);

					AssetDatabase.DeleteAsset("Assets/_SaberMario64.prefab");

					if (File.Exists(path))
					{
						bool isDirectory = (File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory;
						if (!isDirectory) File.Delete(path);
					}

					File.Move(Path.Combine(Application.temporaryCachePath, fileName), path);
					AssetDatabase.Refresh();
					EditorUtility.DisplayDialog("Export Successful!", "Export Successful!", "OK");
				}
				else
				{
					EditorUtility.DisplayDialog("Export Failed!", "Save path is invalid.", "OK");
				}
			}
		}
    }
}
