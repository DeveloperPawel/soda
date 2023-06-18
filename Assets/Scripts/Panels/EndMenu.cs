using System;
using Service.Events;

namespace Panels
{
    public class EndMenu : Panel
    {
        private void Awake()
        {
            eventArg = new GameServiceEnd();
        }
    }
}