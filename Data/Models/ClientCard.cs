using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GymSys.Data.Models
{
    public class ClientCard
    {
        public int CardId { get; set; }

        public CardType CardType { get; set; }        

        public DateTime? LastPayment { get; set; }

        public DateTime ExpareDate { get; set; }

        public decimal TotalSpend { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

    }
}

/* When the client spend X moneys the card is:
 * Between 0 - 100 leva = Bronze  - 5 % discount on purchases 
 * Between 101 - 500 leva = Silver - 7 % discount on purchases 
 * Between 501 - 1000 leva = Gold - 9 % discount on purchases
 * Over 1000 leva = Platinum - 12 % discount on purchases
 * The VIP card is for employees and other special persons
 
 */

