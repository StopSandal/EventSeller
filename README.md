Ticket Management Web API

This API supports CRUD operations via REST architecture for managing tickets, events, place addresses, place halls, hall sectors, and ticket seats.

**New**

1. Added multiple ticket registrations.
2. Added ticket selling logic that interacts with a stub payment system (new repository).
3. Added numerous admin statistics with different types of exports.


To use any other method except Get, user should be authorized.

Usage

Create (POST) methods

Read (GET) methods

Update (PUT) methods

Delete (DELETE) methods


The solution consists of 4 projects:

1. Main Layer (Presentation) - [EventSeller](https://github.com/StopSandal/EventSeller)
2. Service Layer (Service) - [EventSeller.Services](https://github.com/StopSandal/EventSeller.Services)
3. Data Layer (Data) - [EventSeller.DataLayer](https://github.com/StopSandal/EventSeller.DataLayer)
4. Stub of Payment System (EXternal api) - [PaymentSystem](https://github.com/StopSandal/PaymentSystem)

To get started:

1. Clone the Main Layer project using: `git clone https://github.com/StopSandal/EventSeller`
2. Clone the Service Layer project using: `git clone https://github.com/StopSandal/EventSeller.Services`
3. Clone the Data Layer project using: `git clone https://github.com/StopSandal/EventSeller.DataLayer`
4. Clone the Payment System stub project using: `git clone https://github.com/StopSandal/PaymentSystem`

Then, follow these steps:

1. Create a database for your data on your server.
2. Update the connection string in `appsettings.json` to point to your database.
3. Finally, use Entity Framework tools to update your database schema.  Ensure that your project is configured to work with Entity Framework, specifically targeting the [EventSeller.DataLayer] project.
4. Set up runnable projects to EventSeller and PaymentSystem 
