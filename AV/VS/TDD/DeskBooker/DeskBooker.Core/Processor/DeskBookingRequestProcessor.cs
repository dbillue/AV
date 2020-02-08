using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using System;
using System.Linq;

namespace DeskBooker.Core.Processor
{
    public class DeskBookingRequestProcessor : IDeskBookingRequestProcessor
    {
        private readonly IDeskBookingRepository _deskBookingRepository;
        private readonly IDeskRepository _deskRepository;

        public DeskBookingRequestProcessor(IDeskBookingRepository deskBookingRepository,
          IDeskRepository deskRepository)
        {
            _deskBookingRepository = deskBookingRepository;
            _deskRepository = deskRepository;
        }

        public DeskBookingResult BookDesk(DeskBookingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            // Static Create<T> method, create return result.
            var result = Create<DeskBookingResult>(request);

            // Determine if booking is available, search by date.
            var availableDesks = _deskRepository.GetAvailableDesks(request.Date);

            // Check for return booking.
            if (availableDesks.FirstOrDefault() is Desk availableDesk)
            {
                // Booking is available, create request.
                var deskBooking = Create<DeskBooking>(request);

                // Assign booking Id.
                deskBooking.DeskId = availableDesk.Id;

                // Save booking.
                _deskBookingRepository.Save(deskBooking);

                // Assign booking Id to result.
                result.DeskBookingId = deskBooking.Id;

                // Return status code and result.
                result.Code = DeskBookingResultCode.Success;
            }
            else
            {
                // Booking not available, return status code.
                result.Code = DeskBookingResultCode.NoDeskAvailable;
            }

            // Return booking result :: status codes "Success" or "NoDeskAvailable".
            return result;
        }

        private static T Create<T>(DeskBookingRequest request) where T : DeskBookingBase, new()
        {
            return new T
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Date = request.Date
            };
        }
    }
}