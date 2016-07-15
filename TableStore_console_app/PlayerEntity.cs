using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace TableStore_console_app
{
    public class PlayerEntity : TableEntity
    {
        public PlayerEntity(string skey, string srow)
        {
            this.PartitionKey = skey;
            this.RowKey = srow;
        }

        public PlayerEntity() { }

        public string Sport_VC { get; set; }
        public string Club_VC { get; set; }
        public string First_Name_VC { get; set; }
        public string Last_Name_VC { get; set; }
        public string Position_VC { get; set; }
    }
}
