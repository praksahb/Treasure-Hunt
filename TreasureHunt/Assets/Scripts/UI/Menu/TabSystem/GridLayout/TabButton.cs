using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TreasureHunt.UI
{
    public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
    {
        [SerializeField] private TabGroup tabGroup;

        [SerializeField] private List<Graphic> graphics;


        public Action OnTabSelected;
        public Action OnTabDeselected;

        private Image background;
        public Image Background { get { return background; } }

        private void Awake()
        {
            background = GetComponent<Image>();
        }

        private void Start()
        {
            background = GetComponent<Image>();
            Deselect();
            tabGroup.Subscribe(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            tabGroup.OnTabSelected(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tabGroup.OnTabExit(this);
        }

        public void Select()
        {
            OnTabSelected?.Invoke();

            for (int i = 0; i < graphics.Count; i++)
            {
                graphics[i].color = tabGroup.activeColors[i];
            }
        }

        public void Deselect()
        {
            OnTabDeselected?.Invoke();

            for (int i = 0; i < graphics.Count; i++)
            {
                graphics[i].color = tabGroup.inactiveColors[i];
            }
        }
    }
}
