using System;

namespace HotelReservationSystem
{
    public class Reservation
    {
        public int ReservationId { get; }
        public DateTime CheckInDate { get; }
        public DateTime CheckOutDate { get; }
        public Room Room { get; }
        public ReservationStatus Status { get; private set; }

        public Reservation(int reservationId, DateTime checkInDate, DateTime checkOutDate, Room room)
        {
            if (reservationId <= 0)
                throw new ArgumentOutOfRangeException(nameof(reservationId), "Id must be positive.");
            if (room == null)
                throw new ArgumentNullException(nameof(room));
            if (room.IsUnderMaintenance)
                throw new InvalidOperationException($"Room {room.RoomNumber} is under maintenance.");
            if (checkOutDate <= checkInDate)
                throw new ArgumentException("Check-out date must be after check-in date.", nameof(checkOutDate));

            ReservationId = reservationId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Room = room;
            Status = ReservationStatus.Pending;
        }

        public int Nights => (CheckOutDate - CheckInDate).Days;
        public decimal TotalCost => Nights * Room.NightlyRate;

        public void Confirm()
        {
            if (Status != ReservationStatus.Pending)
                throw new InvalidOperationException(
                    $"Cannot confirm a reservation that is {Status}. Only Pending can be confirmed.");

            Status = ReservationStatus.Confirmed;
        }

        public void CheckIn()
        {
            if (Status != ReservationStatus.Confirmed)
                throw new InvalidOperationException(
                    $"Cannot check in a reservation that is {Status}. Only Confirmed can be checked in.");

            Status = ReservationStatus.CheckedIn;
        }

        public void CheckOut()
        {
            if (Status != ReservationStatus.CheckedIn)
                throw new InvalidOperationException(
                    $"Cannot check out a reservation that is {Status}. Only CheckedIn can be checked out.");

            Status = ReservationStatus.CheckedOut;
        }

        // Pending أو Confirmed ← Cancelled
        public void Cancel()
        {
            if (Status != ReservationStatus.Pending && Status != ReservationStatus.Confirmed)
                throw new InvalidOperationException(
                    $"Cannot cancel a reservation that is {Status}. Only Pending or Confirmed can be cancelled.");

            Status = ReservationStatus.Cancelled;
        }
    }
}