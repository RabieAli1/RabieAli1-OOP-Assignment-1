using System;

namespace HotelReservationSystem
{
    public class Room
    {
        public int RoomNumber { get; }
        public RoomType RoomType { get; }
        public decimal NightlyRate { get; private set; }
        public bool IsUnderMaintenance { get; private set; }

        public Room(int roomNumber, RoomType roomType, decimal nightlyRate)
        {
            if (nightlyRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(nightlyRate), "Nightly rate must be positive.");
            if (roomNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(roomNumber), "roomNumber  must be positive.");

            RoomNumber = roomNumber;
            RoomType = roomType;
            NightlyRate = nightlyRate;
            IsUnderMaintenance = false;

        }

        public void ChangeRate(decimal newRate)
        { 
            if(newRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(newRate), "Nightly rate must be positive.");
            NightlyRate = newRate;

            

        }


        public void StartMaintenance()
        {
            if(IsUnderMaintenance) 
                throw new InvalidOperationException($"Room {RoomNumber} is already under maintenance.");

            IsUnderMaintenance=true;
        }


        public void EndMaintenance()
        {
            if (IsUnderMaintenance == false)
                throw new InvalidOperationException($"Room {RoomNumber} is already Ended maintenance.");

            IsUnderMaintenance = false;
        }

        private readonly List<Reservation> _reservations = new List<Reservation>();
        public IReadOnlyList<Reservation> Reservations => _reservations;


        public bool HasConflict(DateTime checkIn, DateTime checkOut)
        {
            foreach (var existing in _reservations)
            {
                if (existing.Status == ReservationStatus.Cancelled ||
                    existing.Status == ReservationStatus.CheckedOut)
                    continue;

                if (checkIn < existing.CheckOutDate && checkOut > existing.CheckInDate)
                    return true;
            }

            return false;
        }

        internal void AddReservation(Reservation reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation));
            if (HasConflict(reservation.CheckInDate, reservation.CheckOutDate))
                throw new InvalidOperationException(
                    $"Room {RoomNumber} is already booked for these dates.");

            _reservations.Add(reservation);
        }
    }
}
