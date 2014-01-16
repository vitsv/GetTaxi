using Data.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.Order
{
    public class OrderStatusModel
    {
        /// <summary>
        /// Id Zamowienia
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Status zamowienia
        /// </summary>
        public GlobalEnumerator.OrderStatus Status { get; set; }

        /// <summary>
        /// Nazwa statusu
        /// </summary>
        public String StatusText { get; set; }

        /// <summary>
        /// Czas do ktorego musi być przypisany samochod
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// Przypisany samochod
        /// </summary>
        public Data.Domain.Car AssignedCar { get; set; }

        /// <summary>
        /// Czak oczekiwanie w minutach
        /// </summary>
        public int? WaitTime { get; set; }
    }
}