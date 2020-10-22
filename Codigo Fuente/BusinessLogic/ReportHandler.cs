using BusinessLogicInterface;
using Domain;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class ReportHandler : IReportHandler
    {
        private IReservationHandler reservationHandler;
        private IAccomodationHandler accomodationHandler;
        public ReportHandler(IAccomodationHandler accomodationHand, IReservationHandler reservationHand)
        {
            accomodationHandler = accomodationHand;
            reservationHandler = reservationHand;
        }
        public List<ReportItem> AccomodationsReport(int spotId, DateTime startingDate, DateTime finishingDate)
        {
            if (startingDate > finishingDate) throw new BadRequestException("The starting date must be before the finishing one");

            var asociatedAccomodations = accomodationHandler.SearchByTouristSpot(spotId);
            List<ReportItem> report = new List<ReportItem>();

            foreach (var actualAccomodation in asociatedAccomodations)
            {
                var reservationsFromAccomodation = reservationHandler.GetAllFromAccomodation(actualAccomodation.Id);
                int reservationsNumber = 0;

                foreach (var actualReservation in reservationsFromAccomodation)
                {
                    if (IncludedReservation(actualReservation, startingDate, finishingDate))
                        reservationsNumber++;
                }

                if (reservationsNumber > 0)
                {
                    report.Add(new ReportItem { Accomodation = actualAccomodation, ReservationsQuantity = reservationsNumber });
                }
            }

            report.Sort((x, y) => Compare(x, y));
            return report;
        }

        private int Compare(ReportItem x, ReportItem y)
        {
            int compareByReservations = x.ReservationsQuantity.CompareTo(y.ReservationsQuantity);

            if (compareByReservations != 0)
            {
                return -compareByReservations;
            }
            else
            {
                return x.Accomodation.Id.CompareTo(y.Accomodation.Id);
            }
            
        }

        private bool IncludedReservation(Reservation actualReservation, DateTime startingDate, DateTime finishingDate)
        {
            if (actualReservation.ReservationState == ReservationState.Expirada ||
                actualReservation.ReservationState == ReservationState.Rechazada)
                return false;
            
            
            if (!IncludedInDates(actualReservation, startingDate, finishingDate))
                return false;

            return true;

        }

        private bool IncludedInDates(Reservation actualReservation, DateTime startingDate, DateTime finishingDate)
        {
            return !(actualReservation.CheckOut<startingDate || actualReservation.CheckIn>finishingDate);
        }
    }
}
