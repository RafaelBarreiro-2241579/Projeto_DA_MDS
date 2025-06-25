using iTasks.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.controllers
{
    class UserController
    {
        // Contexto da base de dados da aplicação
        private iTasksBD.iTasksContext db;

        // Construtor que recebe o contexto da base de dados
        public UserController(iTasksBD.iTasksContext context)
        {
            db = context;
        }

        // Adiciona automaticamente um utilizador administrador caso ainda não exista
        public void addAdmin()
        {
            // Verifica se já existe um utilizador com o username "admin"
            if (!db.Utilizadores.Any(u => u.Username == "admin"))
            {
                // Se não existir, cria um novo gestor com username e password "admin"
                Gestor admin = new Gestor("admin", "admin", "admin", Departamento.Administração, true);
                db.Utilizadores.Add(admin);
                db.SaveChanges();
            }
        }

        // Realiza o login de um utilizador com base no username e password
        public Utilizador loginUtilizador(string username, string password)
        {
            // Procura o primeiro utilizador que corresponda ao username e password fornecidos
            Utilizador user = db.Utilizadores.FirstOrDefault(u => u.Username == username && u.Password == password);
            return user;
        }
    }

}
