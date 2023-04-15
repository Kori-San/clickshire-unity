- ***The Lab***: *Discover new potions by mixing components.* Avoir accés à plein de composants et pouvoir les mélanger pour pouvoir obtenir des nouvelles potions ou des potions de meilleur qualité (comme dans [Little Alchemy](https://littlealchemy.com/)). Si le joueur fait une combinaison et que ça marche il débloqueras la recette. Si il rate il obtiendras une potion nulle par défauts (genre 'Potion Jus de Chaussette').

- ***Auction House***: *Buy components from merchants or make a contract with them to receive components periodically, you can also buy potions recipes.*Avoir accès à l'inventaire complet des composants et pouvoir en acheter ponctuellement, possibilité de passer des accords avec des marchands pour récolter des composants à un rythme régulier (ie. 5 Fleur de lys toutes les secondes, 3 Écailles de dragons par heures ect...). Possibilité aussi d'acheter des formules pour savoir quoi mélanger avec quoi pour obtenir quelles potions. Bonus: Faire en sorte que les composants se débloquent petit à petit en fonction de leur valeur (ie. Faire la potion niveau 1 débloque les composants niveau 2. Faire une potion de soins niveau 2 débloque les composants pour en faire une de niveau 3, etc...).

- ***Artisan's Consortium***: *Upgrade many mechanism by purchasing contracts with other artisans and many more (ie. buy the "Goblin rockets" upgrade to receive components more often).* Avoir une liste d'améliorations (ie. l'Amélioration 'Capital Risk associate' permettrer de réduire le coût des composants, l'amélioration 'Stage en Magie des portails' diviserais le temps qu'il faut pour les commandes récurrentes d'artisanat par deux, ect...) cf. Améliorations AdVenture Capitalist.

- ***??? (Pas de nom encore mais c'est une Usine)*** : *Use known recipes to launch automatic potion making and make passive income.*Utilise les recette déjà apprise ET concue pour lancer une production à la chaîne d'une ou plusieurs potions (dans la limite des stocks disponibles parmi les composants). Contrairement aux potions faîtes à la main qui doivent être vendu à l'HDV, les potions fabriqués automatiquement ici sont vendue directement mais pour un prix un peu moins eleve (90% du prix).

- ***Compendium***: *See all known potions and get tips for unknown potions.* Permettrais de voir toutes les potions disponibles, bien sur seul celles qu'ont a déjà crée ou dont on posséde la recette apparaitrais avec des informations. Les potions inconnues apparaitrais assombris avec seulement leur nom et une vague description permettant au joueur de deviner 1 ou 2 ingrédients qui compose la potion.
Exemple:
```json
{
"nom":"Potion du vol draconique de Bronze",
"description":"Une potion qui vous permettrais d'avoir temporérement les pouvoirs d'un dragon de bronze",
# SI LE JOUEUR POSSEDE LA RECETTE ET/OU L'AS DEJA FAIT
"composant":["1x écaille de dragon de bronze", "1x Hocheblume"],
"prix":"300PO",
# SINON
"composant":"???",
"prix":"???",
}
```