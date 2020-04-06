-- Insertion de quelques infos dans les tables


use cook; 

-- insertion dans utilisateur
INSERT INTO `cook`.`utilisateur` (`email`,`password`) VALUES ('jj@monmail.fr.com', '1234');
INSERT INTO `cook`.`utilisateur` (`email`,`password`) VALUES ('gg@monmail.fr.com', '0000');



-- insertion dans clients
INSERT INTO `cook`.`client` (`nom`,`balance`,`adresse`,`numero`,`emailUtilisateur`) VALUES ('Juniot',0,'46 rue du pigeon Paris', '07456321','jj@monmail.fr.com');
INSERT INTO `cook`.`client` (`nom`,`balance`,`adresse`,`numero`,`emailUtilisateur`) VALUES ('Gerard',0,'45 rue de la mouette Nice', '07987456','gg@monmail.fr.com');


-- insertion dans cdr
INSERT INTO `cook`.`cdr` (`numero`) VALUES ('07456321');
INSERT INTO `cook`.`cdr` (`numero`) VALUES ('07987456');


-- insertion dans fournisseur
INSERT INTO `cook`.`fournisseur` (`nom`,`numero`) VALUES ('Tricatel',111);
INSERT INTO `cook`.`fournisseur` (`nom`,`numero`) VALUES ('BioCBon',222);
INSERT INTO `cook`.`fournisseur` (`nom`,`numero`) VALUES ('100% Pole Nord',333);


-- insertion dans produit
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('201','tomate','legume',0,0,0,'g',222);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('101','poulet','viande',0,0,0,'g',111);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('102','spaghettis','feculant',0,0,0,'g',111);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('301','glace','eau gelee',0,0,0,'cL',333);


-- insertion dans compose
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('1',300,'101','Bolognaise au poulet');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('2',150,'101','Bolognaise au poulet');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('3',500,'102','Bolognaise au poulet');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('4',320,'301','Bouteille du Nord');


-- insertion dans recette
INSERT INTO `cook`.`recette` (`nom`,`categorie`,`description`,`prix`,`numeroCreateur`) VALUES ('Bolognaise au poulet','plat','Bolagnaise classique avec un arriere gout de petrole',3,'07456321');
INSERT INTO `cook`.`recette` (`nom`,`categorie`,`description`,`prix`,`numeroCreateur`) VALUES ('Bouteille du Nord','boisson','Un verre d eau bien fraiche venant des icebergs du pole nord',6,'07456321');


-- insertion dans fournisseur
INSERT INTO `cook`.`fournisseur` (`id`,`quantite_produit`,`refProduit`) VALUES ('1',3,'201');
INSERT INTO `cook`.`fournisseur` (`id`,`quantite_produit`,`refProduit`) VALUES ('2',2,'101');
INSERT INTO `cook`.`fournisseur` (`id`,`quantite_produit`,`refProduit`) VALUES ('3',1,'102');
