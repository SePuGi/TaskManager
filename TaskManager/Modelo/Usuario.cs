using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Modelo
{
    /*
    Usuario:
        id
        nombre
        correo
        contraseña
    */
    public class Usuario
    {
        public int id;
        public string nombre;
        public string correo;
        public string contraseña;

        public Usuario(int id, string nombre, string correo, string contraseña)
        {
            Id = id;
            Nombre = nombre;
            Correo = correo;
            Contraseña = contraseña;
        }

        public Usuario() { }

        public int Id { 
            get { return id; }
            set
            { id = value; }
        }

        public string Nombre {
            get { return nombre; }
            set
            {
                if(ValidarNombre(value))
                    nombre = value;
                else
                    throw new Exception("Nombre no valido");
            
            }
        }

        public string Correo { 
            get { return correo; } 
            set
            {
                if (ValidarCorreo(value))
                    correo = value;
                else
                    throw new Exception("Correo no valido");
            }
        }

        public string Contraseña {
            get { return contraseña; } 
            set
            {
                contraseña = EncriptarContraseña(value);
            }
        } 

        /// <summary>
        /// Valida que el nombre tenga entre 3 y 50 caracteres
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ValidarNombre(string value)
        {
            if (value.Length > 3 && value.Length < 50)
                return true;
            return false;
        }

        /// <summary>
        /// Valida que el correo tenga el formato correcto
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ValidarCorreo(string value)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if (regex.IsMatch(value))
                return true;
            return false;
        }

        //encriptar contraseña
        //hash 
        public static string EncriptarContraseña(string contraseña)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(contraseña);
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

}
