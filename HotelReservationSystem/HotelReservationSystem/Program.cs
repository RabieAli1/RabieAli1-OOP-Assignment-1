
namespace HotelReservationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var room = new Room(101, RoomType.Single, 500m);
            var guest = new Guest(1, "Mona Ali", "01012345678");

            var r = guest.MakeReservation(1, new DateTime(2026, 10, 1), new DateTime(2026, 10, 4), room);
            Console.WriteLine(r.Status);      
            Console.WriteLine(r.TotalCost); 

            r.Confirm();
            r.CheckIn();
            r.CheckOut();
            Console.WriteLine(r.Status);   
        }
    }
}
