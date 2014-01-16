﻿using Data.Domain;
using Data.Enumerators;
using Data.Models;
using Managers.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managers
{
    public class OrderManager : GlobalManager
    {
        public IEnumerable<Order> GetOrderForCompany(int companyId)
        {
            return RepoGeneric.Find<Order>(c => c.OrderProperties.CompanyId == companyId || !c.OrderProperties.CompanyId.HasValue).ToList();
        }

        public IUnitOfWorkResult AddOrder(Order order)
        {
            var repo = RepoGeneric;

            repo.Add<Order>(order);

            return repo.UnitOfWork.SaveChanges();
        }

        public IUnitOfWorkResult OrderProcess(int orderId, GlobalEnumerator.OrderStatus status, int? assignedCarId = null, double? finalPrice = null, string comment = null)
        {
            var repo = RepoGeneric;
            var order = repo.FindOne<Order>(c => c.OrderId == orderId);

            if (order == null)
                return this.CreateResultError(string.Format("Order with id = {0} not found", orderId));

            order.Status = (int)status;

            switch (status)
            {
                case GlobalEnumerator.OrderStatus.Assigned: order.TimeAssigned = DateTime.Now;
                    if (assignedCarId.HasValue)
                        order.CarId = assignedCarId.Value;
                    else
                        return this.CreateResultError("AssignedCarId can't be null!");
                    break;
                case GlobalEnumerator.OrderStatus.Arrived: order.TimeArrived = DateTime.Now; break;
                case GlobalEnumerator.OrderStatus.Incar: order.TimeInCar = DateTime.Now; break;
                case GlobalEnumerator.OrderStatus.Done: order.TimeDone = DateTime.Now;
                    if (finalPrice.HasValue)
                        order.FinalPrice = finalPrice.Value;
                    break;
            }

            if (!string.IsNullOrEmpty(comment))
                order.TaxiComment = comment;

            return repo.UnitOfWork.SaveChanges();
        }

        public IUnitOfWorkResult OrderCancel(int orderId, GlobalEnumerator.OrderStatus status, GlobalEnumerator.OrderCancelCause cause)
        {
            var repo = RepoGeneric;
            var order = repo.FindOne<Order>(c => c.OrderId == orderId);

            if (order == null)
                return this.CreateResultError(string.Format("Order with id = {0} not found", orderId));

            order.Status = (int)status;
            switch (status)
            {
                case GlobalEnumerator.OrderStatus.Canceled_by_client: order.CanceledBy = (int)GlobalEnumerator.OrderCanceledBy.Client; break;
                case GlobalEnumerator.OrderStatus.Canceled_by_system: order.CanceledBy = (int)GlobalEnumerator.OrderCanceledBy.System; break;
                case GlobalEnumerator.OrderStatus.Canceled_by_taxi: order.CanceledBy = (int)GlobalEnumerator.OrderCanceledBy.Taxi; break;
            }
            order.TimeCanceled = DateTime.Now;
            order.CancelCause = (int)cause;

            return repo.UnitOfWork.SaveChanges();
        }

        public IUnitOfWorkResult OrderNote(int orderId, GlobalEnumerator.OrderNoteType type, int? vote = null, string comment = null)
        {
            var repo = RepoGeneric;
            var order = repo.FindOne<Order>(c => c.OrderId == orderId);

            if (order == null)
                return this.CreateResultError(string.Format("Order with id = {0} not found", orderId));

            var orderNote = new OrderNote();
            orderNote.NoteType = (int)type;
            switch (type)
            {
                    //TODO Obsluzyc zmiane rankingu firmy uwzgledniajac ten glos
                case GlobalEnumerator.OrderNoteType.Feedback: orderNote.Vote = vote.GetValueOrDefault(); break;
            }
            if (!string.IsNullOrEmpty(comment))
                orderNote.UserComment = comment;

            orderNote.CreationTime = DateTime.Now;

            repo.Add<OrderNote>(orderNote);

            return repo.UnitOfWork.SaveChanges();
        }

        public IEnumerable<OrderGridModel> GetOrdersGridModelForCompany(int companyId)
        {
            //TODO: filtrowanie zamówień przez statusy
            var orders = RepoGeneric.Find<Order>(c => c.OrderProperties.CompanyId == companyId || !c.OrderProperties.CompanyId.HasValue)
                .Select(c => new OrderGridModel()
                {
                    OrderId = c.OrderId,
                    ClientId = c.ClientId,
                    Status = c.Status,
                    //StatusName = 
                    TimeCreated = c.TimeCreated,
                    AddressFrom = c.Address.AddressFrom,
                    AddressTo = c.Address.AddressTo,
                    Client = c.Client.LastName + " " + c.Client.FirstName,
                    EstimatedPrice = c.EstimatedPrice,
                    Priority = c.OrderProperties.Priority
                })
                .ToList();

            return orders;
        }
    }
}
