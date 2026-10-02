namespace HotelReservationSystem;

    public class Guest
    {
        public int GuestId { get; }
        public string FullName { get; }
        public string PhoneNumber { get; }

        private readonly List<Reservation> _reservations = new();
        public IReadOnlyList<Reservation> Reservations => _reservations;

        public Guest(int guestId, string fullName, string phoneNumber)
        {
            if (guestId <= 0)
                throw new ArgumentOutOfRangeException(nameof(guestId), "Id must be positive.");
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name is required.", nameof(fullName));
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number is required.", nameof(phoneNumber));

            GuestId = guestId;
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }


        public Reservation MakeReservation(int reservationId, DateTime checkIn, DateTime checkOut, Room room)
        {
            var reservation = new Reservation(reservationId, checkIn, checkOut, room);


            room.AddReservation(reservation);   
            _reservations.Add(reservation);       


        return reservation;
        }

        

       
    }
        
