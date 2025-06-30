# Projeto_DA_MDS
Projeto de MDS e DA


![Logo ESTG](https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQe3E41fi7Q9H_9KwPVQfwPDDEc0MMp0tsJ5A&s)



# Grupo de estudantes:

* **Turno:** PL1
* **Curso:** Programação de Sistemas de Informação
* **Grupo:** D
 * Beatriz Dias Nº2241609
 * Rafael Barreiro Nº2241579

************************

# iTask - Sistema de Gestão de Tarefas Kanban
## Descrição 
iTasks é uma aplicação de gestão de tarefas baseada no método Kanban, desenvolvida em C#. Permite a criação, atribuição e acompanhamento de tarefas, com diferentes permissões para Gestores e Programadores.


## Requisitos de Instalação
* Visual Studio 2022
* Entity Framework


## Instalação e Configuração
   1. Extraia o projeto para o seu computador.

   2. Abra a solução (.sln) no Visual Studio.

   3. Restaure os pacotes NuGet, se necessário. Se não tiver, instale o Entity Framework

   4. Verifique a string de ligação à base de dados no ficheiro App.config.
      Exemplo de string de ligação:

     ``` <connectionStrings>
      <add name="iTasksDB" connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=iTasksDB;Integrated Security=True;" providerName="System.Data.SqlClient" />
      </connectionStrings> # Projeto_DA_MDS
Projeto de MDS e DA


![Logo ESTG](https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQe3E41fi7Q9H_9KwPVQfwPDDEc0MMp0tsJ5A&s)



# Grupo de estudantes:

* **Turno:** PL1
* **Curso:** Programação de Sistemas de Informação
* **Grupo:**  
 * Beatriz Dias Nº2241609
 * Rafael Barreiro Nº2241579

************************

# iTask - Sistema de Gestão de Tarefas Kanban
## Descrição 
iTasks é uma aplicação de gestão de tarefas baseada no método Kanban, desenvolvida em C#. Permite a criação, atribuição e acompanhamento de tarefas, com diferentes permissões para Gestores e Programadores.


## Requisitos de Instalação
* Visual Studio 2022
* Entity Framework


## Instalação e Configuração
   1. Extraia o projeto para o seu computador.

   2. Abra a solução (.sln) no Visual Studio.

   3. Restaure os pacotes NuGet, se necessário. Se não tiver, instale o Entity Framework

   4. Verifique a string de ligação à base de dados no ficheiro App.config.

   5. Compile a solução (Build > Build Solution).

   6. Ao executar a aplicação pela primeira vez, a base de dados será criada automaticamente (Code First).

## Execução da Aplicação
   1. Execute o projeto a partir do Visual Studio (F5 ou Ctrl+F5).

   2. Faça login com as credenciais de um utilizador existente ou utilize o utilizador administrador criado automaticamente na primeira execução (ver UserController/addAdmin).

## Funcionalidades
Utilize o menu para aceder às funcionalidades:
   *   Gestão de Tarefas (Kanban)
   * Gestão de Utilizadores (apenas Gestores)
   * Gestão de Tipos de Tarefa (apenas Gestores)
   * Exportação de tarefas concluídas (apenas Gestores)
   * Previsão de conclusão de tarefas (apenas Gestores)

   * Apenas utilizadores do tipo Gestor têm acesso às funcionalidades administrativas e de previsão.


## Observações
   * O cálculo de previsão de conclusão utiliza a média de tempo das tarefas concluídas por StoryPoints, considerando o valor mais próximo caso não haja histórico suficiente.
   * Este projeto é para fins académicos e de demonstração.

   5. Compile a solução (Build > Build Solution).

   6. Ao executar a aplicação pela primeira vez, a base de dados será criada automaticamente (Code First).

## Execução da Aplicação
   1. Execute o projeto a partir do Visual Studio (F5 ou Ctrl+F5).

   2. Faça login com as credenciais de um utilizador existente ou utilize o utilizador administrador criado automaticamente na primeira execução (ver UserController/addAdmin).

## Funcionalidades
Utilize o menu para aceder às funcionalidades:
   *   Gestão de Tarefas (Kanban)
   * Gestão de Utilizadores (apenas Gestores)
   * Gestão de Tipos de Tarefa (apenas Gestores)
   * Exportação de tarefas concluídas (apenas Gestores)
   * Previsão de conclusão de tarefas (apenas Gestores)

   * Apenas utilizadores do tipo Gestor têm acesso às funcionalidades administrativas e de previsão.


## Observações
   * O cálculo de previsão de conclusão utiliza a média de tempo das tarefas concluídas por StoryPoints, considerando o valor mais próximo caso não haja histórico suficiente.
   * Este projeto é para fins académicos e de demonstração.