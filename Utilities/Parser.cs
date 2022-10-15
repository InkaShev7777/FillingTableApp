using System.Collections.Generic;


namespace FillingTable.Utilities
{
    
    internal class Parser
    {
        private List<string> types = new List<string>();

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
