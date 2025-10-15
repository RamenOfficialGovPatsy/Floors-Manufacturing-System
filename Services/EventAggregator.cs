using System;

namespace Master_Floor_Project.Services
{
    public static class EventAggregator
    {
        // Событие изменения данных партнеров
        public static event Action? PartnersChanged;

        // Метод для уведомления подписчиков об изменении партнеров
        public static void PublishPartnersChanged()
        {
            PartnersChanged?.Invoke(); // Вызов события если есть подписчики
        }
    }
}