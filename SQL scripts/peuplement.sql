-- Insertion de quelques infos dans les tables


use cook; 


-- insertion dans clients
INSERT INTO `cook`.`client` (`nom`,`balance`,`adresse`,`numero`,`email`,`password`) VALUES ('Juniot',0,'46 rue du pigeon Paris', '07456321','jj@monmail.fr.com','1234');
INSERT INTO `cook`.`client` (`nom`,`balance`,`adresse`,`numero`,`email`,`password`) VALUES ('Robert',0,'22 rue de la mouette Nice', '07987456','gg@monmail.fr.com','0000');
INSERT INTO `cook`.`client` (`nom`,`balance`,`adresse`,`numero`,`email`,`password`) VALUES ('Cousteau',0,'24 rue du rouge gorge Marseille', '07951487','cuistot@lacuisine.fr','gateau');
INSERT INTO `cook`.`client` (`nom`,`balance`,`adresse`,`numero`,`email`,`password`) VALUES ('Justine',0,'15 rue de l hirondelle Bordeaux', '07852147','just@yah.fr','0000');
INSERT INTO `cook`.`client` (`nom`,`balance`,`adresse`,`numero`,`email`,`password`) VALUES ('Martin',0,'24 rue de l aigle Marseille', '07951263','mart@mars.fr','gateau');
INSERT INTO `cook`.`client` (`nom`,`balance`,`adresse`,`numero`,`email`,`password`) VALUES ('Donald',0,'9 rue du colibri Fort-de-France', '07852654','don@ld.fr','gateau');
INSERT INTO `cook`.`client` (`nom`,`balance`,`adresse`,`numero`,`email`,`password`) VALUES ('Julie',0,'2 rue de la poule Strasbourg', '07741654','jul@i.fr','gateau');
-- admin
INSERT INTO cook.client (nom,balance,adresse,numero,email,password, type) VALUES ('admin',0,'admin', 'admin','admin','admin', 'admin');


-- insertion dans cdr
INSERT INTO `cook`.`cdr` (`numero`) VALUES ('07456321');
INSERT INTO `cook`.`cdr` (`numero`) VALUES ('07987456');
INSERT INTO `cook`.`cdr` (`numero`) VALUES ('07951487');


-- insertion dans fournisseur
INSERT INTO `cook`.`fournisseur` (`nom`,`numero`) VALUES ('Tricatel',111);
INSERT INTO `cook`.`fournisseur` (`nom`,`numero`) VALUES ('BioCBon',222);
INSERT INTO `cook`.`fournisseur` (`nom`,`numero`) VALUES ('100% Pole Nord',333);
INSERT INTO `cook`.`fournisseur` (`nom`,`numero`) VALUES ('Ferme d Alex',444);


-- insertion dans produit
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('101','poulet Tri','viande',0,0,0,'g',111);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('102','spaghettis Tric','feculent',0,0,0,'g',111);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('104','poisson Tric','viande',0,0,0,'g',111);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('103','poudre multicolor Tric','magique',0,0,0,'g',111);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('201','tomate Bio','legume',0,0,0,'g',222);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('202','chocolat Bio','sucre',0,0,0,'g',222);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('203','banane Bio','fruit',0,0,0,'g',222);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('204','semoule Bio','feculent',0,0,0,'g',222);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('205','courgette Bio','legume',0,0,0,'g',222);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('206','pomme Bio','fruit',0,0,0,'g',222);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('207','oeuf Bio','viande',0,0,0,'unitee',222);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('301','glace','eauGlace',0,0,0,'cL',333);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('401','farine','feculent',0,0,0,'g',444);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('402','lait','liquide',0,0,0,'cL',444);
INSERT INTO `cook`.`produit` (`ref`,`nom`,`categorie`,`quantite_actuelle`,`quantite_min`,`quantite_max`,`unite`,`numeroFournisseur`) VALUES ('403','brocoli','legume',0,0,0,'g',444);


-- insertion dans recette
INSERT INTO `cook`.`recette` (`nom`,`categorie`,`description`,`prix`,`numeroCreateur`,   `estHealthy`, `estBio` , `estVegan`,   `estChimique` ,  `estTendance`,`note`) VALUES ('Bolognaise au poulet','plat','Bolagnaise classique avec un arriere gout de petrole',3,'07456321',false,false,false,true,true,3);
INSERT INTO `cook`.`recette` (`nom`,`categorie`,`description`,`prix`,`numeroCreateur`,   `estHealthy`, `estBio` , `estVegan`,   `estChimique` ,  `estTendance`,`note` ) VALUES ('Bouteille du Nord','boisson','Un verre d eau bien fraiche venant des icebergs du pole nord',6,'07456321',true,true,true,true,false,4);
INSERT INTO `cook`.`recette` (`nom`,`categorie`,`description`,`prix`,`numeroCreateur`,   `estHealthy`, `estBio` , `estVegan`,   `estChimique` ,  `estTendance`,`note` ) VALUES ('Couscous','plat','Couscous authentique mais moderne',15,'07951487',true,true,false,false,true,5);
INSERT INTO `cook`.`recette` (`nom`,`categorie`,`description`,`prix`,`numeroCreateur`,   `estHealthy`, `estBio` , `estVegan`,   `estChimique` ,  `estTendance`,`note` ) VALUES ('Banane au chocolat','dessert','Dessert 100 poucent bio, vegan, sans glucides, antioxydants, avec vitamines, avec fibres, ....',8,'07951487',true,true,true,false,true,4);
INSERT INTO `cook`.`recette` (`nom`,`categorie`,`description`,`prix`,`numeroCreateur`,   `estHealthy`, `estBio` , `estVegan`,   `estChimique` ,  `estTendance`,`note` ) VALUES ('Soda T','boisson','Boisson petillante qui brille dans le noir',5,'07456321',false,false,true,true,true,9);
INSERT INTO `cook`.`recette` (`nom`,`categorie`,`description`,`prix`,`numeroCreateur`,   `estHealthy`, `estBio` , `estVegan`,   `estChimique` ,  `estTendance`,`note` ) VALUES ('Cookies','dessert','Dessert classique mais bon',15,'07951487',true,true,false,false,true,5);
INSERT INTO `cook`.`recette` (`nom`,`categorie`,`description`,`prix`,`numeroCreateur`,   `estHealthy`, `estBio` , `estVegan`,   `estChimique` ,  `estTendance`,`note` ) VALUES ('Pizza au courgettes','plat','Pizza un peu differente',20,'07951487',true,true,false,false,false,9);
INSERT INTO `cook`.`recette` (`nom`,`categorie`,`description`,`prix`,`numeroCreateur`,   `estHealthy`, `estBio` , `estVegan`,   `estChimique` ,  `estTendance`,`note` ) VALUES ('Salade de tomate','plat','Il y a beaucoup de tomates et pas grand chose d autre',17,'07456321',true,true,true,false,false,9);
INSERT INTO `cook`.`recette` (`nom`,`categorie`,`description`,`prix`,`numeroCreateur`,   `estHealthy`, `estBio` , `estVegan`,   `estChimique` ,  `estTendance`,`note` ) VALUES ('Pain','complement','Ca va bien avec tout',5,'07987456',false,false,true,false,false,9);
INSERT INTO `cook`.`recette` (`nom`,`categorie`,`description`,`prix`,`numeroCreateur`,   `estHealthy`, `estBio` , `estVegan`,   `estChimique` ,  `estTendance`,`note` ) VALUES ('Quiche au poisson','plat','Une quiche pas tres conventionel',36,'07951487',true,true,false,false,false,9);
INSERT INTO `cook`.`recette` (`nom`,`categorie`,`description`,`prix`,`numeroCreateur`,   `estHealthy`, `estBio` , `estVegan`,   `estChimique` ,  `estTendance`,`note` ) VALUES ('Un brocoli','plat','C est tout.',10,'07951487',true,true,true,false,false,9);


-- insertion dans compose
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('1' ,300,'101','Bolognaise au poulet');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('2' ,150,'201','Bolognaise au poulet');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('3' ,500,'102','Bolognaise au poulet');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('4' ,320,'301','Bouteille du Nord');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('5' ,300,'201','Couscous');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('6' ,400,'204','Couscous');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('7' ,400,'202','Banane au chocolat');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('8' ,400,'203','Banane au chocolat');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('9' ,200,'401','Cookies');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('10' ,100,'202','Cookies');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('11' ,2,  '207','Cookies');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('12',4,  '206','Pizza au courgettes');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('13',150,'205','Pizza au courgettes');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('14',170,'401','Pizza au courgettes');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('15',320,'201','Salade de tomate');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('16',160,'401','Pain');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('17',300,'401','Quiche au poisson');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('18',100,'402','Quiche au poisson');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('19',3,  '207','Quiche au poisson');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('20',150,'104','Quiche au poisson');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('21',150,'103','Soda T');
INSERT INTO `cook`.`compose` (`id`,`quantite_produit`,`refProduit`,`nomRecette`) VALUES ('22',120,'403','Un brocoli');


-- insertion commande
INSERT INTO `cook`.`commande` (`id`,`date`,`numero`,`nomRecette`) VALUES ('1','2020-04-10 00:00:00','07741654','Pizza au courgettes');
INSERT INTO `cook`.`commande` (`id`,`date`,`numero`,`nomRecette`) VALUES ('2','2020-04-08 00:00:00','07741654','Pain');
INSERT INTO `cook`.`commande` (`id`,`date`,`numero`,`nomRecette`) VALUES ('3','2020-04-06 00:00:00','07852654','Bolognaise au poulet');
INSERT INTO `cook`.`commande` (`id`,`date`,`numero`,`nomRecette`) VALUES ('4','2020-04-06 00:00:00','07852654','Soda T');
INSERT INTO `cook`.`commande` (`id`,`date`,`numero`,`nomRecette`) VALUES ('5','2020-04-07 00:00:00','07852654','Cookies');
INSERT INTO `cook`.`commande` (`id`,`date`,`numero`,`nomRecette`) VALUES ('6','2020-04-06 00:00:00','07951263','Couscous');
INSERT INTO `cook`.`commande` (`id`,`date`,`numero`,`nomRecette`) VALUES ('7','2020-04-08 00:00:00','07951263','Banane au chocolat');
INSERT INTO `cook`.`commande` (`id`,`date`,`numero`,`nomRecette`) VALUES ('8','2020-04-09 00:00:00','07852147','Bouteille du Nord');
INSERT INTO `cook`.`commande` (`id`,`date`,`numero`,`nomRecette`) VALUES ('9','2020-04-06 00:00:00','07852147','Pain');
INSERT INTO `cook`.`commande` (`id`,`date`,`numero`,`nomRecette`) VALUES ('10','2020-04-08 00:00:00','07852147','Salade de tomate');
INSERT INTO `cook`.`commande` (`id`,`date`,`numero`,`nomRecette`) VALUES ('11','2020-04-05 00:00:00','07456321','Soda T');
INSERT INTO `cook`.`commande` (`id`,`date`,`numero`,`nomRecette`) VALUES ('12','2020-04-06 00:00:00','07456321','Quiche au poisson');
INSERT INTO `cook`.`commande` (`id`,`date`,`numero`,`nomRecette`) VALUES ('13','2020-04-01 00:00:00','07987456','Quiche au poisson');
INSERT INTO `cook`.`commande` (`id`,`date`,`numero`,`nomRecette`) VALUES ('14','2020-04-04 00:00:00','07987456','Quiche au poisson');
INSERT INTO `cook`.`commande` (`id`,`date`,`numero`,`nomRecette`) VALUES ('15','2020-04-02 00:00:00','07987456','Pizza au courgettes');