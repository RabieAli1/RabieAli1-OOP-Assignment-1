# Hotel Reservation System

## Design
- **Room**: number and type are fixed. Rate and maintenance change only through methods.
- **Reservation**: id, dates and room are fixed. Status changes only through `Confirm`, `CheckIn`, `CheckOut` and `Cancel`. `TotalCost` is calculated, not stored.
- **Guest**: id, name and phone are fixed. Reservations are added only through `MakeReservation`.

## Decisions
- **Double-booking:** `Room` checks it, because it knows all of its reservations.
- **`AddReservation` is `internal`:** so only `Guest` calls it. It is not full protection.
- **Same-day check-out and check-in:** allowed.
- **Cancelled and CheckedOut reservations:** ignored when checking overlap.
- **`StartMaintenance` twice:** does nothing, no exception.
- **Room under maintenance with existing reservations:** nothing happens to them. Maintenance only blocks new bookings.