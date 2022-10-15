
namespace FillingTable.Utilities
{
    public enum Type
    {
        STRING,
        INT,
        FLOAT,
        DATE
    }
    internal class FieldType
    {
        public static Type SelectType(string str)
        {
            if (str.ToLower().Contains("nvarchar") || str.ToLower().Contains("varchar"))
                return Type.STRING;
            if (str.ToLower().Contains("int"))
                return Type.INT;
            if (str.ToLower().Contains("float") || str.ToLower().Contains("money"))
                return Type.FLOAT;
            if (str.ToLower().Contains("datetime") || str.ToLower().Contains("date"))
                return Type.DATE;
            return Type.STRING;
        }
    }
}
