using iTasks.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks.controllers
{
    internal class GerirUtilizadoresController
    {
        // Recebe se o utilizador é gestor ou programador
        public Utilizador ValidarLogin(string username, string password)
        {
            using (var db = new iTasks.) // Corrigido o namespace
            {
                return db.Utilizadores
                         .FirstOrDefault(u => u.Username == username && u.Password == password);
            }
        }
    }
}
