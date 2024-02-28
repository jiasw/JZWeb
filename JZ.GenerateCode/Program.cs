using SqlSugar;

namespace JZ.GenerateCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateModel();
            Console.ReadKey();
        }

        /// <summary>
        /// 生成model实体
        /// </summary>
        private static void CreateModel()
        {
            Console.WriteLine("开始生成Model");
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                DbType = DbType.MySql,
                ConnectionString = "server=175.178.236.80;Database=spfbjl;Uid=root;Pwd=jiasw1q2w#E$R;",
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true,
                ConfigId = "12",


            });

            foreach (var item in db.DbMaintenance.GetTableInfoList())
            {
                string entityName = item.Name.ToUpper();/*实体名大写*/
                db.MappingTables.Add(entityName, item.Name);
            }

            db.DbFirst.IsCreateAttribute().CreateClassFile("D:\\Workstuio\\VideoUpload\\VideoNotes\\Models", "VideoNotes.Entitys");
            Console.WriteLine("结束生成Model");
        }


    }
}