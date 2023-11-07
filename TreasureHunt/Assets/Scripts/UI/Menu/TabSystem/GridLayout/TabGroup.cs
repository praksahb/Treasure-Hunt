using System.Collections.Generic;
using UnityEngine;

namespace TreasureHunt
{
    public class TabGroup : MonoBehaviour
    {
        public List<TabButton> tabButtons;
        public List<GameObject> objectsToSwap;

        public List<Color> activeColors;
        public List<Color> inactiveColors;

        //public Color tabIdle;
        //public Color tabHover;
        //public Color tabSelected;

        public TabButton selectedTab;
        private int currentIdx;

        public void Subscribe(TabButton button)
        {
            tabButtons ??= new List<TabButton>();
            tabButtons.Add(button);

            if (tabButtons.Count == 1)
            {
                OnTabSelected(button);
                currentIdx = 0;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // move selectedtab left
                currentIdx = (currentIdx + 1) % tabButtons.Count;
                selectedTab = tabButtons[currentIdx];
                ResetTabs();

            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                // move selectedTab right
                currentIdx = (currentIdx - 1 + tabButtons.Count) % tabButtons.Count;
                selectedTab = tabButtons[currentIdx];
                ResetTabs();
            }
        }

        public void OnTabExit(TabButton button)
        {
            //ResetTabs();
        }

        public void OnTabSelected(TabButton button)
        {
            if (selectedTab != null)
            {
                selectedTab.Deselect();
            }

            selectedTab = button;
            selectedTab.Select();


            // sets the current tab body active
            int index = button.transform.GetSiblingIndex();
            for (int i = 0; i < objectsToSwap.Count; i++)
            {
                if (i == index)
                {
                    objectsToSwap[i].SetActive(true);
                }
                else
                {
                    objectsToSwap[i].SetActive(false);
                }
            }
        }

        public void ResetTabs()
        {
            foreach (TabButton button in tabButtons)
            {
                if (selectedTab != null && button == selectedTab)
                {
                    button.Select();
                }
                else
                {
                    button.Deselect();
                }
            }
        }
    }
}
