use cook;
-- insert into utilisateur
insert into cook.utilisateur (email, password) values ('EmmanuelMacron@france.fr', 'manu');
-- insert into fournisseur 
insert into cook.fournisseur (numero, nom) values ('06181818', 'Danon');

-- insert into produit
insert into cook.produit (ref, nom, categorie, quantite_actuelle, quantite_min, quantite_max, unite, numeroFournisseur) values ('REDZ12', 'banane', 'fruit', 12, 1, 30, 'kg', '06181818');

-- insert into recette 
insert into cook.recette (nom, categorie, description, prix, numeroCreateur) values ('couscous' , 'plat' , 'super bon', 12, '1');

-- insert into compose
insert into cook.compose (id, quantite_produit, refProduit) values ('1', 1, 'REDZ12');

-- insert into client
insert into cook.client (nom, balance, adresse, numero, nomRecette, email) values ('liolio', 0 , 'courbevoie' , '1', 'couscous', 'EmmanuelMacron@france.fr');

-- insert into cdr 
insert into cook.cdr (numero) values ('1');



