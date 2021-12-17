USE master
GO
DROP DATABASE IF EXISTS ProfilsInverstisseurPOC
GO
CREATE DATABASE ProfilsInverstisseurPOC
GO
USE ProfilsInverstisseurPOC
GO
CREATE TABLE ProfilInvestisseur (
    ProfilInvestisseurId int PRIMARY KEY IDENTITY,
    SubscriberId int NOT NULL,
	Content nvarchar(MAX)
);
GO

CREATE TABLE Label (
    FieldName nvarchar(25) NOT NULL,
    [Version] float NOT NULL,
    [Text] nvarchar(4000) NOT NULL
);

CREATE TABLE Subscriber (
    SubscriberId int PRIMARY KEY IDENTITY,
    Name nvarchar(100) NULL,
    Surname nvarchar(100)
);

ALTER TABLE Label
   ADD CONSTRAINT PK_Labels_FieldName_Version PRIMARY KEY CLUSTERED (FieldName, [Version]);

GO
DELETE ProfilInvestisseur;
GO
GO
DELETE Label;
GO
GO
DELETE Subscriber;
GO
INSERT INTO Label (FieldName, [Version], [Text]) VALUES 
('vcOccupation', '1.0','Quel est votre occupation principale?'), 
('vcEmployeur', '1.0','Quel est le nom de votre employeur actuel?'),
('dDateEmbauche', '1.0','Date de début d''emploi'),
('dDateEmbauche', '1.1','Date de début d''emploi avec cet employeur')

GO
INSERT INTO Subscriber ([Name], Surname) VALUES 
('Yvan', 'Mongeau'), 
('Catherine', 'Laparée')