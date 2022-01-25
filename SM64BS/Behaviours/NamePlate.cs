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

        private TextMeshPro _tmpro;

        public void Initialize(ResourceUtilities utils)
        {
            if (_tmpro == null)
                _tmpro = gameObject.AddComponent<TextMeshPro>();
            if (_textMaterial == null)
            {
                Shader shader = utils.mainBundle.LoadAsset<Shader>("Assets/SM64BS/TMP_SDF-Billboard.shader");
                _textMaterial = new Material(shader);
            }
            if (_plateMaterial == null)
            {
                Shader shader = utils.mainBundle.LoadAsset<Shader>("Assets/SM64BS/sh_ui_billboard.shader");
                _plateMaterial = new Material(shader);
                _plateMaterial.SetFloat("_Skew", 0.18f);
                _plateMaterial.SetColor("_Color", new Color(0.25f, 0.25f, 0.25f, 0.25f));
            }
            if (_plateSprite == null)
                _plateSprite = Resources.FindObjectsOfTypeAll<Sprite>().First(x => x.name == "RoundRect10");

            TMP_FontAsset tekoFontAsset = Resources.FindObjectsOfTypeAll<TMP_FontAsset>().Last(f => f.name == "Teko-Medium SDF");
            Material tekoMaterial = tekoFontAsset.material;
            _textMaterial.mainTexture = tekoMaterial.mainTexture;
            _textMaterial.mainTextureOffset = tekoMaterial.mainTextureOffset;
            _textMaterial.mainTextureScale = tekoMaterial.mainTextureScale;
            _tmpro.material = _textMaterial;
            GetComponent<MeshRenderer>().material = _textMaterial;

            _tmpro.text = Plugin.Settings.MarioName;
            _tmpro.fontSize = 2;
            _tmpro.alignment = TextAlignmentOptions.Center;
            _tmpro.fontStyle = FontStyles.Italic;
            _tmpro.ForceMeshUpdate();

            float textWidth = _tmpro.textBounds.size.x;

            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(2.0f, 0.25f);

            Canvas canvas = new GameObject("Canvas").AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.transform.SetParent(transform);
            canvas.transform.localPosition = Vector3.up * 0.01f;
            canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(textWidth + 0.2f, 0.25f);
            canvas.gameObject.AddComponent<CanvasScaler>().referencePixelsPerUnit = 0.5f;

            Image plateImage = new GameObject("Background").AddComponent<Image>();
            plateImage.type = Image.Type.Sliced;
            plateImage.sprite = _plateSprite;
            plateImage.material = _plateMaterial;
            plateImage.transform.SetParent(canvas.transform);
            plateImage.transform.localPosition = Vector3.zero;
            plateImage.GetComponent<RectTransform>().sizeDelta = new Vector2(textWidth + 0.2f, 0.25f);
        }
    }
}
