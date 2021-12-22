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
	Content nvarchar(MAX),
	CreationDate datetime NOT NULL
);
GO

CREATE TABLE Label (
    FieldName nvarchar(25) NOT NULL,
    [Version] float NOT NULL,
    [Text] nvarchar(4000) NOT NULL,
    IsDeleted bit DEFAULT 0
);

CREATE TABLE Subscriber (
    SubscriberId int PRIMARY KEY IDENTITY,
    Name nvarchar(100) NULL,
    Surname nvarchar(100) NULL
);

GO
DELETE ProfilInvestisseur;
GO
GO
DELETE Label;
GO
GO
DELETE Subscriber;
GO

ALTER TABLE Label
   ADD CONSTRAINT PK_Labels_FieldName_Version PRIMARY KEY CLUSTERED (FieldName, [Version]);

ALTER TABLE ProfilInvestisseur
    ADD CONSTRAINT FK_ProfilInvestisseur_Subscriber
	FOREIGN KEY  (SubscriberId) REFERENCES Subscriber(SubscriberId);

INSERT INTO Label (FieldName, [Version], [Text]) VALUES 
('vcOccupation', '1.0','Quel est votre occupation principale?'), 
('vcEmployeur', '1.0','Quel est le nom de votre employeur actuel?'),
('dDateEmbauche', '1.0','Date de début d''emploi'),
('dDateEmbauche', '1.1','Date de début d''emploi avec cet employeur'),
('DisruptiveEvent', '1.0','Dans le futur, est-ce que l’un ou plusieurs des événements suivants pourraient ' + 
    'affecter votre situation actuelle en tant qu’investisseur ou pourraient faire en sorte que vous deviez ' + 
    'retirer votre investissement plus tôt que prévu?')

GO
INSERT INTO Subscriber ([Name], Surname) VALUES 
('Yvan', 'Mongeau'), 
('Catherine', 'Laparée')