--Script de cria��o da tabela 

IF NOT EXISTS (SELECT 'x' FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TAREFAS' AND TABLE_SCHEMA = 'dbo')
BEGIN
    CREATE TABLE dbo.TAREFAS (
        CD_TAREFA INT IDENTITY(1,1) PRIMARY KEY,  
        NM_TAREFA VARCHAR(100) NOT NULL,          
        FL_CONCLUIDA BIT NOT NULL DEFAULT 0            
    );
END;


INSERT INTO TAREFAS (NM_TAREFA, FL_CONCLUIDA) VALUES
('LAVAR LOU�A', 0),
('LEVAR O CACHORRO PARA PASSEAR', 0),
('IR AT� O CORREIO', 1),
('LAVAR ROUPA', 0), 
('LIMPAR O TAPETE', 0),
('FAZER UM BOLO', 0); 


