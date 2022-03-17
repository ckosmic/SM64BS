using SM64BS.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SM64BS.Behaviours
{
    internal class VRPointerListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private NamePlate _namePlate;
        private SettingsUIManager _settingsUIManager;

        public void Initialize(NamePlate namePlate, SettingsUIManager settingsUIManager)
        {
            _namePlate = namePlate;
            _settingsUIManager = settingsUIManager;

            BoxCollider collider = gameObject.AddComponent<BoxCollider>();
            collider.size = new Vector3(0.5f, 0.6f, 0.5f);
            collider.center = new Vector3(0.0f, 0.1f, 0.0f);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(!_settingsUIManager.modalOpen)
                _namePlate.SetMessageText("CLICK TO OPEN SETTINGS");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _namePlate.SetMessageText("");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _settingsUIManager.ShowModal();
            _namePlate.SetMessageText("");
        }
    }
}
