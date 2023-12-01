using System;
using System.Collections.Generic;
using Panels;
using Service.Events;
using UnityEngine;

namespace Controllers
{
    public class UIController : Controller
    {
        public static UIController Instance { get; private set; }
        protected Dictionary<System.Type, List<GameObject>> typePanelDictionary;
        private int panelCount;
        protected EventArgs initialEvent;
        protected bool newAdd;
        private bool panelFlag = true;

        private void Awake()
        {
            Instance = this;
            typePanelDictionary = new Dictionary<Type, List<GameObject>>();
            Panel[] panels = FindObjectsOfType<Panel>();
            panelCount = panels.Length;
        }

        public void Register(Type type, Panel panel)
        {
            if (!typePanelDictionary.TryGetValue(type, out List<GameObject> panelList))
            {
                typePanelDictionary.Add(type, new List<GameObject>(){panel.gameObject});
                return;
            }
            panelList.Add(panel.gameObject);
        }

        protected void Respond(Type type)
        {
            foreach (var typeKey in typePanelDictionary.Keys)
            {
                if (type == typeKey)
                {
                    foreach (var go in typePanelDictionary[typeKey])
                    {
                        go.SetActive(true);
                    }
                    continue;
                }
                foreach (var go in typePanelDictionary[typeKey])
                {
                    go.SetActive(false);
                }
            }
            
            if (initialEvent != null)
            {
                newAdd = true;
            }
        }
        
        private void LateUpdate()
        {
            if (newAdd)
            {
                Respond(initialEvent.GetType());
                newAdd = false;
            }

            if (panelCount == typePanelDictionary.Count && panelFlag)
            {
                initialEvent = null;
                panelFlag = false;
            }
        }
        
        public override void Consume(GameServiceStart gameServiceStart)
        {
            initialEvent = gameServiceStart;
            Respond(gameServiceStart.GetType());
        } 
        
        public override void Consume(GameServiceEnd gameServiceEnd)
        {
            Respond(gameServiceEnd.GetType());
        }
    }
}