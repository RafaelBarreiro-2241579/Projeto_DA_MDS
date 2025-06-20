﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.models
{
    public enum TipoUtilizador
    {
        Gestor,
        Programador
    }


    class Utilizador
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public TipoUtilizador TipoUtilizador { get; set; }
  
    public Utilizador()
        {
        }
        public Utilizador(string nome, string username, string password, TipoUtilizador tipoUtilizador)
        {
            Nome = nome;
            Username = username;
            Password = password;
            TipoUtilizador = tipoUtilizador;
        }
    }
}
