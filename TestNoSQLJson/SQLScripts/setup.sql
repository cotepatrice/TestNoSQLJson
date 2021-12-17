USE master
GO
DROP DATABASE IF EXISTS ProfilsInverstisseurPOC
GO
CREATE DATABASE ProfilsInverstisseurPOC
GO
USE ProfilsInverstisseurPOC
GO
CREATE TABLE ProfilsInvestisseur (
    ProfilsInvestisseurId int PRIMARY KEY IDENTITY,
	Content nvarchar(MAX)
);
GO

CREATE TABLE Labels (
    FieldName nvarchar(25) NOT NULL,
    [Version] float NOT NULL,
    [Text] nvarchar(4000) NOT NULL
);

ALTER TABLE Labels
   ADD CONSTRAINT PK_Labels_FieldName_Version PRIMARY KEY CLUSTERED (FieldName, [Version]);

GO
DELETE ProfilsInvestisseur;
GO
GO
DELETE Labels;
GO
INSERT INTO Labels (FieldName, [Version], [Text]) VALUES 
('vcOccupation', '1.0','Quel est votre occupation principale?'), 
('vcEmployeur', '1.0','Quel est le nom de votre employeur actuel?'),
('dDateEmbauche', '1.0','Date de début d''emploi')


