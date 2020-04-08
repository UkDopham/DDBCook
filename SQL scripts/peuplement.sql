-- Insertion de quelques infos dans les tables


use cook; 


-- insertion dans clients
INSERT INTO `cook`.`client` (`nom`,`balance`,`adresse`,`numero`,`email`,`password`) VALUES ('Juniot',0,'46 rue du pigeon Paris', '07456321','jj@monmail.fr.com','1234');
INSERT INTO `cook`.`client` (`nom`,`balance`,`adresse`,`numero`,`email`,`password`) VALUES ('Robert',0,'22 rue de la mouette Nice', '07987456','gg@monmail.fr.com','0000');
INSERT INTO `cook`.`client` (`nom`,`balance`,`adresse`,`numero`,`email`,`password`) VALUES ('Cousteau',0,'24 rue du rouge gorge Marseille', '07951487','gg@monmail.fr.com','0000');


-- insertion dans cdr
INSERT INTO `cook`.`cdr` (`numero`) VALUES ('07456321');
INSERT INTO `cook`.`cdr` (`numero`) VALUES ('07987456');
INSERT INTO `cook`.`cdr` (`numero`) VALUES ('07951487');


-- insertion dans fournisseur
INSERT INTO `cook`.`fournisseur` (`nom`,`numero`) VALUES ('Tricatel',111);
INSERT INTO `cook`.`fournisseur` (`nom`,`numero`) VALUES ('BioCBon',222);
INSERT INTO `cook`.`fournisseur` (`nom`,`numero`) VALUES ('100% Pole Nord',333);


-- insertion dans produit
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('201','tomate','legume',0,0,0,'g',222);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('101','poulet','viande',0,0,0,'g',111);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('102','spaghettis','feculent',0,0,0,'g',111);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('301','glace','eauGlace',0,0,0,'cL',333);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('202','chocolat','feculent',0,0,0,'g',222);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('203','banane','fruit',0,0,0,'g',222);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('204','semoule','feculent',0,0,0,'g',222);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('103','poudre multicolor','magic',0,0,0,'g',111);


-- insertion dans recette
INSERT INTO `cook`.`recette` (`nom`,`categorie`,`description`,`prix`,`numeroCreateur`,   `estHealthy`, `estBio` , `estVegan`,   `estChimique` ,  `estTendance`,`note`) VALUES ('Bolognaise au poulet','plat','Bolagnaise classique avec un arriere gout de petrole',3,'07456321',false,false,false,true,true,3);
INSERT INTO `cook`.`recette` (`nom`,`categorie`,`description`,`prix`,`numeroCreateur`,   `estHealthy`, `estBio` , `estVegan`,   `estChimique` ,  `estTendance`,`note` ) VALUES ('Bouteille du Nord','boisson','Un verre d eau bien fraiche venant des icebergs du pole nord',6,'07456321',true,true,true,true,false,4);
INSERT INTO `cook`.`recette` (`nom`,`categorie`,`description`,`prix`,`numeroCreateur`,   `estHealthy`, `estBio` , `estVegan`,   `estChimique` ,  `estTendance`,`note` ) VALUES ('Couscous','plat','Couscous authentique mais moderne',15,'07951487',true,true,false,false,true,5);
INSERT INTO `cook`.`recette` (`nom`,`categorie`,`description`,`prix`,`numeroCreateur`,   `estHealthy`, `estBio` , `estVegan`,   `estChimique` ,  `estTendance`,`note` ) VALUES ('Banane au chocolat','dessert','Dessert 100 poucent bio, vegan, sans glucides, antioxydants, avec vitamines, avec fibres, ....',8,'07951487',true,true,true,false,true,4);
INSERT INTO `cook`.`recette` (`nom`,`categorie`,`description`,`prix`,`numeroCreateur`,   `estHealthy`, `estBio` , `estVegan`,   `estChimique` ,  `estTendance`,`note` ) VALUES ('Soda T','boisson','Boisson petillante qui brille dans le noir',5,'07456321',false,false,true,true,true,9);


-- insertion dans compose
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('1',300,'101','Bolognaise au poulet');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('2',150,'101','Bolognaise au poulet');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('3',500,'102','Bolognaise au poulet');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('4',320,'301','Bouteille du Nord');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('5',300,'201','Couscous');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('6',400,'204','Couscous');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('7',320,'103','Soda T');

