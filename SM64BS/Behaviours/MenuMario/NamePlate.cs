using SM64BS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SM64BS.Behaviours
{
    internal class NamePlate : MonoBehaviour
    {
        private Material _textMaterial;
        private Material _plateMaterial;
        private Sprite _plateSprite;
        private Image _plateImage;
        private Canvas _canvas;

        private TextMeshPro _nameTMPro;
        private TextMeshPro _messageTMPro;

        public void Initialize(ResourceUtilities utils)
        {
            if (_nameTMPro == null)
            {
                _nameTMPro = new GameObject("NameText").AddComponent<TextMeshPro>();
                Transform tmproTransform = _nameTMPro.transform;
                tmproTransform.SetParent(transform);
                tmproTransform.localPosition = Vector3.zero;
                tmproTransform.localRotation = Quaternion.identity;
            }
            if (_messageTMPro == null)
            {
                _messageTMPro = new GameObject("MessageText").AddComponent<TextMeshPro>();
                Transform tmproTransform = _messageTMPro.transform;
                tmproTransform.SetParent(transform);
                tmproTransform.localPosition = Vector3.up * 0.25f;
                tmproTransform.localRotation = Quaternion.identity;
            }
            if (_textMaterial == null)
            {
                Shader shader = utils.LoadAssetFromMainBundle<Shader>("TMP_SDF-Billboard.shader");
                _textMaterial = new Material(shader);
            }
            if (_plateMaterial == null)
            {
                Shader shader = utils.LoadAssetFromMainBundle<Shader>("sh_ui_billboard.shader");
                _plateMaterial = new Material(shader);
                _plateMaterial.SetFloat("_Skew", 0.18f);
                _plateMaterial.SetColor("_Color", new Color(0.0f, 0.0f, 0.0f, 0.35f));
            }
            if (_plateSprite == null)
                _plateSprite = Resources.FindObjectsOfTypeAll<Sprite>().First(x => x.name == "RoundRect10");

            TMP_FontAsset tekoFontAsset = Resources.FindObjectsOfTypeAll<TMP_FontAsset>().Last(f => f.name == "Teko-Medium SDF");
            Material tekoMaterial = tekoFontAsset.material;
            _textMaterial.mainTexture = tekoMaterial.mainTexture;
            _textMaterial.mainTextureOffset = tekoMaterial.mainTextureOffset;
            _textMaterial.mainTextureScale = tekoMaterial.mainTextureScale;

            _nameTMPro.material = _textMaterial;
            _nameTMPro.GetComponent<MeshRenderer>().material = _textMaterial;
            _messageTMPro.material = _textMaterial;
            _messageTMPro.GetComponent<MeshRenderer>().material = _textMaterial;

            _nameTMPro.text = Plugin.Settings.MarioName;
            _nameTMPro.fontSize = 2;
            _nameTMPro.alignment = TextAlignmentOptions.Center;
            _nameTMPro.fontStyle = FontStyles.Italic;
            _nameTMPro.ForceMeshUpdate();

            _messageTMPro.text = "";
            _messageTMPro.fontSize = 1.25f;
            _messageTMPro.alignment = TextAlignmentOptions.Bottom;
            _messageTMPro.fontStyle = FontStyles.Italic;
            _messageTMPro.ForceMeshUpdate();

            float textWidth = _nameTMPro.textBounds.size.x;

            _nameTMPro.GetComponent<RectTransform>().sizeDelta = new Vector2(2.0f, 0.25f);
            _messageTMPro.GetComponent<RectTransform>().sizeDelta = new Vector2(2.0f, 0.25f);

            _canvas = new GameObject("Canvas").AddComponent<Canvas>();
            _canvas.renderMode = RenderMode.WorldSpace;
            _canvas.transform.SetParent(_nameTMPro.transform);
            _canvas.transform.localPosition = Vector3.up * 0.01f;
            _canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(textWidth + 0.2f, 0.25f);
            _canvas.gameObject.AddComponent<CanvasScaler>().referencePixelsPerUnit = 0.25f;

            _plateImage = new GameObject("Background").AddComponent<Image>();
            _plateImage.type = Image.Type.Sliced;
            _plateImage.sprite = _plateSprite;
            _plateImage.material = _plateMaterial;
            _plateImage.transform.SetParent(_canvas.transform);
            _plateImage.transform.localPosition = Vector3.zero;
            _plateImage.GetComponent<RectTransform>().sizeDelta = new Vector2(textWidth + 0.2f, 0.25f);
        }

        public void SetMessageText(string message)
        {
            _messageTMPro.text = message;
        }

        public void SetNamePlateText(string name)
        {
            _nameTMPro.text = name;
            float textWidth = _nameTMPro.textBounds.size.x;
            _canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(textWidth + 0.2f, 0.25f);
            _plateImage.GetComponent<RectTransform>().sizeDelta = new Vector2(textWidth + 0.2f, 0.25f);
        }
    }
}
