using FillingTable.Model.Fields;
using FillingTable.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = FillingTable.Utilities.Type;

namespace FillingTable.Controller
{
    internal class FieldController
    {
        private List<string> _query;
        private List<IField> _fields;
        public FieldController()
        {
            _query = new List<string>();
            _fields = new List<IField>();
        }

        public void SetSqlData(string data)
        {
            data = data.Trim();
            string[] sql = data.Split('\n');
            CreateFields(sql);
            CreateQuery(sql);
        }

        private void CreateQuery(string[] data)
        {
            string tableName = Parser.GetName(data[0], true);
            _query.Add($"INSERT INTO {tableName}");
            _query.Add(GetFieldsNames());
            SetDataToFields();
        }

        private void CreateFields(string[] data)
        {
            foreach (var item in data)
            {
                if (!item.ToLower().Contains("not null"))
                    continue;
                if (item.ToLower().Contains("identity"))
                    continue;

                int maxLenght = 0;
                if (item.IndexOf('(') > 0)
                    maxLenght = Convert.ToInt32(item.Substring(item.IndexOf('(') + 1, item.IndexOf(')') - item.IndexOf('(') - 1));

                Type type = FieldType.SelectType(item);
                _fields.Add(FieldFactory.CreateField(type, item, maxLenght));
            }
        }

        private string GetFieldsNames()
        {
            string fieldsForInsert = "(";
            foreach (var item in _fields)
            {
                string name =  item.GetFieldName()+",";
                fieldsForInsert += name;
            }
            fieldsForInsert = fieldsForInsert.Remove(fieldsForInsert.Length - 1, 1);
            fieldsForInsert += ")";
            return fieldsForInsert;
        }

        private void SetDataToFields()
        {
            for (int i = 0; i < _fields.Count; i++)
            {
                if (_fields[i].GetFieldName().ToLower().Contains("last") && _fields[i].GetFieldName().ToLower().Contains("name"))
                    _fields[i].SetValue("some last name");
                else if (_fields[i].GetFieldName().ToLower().Contains("first") && _fields[i].GetFieldName().ToLower().Contains("name"))
                    _fields[i].SetValue("some first name");
                else if (_fields[i].GetFieldName().ToLower().Contains("full") && _fields[i].GetFieldName().ToLower().Contains("name"))
                    _fields[i].SetValue("some first name + some last name");
                else if (_fields[i].GetFieldName().ToLower().Contains("birthday"))
                    _fields[i].SetValue("some birthday");
                else if (_fields[i].GetFieldName().ToLower().Contains("city"))
                    _fields[i].SetValue("some city");
                else if (_fields[i].GetFieldName().ToLower().Contains("country"))
                    _fields[i].SetValue("some country");
                else if (_fields[i].GetFieldName().ToLower().Contains("street"))
                    _fields[i].SetValue("some street");
                else if (_fields[i].GetFieldName().ToLower().Contains("phonenumber"))
                    _fields[i].SetValue("some number");
                else
                {
                    //выполниться когда будет поле по типу Description, Title и тд. Сгенерировать рандомные слова но сначала проверить тип
                    if (_fields[i].GetFieldType().Equals(Type.INT))
                        _fields[i].SetValue("1");
                    else if(_fields[i].GetFieldType().Equals(Type.FLOAT))
                        _fields[i].SetValue("1.45");
                    else if (_fields[i].GetFieldType().Equals(Type.STRING))
                        _fields[i].SetValue("Some random words");
                    else if (_fields[i].GetFieldType().Equals(Type.DATE))
                        //соблюсти именно такой формат даты про время хз
                        _fields[i].SetValue("2022-10-5");

                }

                if (_fields[i].IsForeign())
                    _fields[i].SetValue("REPLACE THIS ON YOUR KEY FROM ANOTHER TABLE");

            }
        }
    }
}
