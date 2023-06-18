using System;
using System.Collections.Generic;
using Controllers;
using Interfaces;
using Service.Events;
using UnityEngine;

namespace Panels
{
    public abstract class Panel : MonoBehaviour
    {
        protected EventArgs eventArg;

        protected void Start()
        {
            if (eventArg == null) Debug.LogWarning($"no event args applied to {this.GetType()} - WILL NOT SHOW, ADD EVENT TO eventArg");
            
            UIController.Instance.Register(eventArg.GetType(), this);
        }
    }
}