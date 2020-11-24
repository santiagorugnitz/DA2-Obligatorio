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
    public class AccomodationHandler : IAccomodationHandler
    {
        private ITouristSpotHandler touristSpotHandler;
        private IRepository<Accomodation> accomodationRepository;

        public AccomodationHandler(IRepository<Accomodation> accomodationRepo,
            ITouristSpotHandler touristSpotHand)
        {
            accomodationRepository = accomodationRepo;
            touristSpotHandler = touristSpotHand;
        }

        public Accomodation Add(Accomodation accomodation, int touristSpotId, List<string> imageNames)
        {
            List<Image> accomodationImages = new List<Image>();

            if (imageNames == null)
            {
                throw new BadRequestException("The accommodation needs at least one image");
            }

            foreach (var imageName in imageNames)
            {
                Image image = new Image { Name = imageName };
                accomodationImages.Add(image);
            }

            var gotTouristSpot = touristSpotHandler.Get(touristSpotId);

            if (gotTouristSpot == null)
            {
                throw new BadRequestException("The tourist spot does not exist");
            }

            accomodation.Images = accomodationImages;
            accomodation.TouristSpot = gotTouristSpot;

            return accomodationRepository.Add(accomodation);
        }

        public bool Delete(int id)
        {
            var accomodation = Get(id);
            if (accomodation == null) throw new NotFoundException("The accomodation does not exist");

            return accomodationRepository.Delete(accomodation);
        }
        public bool Exists(Accomodation accomodation)
        {
            return accomodationRepository.Get(accomodation.Id) != null;
        }

        public bool ChangeAvailability(int id, bool availability)
        {
            var accomodation = Get(id);
            if (accomodation == null) throw new NotFoundException("The accomodation does not exist");

            accomodation.Available = availability;
            return accomodationRepository.Update(accomodation);
        }

        public List<Accomodation> SearchByTouristSpot(int spotId,bool onlyAvailable=true)
        {
            if (spotId == 0)
            {
                if(onlyAvailable)
                    return accomodationRepository.GetAll(x =>((Accomodation)x).Available).ToList();

                return accomodationRepository.GetAll().ToList();
            }

            if (touristSpotHandler.Get(spotId) == null)
            {
                throw new BadRequestException("The spot does not exist");
            }

            if(onlyAvailable)
            return accomodationRepository.GetAll(x => ((Accomodation)x).TouristSpot.Id == spotId && ((Accomodation)x).Available).ToList();

            return accomodationRepository.GetAll(x => ((Accomodation)x).TouristSpot.Id == spotId).ToList();


        }

        public Accomodation Get(int accomodationId)
        {
            return accomodationRepository.Get(accomodationId);
        }

        public bool Add(List<AccomodationImport> accomodations)
        {
            var ret = true;
            foreach (var accomodation in accomodations)
            {
                var spotImport = accomodation.TouristSpot;

                var spot = touristSpotHandler.Get(spotImport.Name);

                if (spot == null)
                {
                    spot = touristSpotHandler.Add(spotImport.ToEntity(), spotImport.RegionId, spotImport.CategoryIds, spotImport.Image);
                }
                try
                {
                    if (spot == null) throw new BadRequestException();
                    Add(accomodation.ToEntity(), spot.Id, accomodation.ImageNames);
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
            if(accommodation==null) throw new NotFoundException("The accomodation does not exist");

            double ret = 0;
            int days = (stay.CheckOut - stay.CheckIn).Days;
            ret += stay.AdultQuantity + stay.ChildrenQuantity * 0.5 + stay.BabyQuantity * 0.25;
            ret += ((int)stay.RetiredQuantity / 2) * 1.7;

            ret += stay.RetiredQuantity % 2;
            ret *= days * accommodation.Fee;
            return ret;
        }
    }
}
