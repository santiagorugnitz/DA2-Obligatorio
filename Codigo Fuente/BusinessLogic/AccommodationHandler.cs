using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogicInterface;
using Exceptions;
using DataImport;

namespace BusinessLogic
{
    public class AccommodationHandler : IAccommodationHandler
    {
        private static readonly double CHILDREN_FEE = 0.5;
        private static readonly double BABY_FEE = 0.25;
        private static readonly double RETIRED_FEE = 1.7;

        private ITouristSpotHandler touristSpotHandler;
        private IRepository<Accommodation> accommodationRepository;

        public AccommodationHandler(IRepository<Accommodation> accommodationRepo,
            ITouristSpotHandler touristSpotHand)
        {
            accommodationRepository = accommodationRepo;
            touristSpotHandler = touristSpotHand;
        }

        public Accommodation Add(Accommodation accommodation, int touristSpotId, List<string> imageNames)
        {
            List<Image> accommodationImages = new List<Image>();

            if (imageNames == null)
            {
                throw new BadRequestException("The accommodation needs at least one image");
            }

            foreach (var imageName in imageNames)
            {
                Image image = new Image { Name = imageName };
                accommodationImages.Add(image);
            }

            var gotTouristSpot = touristSpotHandler.Get(touristSpotId);

            if (gotTouristSpot == null)
            {
                throw new BadRequestException("The tourist spot does not exist");
            }

            accommodation.Images = accommodationImages;
            accommodation.TouristSpot = gotTouristSpot;

            return accommodationRepository.Add(accommodation);
        }

        public bool Delete(int id)
        {
            var accommodation = Get(id);
            if (accommodation == null) throw new NotFoundException("The accommodation does not exist");

            return accommodationRepository.Delete(accommodation);
        }
        public bool Exists(Accommodation accommodation)
        {
            return accommodationRepository.Get(accommodation.Id) != null;
        }

        public bool ChangeAvailability(int id, bool availability)
        {
            var accommodation = Get(id);
            if (accommodation == null) throw new NotFoundException("The accommodation does not exist");

            accommodation.Available = availability;
            return accommodationRepository.Update(accommodation);
        }

        public List<Accommodation> SearchByTouristSpot(int spotId,bool onlyAvailable=true)
        {
            if (spotId == 0)
            {
                if(onlyAvailable)
                    return accommodationRepository.GetAll(x =>((Accommodation)x).Available).ToList();

                return accommodationRepository.GetAll().ToList();
            }

            if (touristSpotHandler.Get(spotId) == null)
            {
                throw new BadRequestException("The spot does not exist");
            }

            if(onlyAvailable)
            return accommodationRepository.GetAll(x => ((Accommodation)x).TouristSpot.Id == spotId && ((Accommodation)x).Available).ToList();

            return accommodationRepository.GetAll(x => ((Accommodation)x).TouristSpot.Id == spotId).ToList();


        }

        public Accommodation Get(int accommodationId)
        {
            return accommodationRepository.Get(accommodationId);
        }

        public bool Add(List<AccommodationImport> accommodations)
        {
            var ret = true;
            foreach (var accommodation in accommodations)
            {
                var spotImport = accommodation.TouristSpot;

                var spot = touristSpotHandler.Get(spotImport.Name);

                if (spot == null)
                {
                    spot = touristSpotHandler.Add(spotImport.ToEntity(), spotImport.RegionId, spotImport.Categories.ToList(), spotImport.Image);
                }
                try
                {
                    if (spot == null) throw new BadRequestException();
                    Add(accommodation.ToEntity(), spot.Id, accommodation.Images);
                }
                catch (BadRequestException)
                {
                    ret = false;
                }
            }

            return ret;
        }

        public double CalculateTotal(int id, Stay stay)
        {
            var accommodation = Get(id);
            if(accommodation==null) throw new NotFoundException("The accommodation does not exist");

            double ret = 0;
            int days = (stay.CheckOut - stay.CheckIn).Days;
            ret += stay.AdultQuantity + stay.ChildrenQuantity * CHILDREN_FEE + stay.BabyQuantity * BABY_FEE;
            ret += ((int)stay.RetiredQuantity / 2) * RETIRED_FEE;

            ret += stay.RetiredQuantity % 2;
            ret *= days * accommodation.Fee;
            return ret;
        }




    }
}
