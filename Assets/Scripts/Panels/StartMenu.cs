using System;
using Service.Events;

namespace Panels
{
    public class StartMenu : Panel
    {
        private void Awake()
        {
            eventArg = new GameServiceStart();
        }
    }
}