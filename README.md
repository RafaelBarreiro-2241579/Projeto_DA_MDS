# Projeto_DA_MDS
Projeto de MDS e DA

pl1
Curso: Programação de Sistemas de Informação
2024/2025
Beatriz Dias-2241609
Rafael Barreiro-2241579

iTasks - Sistema de Gestão de Tarefas Kanban
===========================================

1. Descrição Geral
------------------
O iTasks é uma aplicação de gestão de tarefas baseada no método Kanban, desenvolvida em C# (.NET Framework 4.8, WinForms). Permite a criação, atribuição e acompanhamento de tarefas, com diferentes permissões para Gestores e Programadores.

2. Requisitos de Instalação
---------------------------
- Visual Studio 2022 (ou superior)
- .NET Framework 4.8
- SQL Server Express ou LocalDB (para persistência de dados)

3. Instalação e Configuração
----------------------------
a) Extraia o projeto para o seu computador.

b) Abra a solução (.sln) no Visual Studio.

c) Restaure os pacotes NuGet, se necessário.

d) Verifique a string de ligação à base de dados no ficheiro App.config.
   Exemplo de string de ligação:

   <connectionStrings>
     <add name="iTasksDB" connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=iTasksDB;Integrated Security=True;" providerName="System.Data.SqlClient" />
   </connectionStrings>

e) Compile a solução (Build > Build Solution).

f) Ao executar a aplicação pela primeira vez, a base de dados será criada automaticamente (Code First).

4. Execução da Aplicação
------------------------
a) Execute o projeto a partir do Visual Studio (F5 ou Ctrl+F5).

b) Faça login com as credenciais de um utilizador existente ou utilize o utilizador administrador criado automaticamente na primeira execução (ver UserController/addAdmin).

c) Utilize o menu para aceder às funcionalidades:
   - Gestão de Tarefas (Kanban)
   - Gestão de Utilizadores (apenas Gestores)
   - Gestão de Tipos de Tarefa (apenas Gestores)
   - Exportação de tarefas concluídas (apenas Gestores)
   - Previsão de conclusão de tarefas (apenas Gestores)

d) Apenas utilizadores do tipo Gestor têm acesso às funcionalidades administrativas e de previsão.

5. Elementos do Grupo
---------------------
- Beatriz Dias
- Rafael Barreiro / 2241579

6. Observações
--------------
- O cálculo de previsão de conclusão utiliza a média de tempo das tarefas concluídas por StoryPoints, considerando o valor mais próximo caso não haja histórico suficiente.
- Este projeto é para fins académicos e de demonstração.