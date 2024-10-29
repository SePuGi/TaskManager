using Microsoft.Data.Sqlite;
using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSqlLite
{
    public class DB_connection
    {
        public DB_connection()
        {
        }

        #region Usuarios

        public static List<Usuario> GetUsuarios()
        {
            using (SQLiteDBContext context = new SQLiteDBContext())
            {
                context.Database.EnsureCreated();
                using (DbConnection db = context.GetConnection())
                {
                    db.Open();

                    List<Usuario> usuarios = new List<Usuario>();

                    DbCommand cmd = db.CreateCommand();
                    cmd.CommandText = "SELECT id, nombre, correo FROM Usuario";
                    DbDataReader result = cmd.ExecuteReader();
                    
                    while (result.Read())
                        usuarios.Add(new Usuario(result.GetInt32(0), result.GetString(1), result.GetString(2), ""));
                    
                    db.Close();

                    return usuarios;
                }
            }
        }

        /// <summary>
        /// Comprueba si el usuario y la contraseña son correctos, la contraseña debe llegar encriptada
        /// </summary>
        /// <param name="correo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Usuario CheckUser(string correo, string password)
        {
            using (SQLiteDBContext context = new SQLiteDBContext())
            {
                context.Database.EnsureCreated();
                using (DbConnection db = context.GetConnection())
                {
                    db.Open();

                    DbCommand cmd = db.CreateCommand();
                    cmd.CommandText = $"SELECT id, nombre FROM Usuario where correo = '{correo}' and password = '{password}'";
                    DbDataReader result = cmd.ExecuteReader();

                    //si no hay resultado return null
                    if (!result.HasRows)
                    {
                        db.Close();
                        return null;
                    }
                    else
                    {
                        try 
                        {
                            result.Read();
                            return new Usuario(result.GetInt32("id"), result.GetString("nombre"), correo, "");
                        }
                        catch(Exception e)
                        {
                            throw new Exception("Error en la base de datos, checkUser");
                        }
                        finally
                        {
                            db.Close();
                        }
                    }
                }
            }
        }

        public static Usuario GetUserById(int id)
        {
            using (SQLiteDBContext context = new SQLiteDBContext())
            {
                context.Database.EnsureCreated();
                using (DbConnection db = context.GetConnection())
                {
                    db.Open();

                    DbCommand cmd = db.CreateCommand();
                    cmd.CommandText = $"SELECT nombre, correo FROM Usuario where id = '{id}'";
                    DbDataReader result = cmd.ExecuteReader();

                    if(!result.HasRows)
                    {
                        db.Close();
                        return null;
                    }
                    else
                    {
                        try
                        {
                            result.Read();
                            return new Usuario(id, result.GetString("nombre"), result.GetString("correo"), "");
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Error en la base de datos, GetUserById");
                        }
                        finally
                        {
                            db.Close();
                        }
                    }
                }
            }
        }

        public static bool AddUser(Usuario user)
        {
            using (SQLiteDBContext context = new SQLiteDBContext())
            {
                context.Database.EnsureCreated();
                using (DbConnection db = context.GetConnection())
                {
                    db.Open();

                    DbCommand cmd = db.CreateCommand();
                    cmd.CommandText = $"INSERT INTO Usuario (nombre, correo, password) VALUES ('{user.Nombre}', '{user.Correo}', '{user.contraseña}')";
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        //todo bien
                        db.Close();
                        return true;
                    }
                    else
                    {
                        db.Close();
                        return false;
                    }
                }
            }
        }

        #endregion

        #region Tareas

        public static List<Tarea> GetTask()
        {
            using (SQLiteDBContext context = new SQLiteDBContext())
            {
                context.Database.EnsureCreated();
                using (DbConnection db = context.GetConnection())
                {
                    db.Open();

                    DbCommand cmd = db.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Tarea";
                    DbDataReader result = cmd.ExecuteReader();

                    List<Tarea> taksList = new List<Tarea>();

                    while (result.Read())
                    {
                        taksList.Add(new Tarea(
                            result.GetInt32(0),
                            result.GetInt32(1),
                            result.GetString(2),
                            result.GetDateTime(3),
                            result.GetDateTime(4)));
                    }

                    db.Close();

                    return taksList;
                }
            }
        }

        public static List<Tarea> GetTaskByUserId(int user_id)
        {
            using (SQLiteDBContext context = new SQLiteDBContext())
            {
                context.Database.EnsureCreated();
                using (DbConnection db = context.GetConnection())
                {
                    db.Open();

                    DbCommand cmd = db.CreateCommand();
                    cmd.CommandText = $"SELECT id, descripcion, dataInicio, dataFin FROM Tarea where idUsuario = '{user_id}'";
                    DbDataReader result = cmd.ExecuteReader();

                    List<Tarea> taskList = new List<Tarea>();
                    while (result.Read())
                    {
                        taskList.Add(new Tarea(
                            result.GetInt32(0),
                            user_id,
                            result.GetString(1),
                            result.GetDateTime(2),
                            result.GetDateTime(3)));
                    }
                    db.Close();

                    return taskList;
                }
            }
        }

        public static bool AddTask(Tarea tarea)
        {
            using (SQLiteDBContext context = new SQLiteDBContext())
            {
                context.Database.EnsureCreated();
                using (DbConnection db = context.GetConnection())
                {
                    db.Open();

                    DbCommand cmd = db.CreateCommand();
                    cmd.CommandText = $"INSERT INTO Tarea (idUsuario, descripcion, dataInicio, dataFin) " +
                        $"VALUES ('{tarea.IdUsuario}', '" +
                        $"{tarea.Descripcion}', " +
                        $"'{tarea.DataInicio.Year}-{tarea.DataInicio.Month}-{tarea.DataInicio.Day}', " +
                        $"'{tarea.DataFin.Year}-{tarea.DataFin.Month}-{tarea.DataFin.Day}')";
                    
                    if(cmd.ExecuteNonQuery() == 1)
                    {
                        db.Close();
                        return true;
                    }
                    else
                    {
                        db.Close();
                        return false;
                    }
                }
            }
        }
        #endregion

        /*
         Plantilla para hacer consultas a la base de datos
         
        using (SQLiteDBContext context = new SQLiteDBContext())
        {
            context.Database.EnsureCreated();
            using (DbConnection db = context.GetConnection())
            {
                db.Open();
                        
                DbCommand cmd = db.CreateCommand();

                db.Close();

                return null;
            }
        }
         */
    }
}