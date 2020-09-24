using InterfaceProject.Entities;
using System;

namespace InterfaceProject.Services
{
    class RentalService 
    {
        public double PricePerHour { get; private set; }
        public double PricePerDay { get; private set; }

        private ITaxService _taxService;

        public RentalService(double pricePerHour, double pricePerDay, ITaxService taxService)
        {
            PricePerHour = pricePerHour;
            PricePerDay = pricePerDay;
            _taxService = taxService;
        }

        public void ProcessInvoice (CarRental carRental)
        {
            TimeSpan duration = carRental.Finish.Subtract(carRental.Start);

            double basicAmount = 0.0;
            if(duration.TotalHours <= 12)
            {
                basicAmount = PricePerHour * Math.Ceiling(duration.TotalHours);
            }
            else
            {
                basicAmount = PricePerDay * Math.Ceiling(duration.TotalDays);
            }

            double tax = _taxService.Tax(basicAmount);

            carRental.Invoice = new Invoice(basicAmount, tax);
        }
    }
}
