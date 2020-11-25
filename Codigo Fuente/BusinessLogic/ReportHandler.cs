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
        private IAccommodationHandler accommodationHandler;
        public ReportHandler(IAccommodationHandler accommodationHand, IReservationHandler reservationHand)
        {
            accommodationHandler = accommodationHand;
            reservationHandler = reservationHand;
        }
        public List<ReportItem> accommodationsReport(int spotId, DateTime startingDate, DateTime finishingDate)
        {
            if (startingDate > finishingDate) throw new BadRequestException("The starting date must be before the finishing one");

            var asociatedaccommodations = accommodationHandler.SearchByTouristSpot(spotId,false);
            List<ReportItem> report = CreateReport(startingDate, finishingDate, asociatedaccommodations);

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
                return x.accommodation.Id.CompareTo(y.accommodation.Id);
            }
        }

        private List<ReportItem> CreateReport(DateTime startingDate, DateTime finishingDate, List<Accommodation> asociatedaccommodations)
        {
            List<ReportItem> report = new List<ReportItem>();

            foreach (var actualaccommodation in asociatedaccommodations)
            {
                var reservationsFromaccommodation = reservationHandler.GetAllFromaccommodation(actualaccommodation.Id);
                int reservationsNumber = 0;

                foreach (var actualReservation in reservationsFromaccommodation)
                {
                    if (IncludedReservation(actualReservation, startingDate, finishingDate))
                        reservationsNumber++;
                }

                if (reservationsNumber > 0)
                {
                    report.Add(new ReportItem { accommodation = actualaccommodation, ReservationsQuantity = reservationsNumber });
                }
            }

            return report;
        }

        private bool IncludedReservation(Reservation actualReservation, DateTime startingDate, DateTime finishingDate)
        {
            if (actualReservation.ReservationState == ReservationState.Expired ||
                actualReservation.ReservationState == ReservationState.Rejected)
                return false;
            
            
            if (!IncludedInDates(actualReservation, startingDate, finishingDate))
                return false;

            return true;

        }

        private bool IncludedInDates(Reservation actualReservation, DateTime startingDate, DateTime finishingDate)
        {
            return !(actualReservation.CheckOut.Date < startingDate.Date 
                || actualReservation.CheckIn.Date > finishingDate.Date);
        }
    }
}
