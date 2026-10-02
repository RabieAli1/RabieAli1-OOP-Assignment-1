# Critique of the C++ Order System

## 1. Parallel arrays
**Problem:** One customer is split across 5 arrays (`customerIds`, `customerNames`, `customerEmails`, ...). Products and orders are the same.
**What can go wrong:** The data of one customer is not in one place. If I change one array and forget another, the data gets out of sync, and the code is hard to read.

## 2. Global variables and no data protection
**Problem:** All the data and counters are global, so any function can change them directly.
**What can go wrong:** Someone can write `productStock[i] = -999` or mark an empty order as paid. The rules only live inside some functions, so they can be bypassed.

## 3. Indexes instead of real references
**Problem:** An order stores `orderCustomerIndexes`, which is just a number pointing into an array. A failed search returns `-1`.
**What can go wrong:** A wrong number points to the wrong customer or product, and nothing checks it. If someone forgets to check for `-1`, the program reads `array[-1]` and crashes or gives a wrong result.

## 4. Logic mixed with printing
**Problem:** Functions like `addCustomer` and `markOrderPaid` print `ERROR` with `cout` and just `return`.
**What can go wrong:** The caller never knows the function failed, so the next steps keep running on bad data. Also, the logic cannot be reused or tested without a console.