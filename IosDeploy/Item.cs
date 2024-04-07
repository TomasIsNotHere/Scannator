using System;
using SQLite;

namespace IosDeploy
{

        [Table("item")]
        public class Item
        {
            [PrimaryKey, AutoIncrement, Column("id")]
            public int id { get; set; }

            public string name { get; set; }
            public string category { get; set; }
            public string description { get; set; }
            public int count { get; set; }
            public string barcode { get; set; }
        }
    
}

