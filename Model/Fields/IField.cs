using FillingTable.Utilities;
using Type = FillingTable.Utilities.Type;

namespace FillingTable.Model.Fields
{
    public interface IField
    {
        void SetValue(string value);

        string GetFieldName();

        Type GetFieldType();
        bool IsForeign();

    }
}
