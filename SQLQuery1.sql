Create database Gestion_de_stock
go
create table produits (
produit varchar(50) primary key not null,
quantite int not null,
pu float not null,
NU_vendues int );
go


create table fournisseurs (
nom_fourn varchar(50) not null,
produit varchar(50) not null,
qte int not null,
foreign key (produit) references produits (produit),
date Date not null);
go

create table commandes (
n_cmd int primary key not null,
produit varchar(50) not null,
qte int not null,
etat varchar(50) not null,
date Date not null,
foreign key (produit) references produits (produit));
go

create table moderateur (
email varchar(50)  not null,
mdp varchar(50) not null);
go

insert into moderateur (email, mdp) values (
'admin@admin.com', 'admin');
go

INSERT INTO Produits (Produit, Quantite, PU)
VALUES 
('iPhone 13', 2, 1099),
('MacBook Air', 1, 1299),
('Samsung Galaxy S22', 3, 899),
('Dell Inspiron 15', 2, 749),
('Google Pixel 7', 1, 799),
('Lenovo Yoga Slim 7', 2, 999);
go

insert into commandes (n_cmd, produit, qte, etat, date) values 
(1, 'iPhone 13', 2,'Livré', '2024-07-21'),
(2, 'MacBook Air', 1, 'Livré', '2024-08-15'),
(3, 'Samsung Galaxy S22', 3, 'Livré', '2024-09-01'),
(4, 'Dell Inspiron 15', 2, 'Livré', '2024-07-28'),
(5, 'MacBook Air', 1, 'Livré', '2024-08-15'),
(6, 'Lenovo Yoga Slim 7', 2, 'Livré', '2024-07-10');
go


CREATE PROCEDURE table_produits 
AS
BEGIN
    SELECT 
        p.produit,
        p.quantite,
        p.pu,
        ISNULL(SUM(c.qte), 0) as 'NU_vendues'
    FROM Gestion_de_stock.dbo.produits p
    LEFT JOIN Gestion_de_stock.dbo.commandes c ON p.produit = c.produit 
    GROUP BY p.produit, p.quantite, p.pu
END
GO


CREATE PROCEDURE table_fournisseurs 
AS
BEGIN
    SELECT *
    FROM Gestion_de_stock.dbo.fournisseurs
END
GO
exec table_produits
