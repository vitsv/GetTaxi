using System;
using System.Collections.Generic;
using System.ComponentModel;

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
            [Description("Oczekuję na samochód")]
            Created,
            [Description("Samochód jest w drodzę")]
            Assigned,
            [Description("Samochód już czeka")]
            Arrived,
            [Description("Pasażer w samochodzie")]
            Incar,
            [Description("Koniec podróży")]
            Done,
            [Description("Anulowane prze taxi")]
            Canceled_by_taxi,
            [Description("Anulowane przez system")]
            Canceled_by_system,
            [Description("Anulowane przez klienta")]
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

        /// <summary>
        /// Buduje słownik na podstawie podanego enum
        /// </summary>
        /// <typeparam name="T">Typ enumeratora</typeparam>
        /// <returns></returns>
        public static IEnumerable<DictionaryItem> GetDictionary<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            List<DictionaryItem> dictionary = new List<DictionaryItem>();
            int displayOrder = 1;
            foreach (T item in Enum.GetValues(typeof(T)))
            {

                var type = typeof(T);
                var memInfo = type.GetMember(item.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute),
                    false);
                if (attributes != null)
                {
                    var description = ((DescriptionAttribute)attributes[0]).Description;
                    dictionary.Add(new DictionaryItem { Name = description, Value = Convert.ToInt32(item), DisplayOrder = displayOrder });
                }
                else
                {
                    dictionary.Add(new DictionaryItem { Name = item.ToString(), Value = Convert.ToInt32(item), DisplayOrder = displayOrder });
                }


                displayOrder++;
            }

            return dictionary;
        }

        /// <summary>
        /// Zwraca nazwę pola enumeratora
        /// </summary>
        /// <typeparam name="T">Typ enumeratora</typeparam>
        /// <param name="enumItem">Pole enumeratora</param>
        /// <returns></returns>
        public static string GetEnumName<T>(T enumItem) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            var type = typeof(T);
            var memInfo = type.GetMember(enumItem.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute),
                false);
            if (attributes != null)
            {
                var description = ((DescriptionAttribute)attributes[0]).Description;
                return description;
            }
            else
            {
                return enumItem.ToString();
            }
        }
    }
}
