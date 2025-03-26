CREATE TABLE Contato (
    Id INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    NomeCompleto VARCHAR(1024) NULL,
    DDD INT NULL,
    Telefone INT NULL,
    Email VARCHAR(512) NULL
);

INSERT INTO Contato (NomeCompleto, DDD, Telefone, Email) VALUES ('Felipe Bolivar', 47, 992356623, 'felipe@email.com'); 