CREATE TABLE tabCategoria(
			cID INT PRIMARY KEY IDENTITY (1,1),
			cCategoria VARCHAR(20) NOT NULL,
			cAtivo BIT NOT NULL)

CREATE TABLE tabTurma (
			tID INT PRIMARY KEY IDENTITY(1,1),
			tNome VARCHAR(20) NOT NULL,
			tAtivo BIT NOT NULL)

CREATE TABLE tabAluno(
			aID int PRIMARY KEY IDENTITY(1,1),
			aNome VARCHAR(100) NOT NULL , 
			aEmail VARCHAR(100) UNIQUE,
			tID INT NOT  NULL,
			cID INT NULL,
			aImagem varbinary(max))

ALTER TABLE tabAluno
   ADD CONSTRAINT FK_tabTurma FOREIGN KEY (tID)
   REFERENCES tabTurma (tID)

ALTER TABLE tabAluno
   ADD CONSTRAINT FK_tabCategoria FOREIGN KEY (cID)
   REFERENCES tabCategoria(cID)


INSERT INTO	dbo.tabTurma VALUES ( 
    'Turma 1',1),
	('Turma 2',1),
	('Turma 3', 1),
	('Turma 4',1),
	('Turma 5',1 ),
	('Turma 6' , 0),
	('Turma 7',1)

   INSERT INTO dbo.tabCategoria VALUES (
      'TI', 1),('ADM',1),('Infra',0)

INSERT INTO	dbo.tabAluno VALUES( 
    'Rafael',   -- aNome - varchar(100)
    'rafaelfavoreto@hotmail.com', -- aEmail - varchar(100)
     1,    -- tID - int
     1, -- cID - int
     NULL  -- aImagem - varbinary(max)
    )
	

SELECT aID identificao, aNome Nome,cCategoria Curso,tNome Turma FROM tabAluno
INNER JOIN dbo.tabCategoria ON tabCategoria.cID = tabAluno.cID AND cAtivo = 1
INNER JOIN dbo.tabTurma ON tabTurma.tID = tabAluno.tID AND tAtivo = 1


