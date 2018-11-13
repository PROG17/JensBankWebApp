using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;


namespace JensBankWebApp.ViewModels
{
    public class TransferViewModel
    {
        [DisplayName("Origin Account no.")]
        public int OriginAccountId { get; set; }

        [DisplayName("Destination Account no.")]
        public int DestinationAccountId { get; set; }

        [DisplayName("Amount to transfer")]
        public decimal TransferAmount { get; set; }

        [DisplayName("Origin account new balance")]
        public decimal OriginNewAmount { get; set; }

        [DisplayName("Destination account new balance")]
        public decimal DestinationNewAmount { get; set; }

        public string Message { get; set; }

        public bool IsError { get; set; }
    }
}
