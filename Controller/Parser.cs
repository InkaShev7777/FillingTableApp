using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillingTable.Controller
{
    internal class Parser
    {

        private int count = 0;
        public string GetQuery(string[] basesSql, int queryCount)
        {
            count = 0;
            List<string> query = new List<string>();

            string tableName = GetName(basesSql[0], true);
            query.Add($"INSERT INTO {tableName}");
            query.Add(GetValues(basesSql));
            query.Add("VALUES");
            query.Add(GetDataToInsert(queryCount));
            return string.Empty;
        }

        private string GetValues(string[] baseSql)
        {
            string value = "(";
            foreach (var item in baseSql)
            {
                if (item.ToLower().Contains("not null"))
                {
                    value += GetName(item, false);
                    count += 1;
                }
            }
            value = value.Remove(value.Length - 1, 1);
            value += ")";
            return value;
        }

        private string GetDataToInsert(int queryCount)
        {
            List<string> completeData = new List<string>();
            List<string> db = new List<string>();// заменить на получение 
            for (int i = 0; i < queryCount; i++)
            {
                string res = "(";
                for (int j = 0; j < count; j++)
                {
                    int index = new Random().Next(0, db.Count);
                    res += $"'{db[index]}',";
                    if (j == count - 1)
                        res = res.Remove(res.Length - 1, 1);
                }
                res += "),";
                if (i == queryCount - 1)
                {
                    res = res.Remove(res.Length - 1, 1);
                    res += ";";
                }
                res += "\n";
                completeData.Add(res);
            }
            return completeData.ToString();
        }

        private string GetName(string str, bool isTableName)
        {
            if (isTableName)
                return "[" + str.Split('[')[1].Replace("(", "");

            string name = str.Split('[')[1];
            return "[" + name.Substring(0, name.IndexOf(" "));
        }
    }
}
