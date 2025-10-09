// Services/EventAggregator.cs
using System;

namespace Master_Floor_Project.Services
{
    public static class EventAggregator
    {
        public static event Action? PartnersChanged;

        public static void PublishPartnersChanged()
        {
            PartnersChanged?.Invoke();
        }
    }
}