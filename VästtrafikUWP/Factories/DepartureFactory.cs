using System;

using VästtrafikUWP.Models.Departure;

namespace VästtrafikUWP.Factories
{
    static class DepartureFactory
    {
        public static DepartureViewModel DepartureToDepartureViewModel(Departure departure, int counter)
        {
            if (departure != null)
            {
                string date = departure.Date;
                string realdate = departure.RealDate;
                string time = departure.Time;
                string realtime = departure.RealTime;

                var departureTime = DateTime.Parse((realdate != null ? realdate : date) + " " + (realtime != null ? realtime : time) + ":00");

                TimeSpan diff = departureTime - DateTime.Now;

                string minutesLeft = Math.Round(diff.TotalMinutes, 0).ToString();

                return new DepartureViewModel
                {
                    Departure = departure,
                    MinutesLeft = (minutesLeft == "0" ? "Nu" : minutesLeft),
                    Background = (counter % 2 == 0 ? "Darkgray" : "Black")
                };
            }

            throw new ArgumentException("Departure is null");
        }
    }
}
