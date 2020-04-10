-- Creation des tables pour le projet

-- CREATE DATABASE cook;

use cook; 

CREATE TABLE cook.client (
  `nom` VARCHAR(20) NOT NULL,
  `balance` INT NULL,
  `adresse` VARCHAR(50) NULL,
  `numero` VARCHAR(20) not NULL,
  `email` varchar(20) not null,
  `password` varchar(20) not null,
  `type` varchar(20) DEFAULT 'user', 
  PRIMARY KEY (`numero`) );
  
    
create table cook.cdr(	
	`numero` VARCHAR(20) not NULL,
    primary key(`numero`),
	foreign key(`numero`) references cook.client(`numero`));
    
create table cook.fournisseur(
	`numero` varchar(20) not null,
    `nom` varchar(20) not null,
    primary key(`numero`));
    
create table cook.produit(
	`ref` varchar(20) not null,
	`nom` varchar(30) not null,
    `categorie` varchar(20) not null,
    `quantite_actuelle`int null,
    `quantite_min` int null,
    `quantite_max` int null,
    `unite` varchar(10) not null,
    `numeroFournisseur` varchar(20) not null,
    primary key (`ref`),
    foreign key(`numeroFournisseur`) references cook.fournisseur(`numero`));    
    

create table cook.recette(
    `nom` varchar(20) not null,
    `categorie` varchar(20) not null,
    `description` varchar(250) not null,
    `prix` int null,
    `numeroCreateur` varchar(20) not null,
    `estHealthy` boolean DEFAULT false,
    `estBio` boolean DEFAULT false,
    `estVegan` boolean DEFAULT false,
    `estChimique` boolean DEFAULT false,
    `estTendance` boolean DEFAULT false,
    `note` int DEFAULT 0,
    primary key(`nom`),
    foreign key (`numeroCreateur`) references cook.cdr(`numero`));
    
create table cook.compose(
	`id` varchar(40) not null,  
    `quantite_produit`int not null,    
    `refProduit` varchar(20) not null,
    `nomRecette` varchar(20) not null,
    primary key(`id`),	
    foreign key(`nomRecette`) references cook.recette(`nom`),
    foreign key(`refProduit`) references cook.produit(`ref`));
    
create table cook.commande(
	`id` varchar(40) not null,
	`date` datetime,
	`numero` varchar(20) not null,
    `nomRecette` varchar(20) not null,
    primary key (`id`),
    foreign key (`nomRecette`) references cook.recette(`nom`),
    foreign key (`numero`) references cook.client(`numero`));
    


    