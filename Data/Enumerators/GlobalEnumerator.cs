using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Enumerators
{
    public class GlobalEnumerator
    {
        public enum UserRoleId
        {
            User = 1,
            Company = 2,
            NotActive = 3
        }

        public enum OrderStatus
        {
            Created,
            Assigned,
            Arrived,
            Incar,
            Done,
            Canceled_by_taxi,
            Canceled_by_system,
            Canceled_by_client
        }

        public enum OrderCanceledBy
        {
            Client,
            Taxi,
            System
        }

        public enum OrderCancelCause
        {

        }

        public enum OrderNoteType
        {
            CantSeeCar,
            InCar,
            Done,
            Feedback
        }

        public enum OrderClass
        {
            Fastest,
            Econom,
            Comfort,
            Business
        }

        public enum OrderPriority
        {
            Low,
            Normal,
            Hight,
            VIP
        }
    }
}
