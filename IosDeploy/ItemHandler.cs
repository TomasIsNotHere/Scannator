using System;
using SQLite;

namespace IosDeploy
{
    public class ItemHandler
    {
        string _dbPath;
        private SQLiteConnection conn;

        public ItemHandler(string dbPath)
        {
            _dbPath = dbPath;
            Init();

        }

        public void Init()
        {
            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Item>();
        }

        public List<Item> GetAllItems()
        {
            Init();
            return conn.Table<Item>().ToList();
        }

        public Item GetItemByCode(string code)
        {

            conn = new SQLiteConnection(_dbPath);
            Item item = conn.Table<Item>().Where(i => i.barcode == code).FirstOrDefault();

            if(item is null)
            {
                Item returnItem = new Item() { id = 0, name = "IsNotThere", category = "a", description = "a", count = 0, barcode = "a" };
                return returnItem;
            } else
            {
                return item;
            }
        }

        public void Add(Item item)
        {
            conn = new SQLiteConnection(_dbPath);
            conn.Insert(item);
        }

        public void Update(Item item)
        {
            conn = new SQLiteConnection(_dbPath);
            conn.Update(item);
        }

        public void Delete(Item item)
        {
            conn = new SQLiteConnection(_dbPath);
            conn.Delete(item);
        }

    }

}

