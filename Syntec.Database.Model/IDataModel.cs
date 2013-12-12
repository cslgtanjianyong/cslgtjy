using System;
using System.Collections.Generic;
using System.Web;
using Syntec.Database;
using System.Data;

namespace Syntec.Database.Model
{
    public interface IDataModel
    {
        string[] PrimaryKeys { get; set; }
        string[] Fields { get; set; }
        DbType[] DataTypes { get; set; }
        string TableName { get; set; }
        string DatabaseName { get; }
        string ErrorMessage { get; set; }
        IDatabase Database {get;}

        void AddAutoIncreaseField(string strFieldName);
        bool Exists();
        bool Update();
        List<T> Query<T>(string[] strFields, string[] strValues);
        List<T> Query<T>(string strStatement, string[] strFields, string[] strValues);
        T QueryByPrimaryKey<T>(string[] strValues);
        T QueryFirst<T>(string[] strFields, string[] strValues);
        bool ExecuteUpdate();
        bool ExecuteUpdate(string strStatement, string[] strFields, string[] strValues);
        bool ExecuteInsert();
        bool ExecuteDelete();
        bool ExecuteDelete(string strStatement, string[] strFields, string[] strValues);
        void Value(string strField, object oValue);
        object Value(string strField);
        IDataModel CreateInstance();
    }
}
