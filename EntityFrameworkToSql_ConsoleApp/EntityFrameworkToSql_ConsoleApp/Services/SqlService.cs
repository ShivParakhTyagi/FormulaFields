using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkToSql_ConsoleApp.Model;

namespace EntityFrameworkToSql_ConsoleApp.Services
{
    public class SqlService
    {


        public async Task<T> GetDataAsync<T>(string connectionString, string sqlCommand) where T : new()
        {
            using (var con = new SqlConnection(connectionString))
            {
                await con.OpenAsync();

                var command = con.CreateCommand();
                command.CommandText = sqlCommand;
                command.CommandType = CommandType.Text;
                var reader = await command.ExecuteReaderAsync();

                var type = typeof(T);

                var obj = Activator.CreateInstance(type);

                while (reader.Read())
                {
                    foreach (var p in type.GetProperties())
                    {
                        if (p.PropertyType.Namespace == typeof(User).Namespace)
                        {
                            var val = await Construct(reader, p.PropertyType);
                            p.SetValue(obj, val);
                            continue;
                        }
                        if (!p.PropertyType.IsInterface)
                        {
                            p.SetValue(obj, reader[p.Name] == null || reader[p.Name] == DBNull.Value ? null : reader[p.Name]);
                        }

                        if (p.PropertyType.IsInterface)
                        {
                            // this is some sort of interface property
                            var val =p.GetValue(obj);
                            var valType = val.GetType();
                            Console.WriteLine(valType);
                            //p.SetValue(obj, reader[p.Name] == null || reader[p.Name] == DBNull.Value ? null : reader[p.Name]);
                        }
                    }
                }
                reader.Close();
                return (T)obj;

            }

        }

        private async Task<object> Construct(SqlDataReader reader,Type type)
        {

            var obj = Activator.CreateInstance(type);
            
            {
                foreach (var p in type.GetProperties())
                {

                    if (p.PropertyType.Namespace == typeof(User).Namespace)
                    {
                        var val = await Construct(reader, p.PropertyType);
                        p.SetValue(obj, val);
                        continue;
                    }
                    if (!p.PropertyType.IsInterface)
                    {
                        p.SetValue(obj, reader[p.Name] == null || reader[p.Name] == DBNull.Value ? null : reader[p.Name]);
                    }
                }
            }
            return obj;
        }

        public async Task<List<Dictionary<string, object>>> GetDataAsync(string connectionString, string sqlCommand)
        {
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    await con.OpenAsync();

                    var command = con.CreateCommand();
                    command.CommandText = sqlCommand;
                    command.CommandType = CommandType.Text;
                    var reader = await command.ExecuteReaderAsync();

                    var listOfDictionary = new List<Dictionary<string, object>>();

                    while (reader.Read())
                    {
                        var dictionary = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var name = reader.GetName(i);
                            if(!dictionary.ContainsKey(name))
                            dictionary.Add(name,
                                reader[i] == null || reader[i] == DBNull.Value ? null : reader[i]);
                        }
                        listOfDictionary.Add(dictionary);
                    }
                    reader.Close();
                    return listOfDictionary;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("ExeptionAt-SqlService.cs-GetDataAsync()-" + ex.Message);
                return null;
            }
        }

    }
}
