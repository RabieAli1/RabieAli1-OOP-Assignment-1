# Part 3: Builder Pattern Answers

## Task 3.1: The 20-Parameter Constructor Problem

### 1. Why is a 20-parameter constructor bad in practice?

* **Hard to read:** Looking at `new Invoice(1, "Mona", "Cairo", "EG", 100, 10...)` makes it impossible to know what each value means.
* **Easy to make silent mistakes:** If you swap two strings (like Billing City and Shipping City) or two numbers (like Tax and Discount), C# won't show any error, but the app will save wrong data.
* **Hard to update:** Adding just one new optional parameter breaks every place in your code where this constructor is called.

### 2. Is it just "too long" or a deeper design issue?

It is a deeper design issue:

* **Too many responsibilities:** A single `Invoice` class shouldn't handle street validation, tax rules, and customer info all at once.
* **Missing proper objects:** Properties like street, city, and zip code naturally belong together inside an `Address` class, rather than being 10 separate string variables.

---

## Task 3.3: Why Composed Builders are Better

Small, composed builders are better than one big builder for these reasons:

1. **Single Responsibility:** Each builder does only one job. `AddressBuilder` only builds addresses, `OrderBuilder` handles payments/totals, and `InvoiceBuilder` just connects them together.
2. **Independent Validation:**
