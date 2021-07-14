using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Spine.Librerias.Database
{
    public class Conexion : IDisposable
    {
        // Encapsulamientos
        private bool lError { set; get; }
        private SqlConnection objConnection { set; get; }
        private SqlTransaction objTransaction { set; get; }

        // Constructor
        public Conexion(string psCadena)
        {
            objConnection = new SqlConnection(psCadena);
            objConnection.Open();
        }
        public void Open()
        {
            try
            {
                objConnection.Open();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void BeginTransaction()
        {
            try
            {
                objTransaction = objConnection.BeginTransaction();
            }
            catch (Exception)
            {
            }
        }
        public void Commit()
        {
            try
            {
                objTransaction.Commit();
            }
            catch (Exception)
            {
            }
        }
        public void Rollback()
        {
            try
            {
                objTransaction.Rollback();
            }
            catch (Exception)
            {
            }
        }
        public void Close()
        {
            try
            {
                objConnection.Close();
            }
            catch (Exception)
            {
            }
        }

        // Execute Update
        public int Ejecutar(string psProcedimiento, params SqlParameter[] parrParametros)
        {
            SqlCommand vobjCommand = new SqlCommand();
            vobjCommand.Connection = objConnection;
            if (objTransaction != null)
                vobjCommand.Transaction = objTransaction;
            vobjCommand.CommandText = psProcedimiento;
            vobjCommand.CommandType = CommandType.StoredProcedure;
            vobjCommand.Parameters.AddRange(parrParametros);
            int viRpta = vobjCommand.ExecuteNonQuery();
            return viRpta;
        }

        public async Task<int> EjecutarAsync(string psProcedimiento, params SqlParameter[] parrParametros)
        {
            SqlCommand vobjCommand = new SqlCommand();
            vobjCommand.Connection = objConnection;
            if (objTransaction != null)
                vobjCommand.Transaction = objTransaction;
            vobjCommand.CommandText = psProcedimiento;
            vobjCommand.CommandType = CommandType.StoredProcedure;
            vobjCommand.Parameters.AddRange(parrParametros);
            int viRpta = await vobjCommand.ExecuteNonQueryAsync();
            return viRpta;
        }



        // Execute Scalar
        public object EjecutarEscalar(string psProcedimiento, params SqlParameter[] parrParametros)
        {
            SqlCommand vobjCommand = new SqlCommand();
            vobjCommand.Connection = objConnection;
            if (objTransaction != null)
                vobjCommand.Transaction = objTransaction;
            vobjCommand.CommandText = psProcedimiento;
            vobjCommand.CommandType = CommandType.StoredProcedure;
            vobjCommand.Parameters.AddRange(parrParametros);
            return vobjCommand.ExecuteScalar();
        }

        public async Task<object> EjecutarEscalarAsync(string psProcedimiento, params SqlParameter[] parrParametros)
        {
            SqlCommand vobjCommand = new SqlCommand();
            vobjCommand.Connection = objConnection;
            if (objTransaction != null)
                vobjCommand.Transaction = objTransaction;
            vobjCommand.CommandText = psProcedimiento;
            vobjCommand.CommandType = CommandType.StoredProcedure;
            vobjCommand.Parameters.AddRange(parrParametros);
            return await vobjCommand.ExecuteScalarAsync();
        }

        // Execute Query
        public List<T> EjecutarConsulta<T>(string psProcedimiento, params SqlParameter[] parrParametros)
        {
            try
            {
                SqlCommand vobjCommand = new SqlCommand();
                vobjCommand.Connection = objConnection;
                if (objTransaction != null)
                    vobjCommand.Transaction = objTransaction;
                vobjCommand.CommandText = psProcedimiento;
                vobjCommand.CommandType = CommandType.StoredProcedure;
                vobjCommand.Parameters.AddRange(parrParametros);
                return GenericHelper.GetAsList<T>(vobjCommand.ExecuteReader());
            }
            catch (Exception)
            {
                lError = true;
                throw;
            }
        }

        public async Task<List<T>> EjecutarConsultaAsync<T>(string psProcedimiento, params SqlParameter[] parrParametros)
        {
            try
            {
                SqlCommand vobjCommand = new SqlCommand();
                vobjCommand.Connection = objConnection;
                if (objTransaction != null)
                    vobjCommand.Transaction = objTransaction;
                vobjCommand.CommandText = psProcedimiento;
                vobjCommand.CommandType = CommandType.StoredProcedure;
                vobjCommand.Parameters.AddRange(parrParametros);
                return GenericHelper.GetAsList<T>(await vobjCommand.ExecuteReaderAsync());
            }
            catch (Exception)
            {
                lError = true;
                throw;
            }
        }

        public void Dispose()
        {
            if (lError && objTransaction != null)
                objTransaction.Rollback();
            objConnection.Close();
        }

        internal class GenericHelper
        {
            private static IDictionary<string, PropertyInfo[]> arrPropertiesCache = new Dictionary<string, PropertyInfo[]>();

            private static ReaderWriterLockSlim objPropertiesCacheLock = new ReaderWriterLockSlim();

            public static PropertyInfo[] GetCachedProperties<T>()
            {
                PropertyInfo[] props = new PropertyInfo[0];
                if (objPropertiesCacheLock.TryEnterUpgradeableReadLock(100))
                {
                    try
                    {
                        if (!arrPropertiesCache.TryGetValue(typeof(T).FullName, out props))
                        {
                            props = typeof(T).GetProperties();
                            if (objPropertiesCacheLock.TryEnterWriteLock(100))
                            {
                                try
                                {
                                    arrPropertiesCache.Add(typeof(T).FullName, props);
                                }
                                finally
                                {
                                    objPropertiesCacheLock.ExitWriteLock();
                                }
                            }
                        }
                    }
                    finally
                    {
                        objPropertiesCacheLock.ExitUpgradeableReadLock();
                    }
                    return props;
                }
                else
                {
                    return typeof(T).GetProperties();
                }
            }

            public static List<T> GetAsList<T>(IDataReader pobjDbReader)
            {
                List<T> vlstData = new List<T>();

                if (pobjDbReader.GetSchemaTable() != null)
                {
                    List<string> vlstColumnas = new List<string>();
                    vlstColumnas = GetColumnList(pobjDbReader);
                    var vdctPropiedades = new Dictionary<string, PropertyInfo[]>();
                    while (pobjDbReader.Read())
                    {
                        T vobjItem = Activator.CreateInstance<T>();
                        foreach (string vsColumna in vlstColumnas)
                        {
                            try
                            {
                                LlenarObjetoRecursivo(vdctPropiedades, pobjDbReader, vsColumna, vobjItem, vsColumna, vobjItem.GetType());
                            }
                            catch (Exception voExcepcion)
                            {
                                throw new Exception("(" + vsColumna + ") " + voExcepcion.Message);
                            }
                        }
                        vlstData.Add(vobjItem);
                    }
                }
                return vlstData;
            }

            private static void LlenarObjetoRecursivo<T>(Dictionary<string, PropertyInfo[]> pdctProps, IDataReader pobjReader, string psReaderColumna, T pobjObjeto, string psPropiedad, Type pobjType)
            {
                if (!string.IsNullOrEmpty(psPropiedad))
                {
                    if (!pdctProps.ContainsKey(pobjType.Name))
                        pdctProps.Add(pobjType.Name, GetCachedProperties(pobjType));

                    if (!psPropiedad.Contains("."))
                    {
                        if (pobjReader[psReaderColumna] != DBNull.Value)
                        {
                            var vobjPropertyInfo = pdctProps[pobjType.Name].FirstOrDefault(x => x.Name == psPropiedad);
                            if (vobjPropertyInfo != null)
                            {
                                if (vobjPropertyInfo.PropertyType == typeof(Nullable<Int32>))
                                    vobjPropertyInfo.SetValue(pobjObjeto, pobjReader[psReaderColumna]);
                                else
                                    vobjPropertyInfo.SetValue(pobjObjeto, Convert.ChangeType(pobjReader[psReaderColumna], vobjPropertyInfo.PropertyType));
                            }
                        }
                    }
                    else
                    {
                        var vobjNuevaProp = pdctProps[pobjType.Name].FirstOrDefault(x => x.Name == psPropiedad.Split(new char[] { '.' })[0]);
                        if (vobjNuevaProp != null)
                        {
                            var veNuevEntidad = vobjNuevaProp.GetValue(pobjObjeto);
                            if (veNuevEntidad == null)
                            {
                                veNuevEntidad = Activator.CreateInstance(vobjNuevaProp.PropertyType);
                                vobjNuevaProp.SetValue(pobjObjeto, veNuevEntidad);
                            }

                            string vsNuevaProp = psPropiedad.Replace(psPropiedad.Split(new char[] { '.' })[0] + ".", "");

                            LlenarObjetoRecursivo(pdctProps, pobjReader, psReaderColumna, veNuevEntidad, vsNuevaProp, veNuevEntidad.GetType());
                        }
                    }
                }
            }

            public static PropertyInfo[] GetCachedProperties(Type pobjType)
            {
                PropertyInfo[] props = new PropertyInfo[0];
                if (objPropertiesCacheLock.TryEnterUpgradeableReadLock(100))
                {
                    try
                    {
                        if (!arrPropertiesCache.TryGetValue(pobjType.FullName, out props))
                        {
                            props = pobjType.GetProperties();
                            if (objPropertiesCacheLock.TryEnterWriteLock(100))
                            {
                                try
                                {
                                    arrPropertiesCache.Add(pobjType.FullName, props);
                                }
                                finally
                                {
                                    objPropertiesCacheLock.ExitWriteLock();
                                }
                            }
                        }
                    }
                    finally
                    {
                        objPropertiesCacheLock.ExitUpgradeableReadLock();
                    }
                    return props;
                }
                else
                {
                    return pobjType.GetProperties();
                }
            }

            private static bool IsNullableType(Type type)
            {
                return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
            }

            public static List<string> GetColumnList(IDataReader reader)
            {
                List<string> columnList = new List<string>();
                System.Data.DataTable readerSchema = reader.GetSchemaTable();
                for (int i = 0; i < readerSchema.Rows.Count; i++)
                    columnList.Add(readerSchema.Rows[i]["ColumnName"].ToString());
                return columnList;
            }
        }
    }
}
