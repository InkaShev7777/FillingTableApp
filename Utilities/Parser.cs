using System.Collections.Generic;


namespace FillingTable.Utilities
{
    
    internal class Parser
    {
        private List<string> types = new List<string>();
        //move to fieldControllers
        //public string GetQuery(string[] basesSql, int queryCount)
        //{
        //    count = 0;
        //    List<string> query = new List<string>();

        //    string tableName = GetName(basesSql[0], true);
        //    query.Add($"INSERT INTO {tableName}");
        //    query.Add(GetValues(basesSql));
        //    query.Add("VALUES");
        //    //query.Add(GetDataToInsert(queryCount));
        //    return string.Empty;
        //}

        //move to fieldControllers

        //private string GetValue(string[] baseSql)
        //{
        //    string value = "(";
        //    foreach (var item in baseSql)
        //    {
        //        if (item.ToLower().Contains("not null"))
        //        {
        //            types.Add(item.Split(' ')[1]);
        //            value += GetName(item, false);
        //        }
        //    }
        //    value = value.Remove(value.Length - 1, 1);
        //    value += ")";
        //    return value;
        //}

        //move to fieldControllers

        //private string GetDataToInsert(int queryCount)
        //{
        //    List<string> completeData = new List<string>();
        //    List<string> db = new List<string>();// заменить на получение 
        //    for (int i = 0; i < queryCount; i++)
        //    {
        //        string res = "(";
        //        for (int j = 0; j < count; j++)
        //        {
        //            int index = new Random().Next(0, db.Count);
        //            res += $"'{db[index]}',";
        //            if (j == count - 1)
        //                res = res.Remove(res.Length - 1, 1);
        //        }
        //        res += "),";
        //        if (i == queryCount - 1)
        //        {
        //            res = res.Remove(res.Length - 1, 1);
        //            res += ";";
        //        }
        //        res += "\n";
        //        completeData.Add(res);
        //    }
        //    return completeData.ToString();
        //}

        public static string GetName(string str, bool isTableName)
        {
            if (isTableName)
                return "[" + str.Split('[')[1].Replace("(", "");

            string name = str.Split('[')[1];
            return "[" + name.Substring(0, name.IndexOf(" "));
        }

        public static bool IsUnique(string str) => str.ToLower().Contains("unique");
        public static bool IsForeign(string str) => str.ToLower().Contains("foreign");
    }
}
