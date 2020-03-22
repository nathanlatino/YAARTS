# YAARTS

* [Wiki](https://github.com/nathanlatino/YAARTS/wiki)

Pour faciliter le développement et depuis que Unity propose la version pro gratuitement aux étudiants, il a été fait le choix d'utiliser Unity Collab: le git intégré de la version pro Unity. Pour faciliter la vie des professeurs, une copie du dossier "Scripts" du projet sera push chaque semaine sur ce repo.

## Développeurs

* Nathan Latino
* Tristan Seuret
* Sol Rosca

## Sujet

YAARTS: Yet Again Another Real Time Strategy (Game). Ce projet est une tentative d'aller le plus loin possible dans le développement d'un jeu de stratégie en temps réel (Dune 2, Command & Conqueres, Warcraft, Starcraft, ...). Le style visuel visé est Lowpoly/Cartoon.

Le projet est partagé entre les cours d'infographie et de .NET. La partie infographie se concentre sur la partie visuelle ainsi que l'utilisation du moteur Uity 3D et la partie .NET sur la qualité du code C#.

## Base de travail

Nathan Latino et Sol Rosca on fait une première tentative d'implémentation d'un RTS (YARTS) en 2e année en utilisant Java et Libgdx. Bien que YARTS (avec un "A") était entièrement 2D en vue topdown, une base de réflexion concernant les points chauds existe déjà. Le rapport de ce projet est accèssible [ici](https://github.com/nathanlatino/yarts/blob/master/doc/Rapport-YARTS.pdf).

LibGDX n'étant pas un moteur de jeu mais un framework, les philosophies de ces deux technologies ne sont pas comparables. LibGDX ne comporte aucune interface et est relativement bas niveau, particulièrement bas niveau en comparaison avec Unity. Dans ces circonstances il est ardu de pouvoir spécifier ce qui vient directement de ce premier projet autrement que le nom ainsi qu'une première expérience dans le développement d'un jeu video.

Depuis l'introduction à Unity, de nombreux petits prototypes de mécanismes ont été mis en place pour servir de base concrète lors de la familiarisation avec Unity. Le projet actuel utilise les connaissances acquises lors de ces expérimentations mais reppart de zero. En effet, les premiers prototypes n'ont pas été pensés pour pouvoir s'intégrer dans une architecture complète et les reprendre en tant que tel aurait posé de nombreux problèmes.

## Technologies

* Le projet utilise le nouvel Universal Render Pipline (URP). Il permet une utilisation plus flexible et aisée du postprocessing ainsi qu'un gain de performance dans de nombreuses situations.

* La collaboration se fait avec Unity Collab, service offert dans la version pro d'unity (gratuite depuis le mois de février pour les étudiants).

## Assets utilisés

Le fait est que nous aimons Unity et nous avons investi dans certains tools et packs d'assets.

### Tools

#### Free
* Debug Drawing
* LiteFPSCounter
* Icon Maker
* ProBuilder
* ProGrid
* PolyBrush

#### Payed
* Peak
* Console Pro
* QHierarchy
* Rainbow Folders

### Assets

#### Payed
* ToonyTinyPeople
* Tarbo-FantasyVillage
* Polygonal Arsenal



## Ressources utilisées

* Unity learn
* **Youtube:**
    * [Code monkey](https://www.youtube.com/channel/UCFK6NCbuCIVzA6Yj1G_ZqCg)
    * [Brackeys](https://www.youtube.com/user/Brackeys)
    * [Sebastian Lague](https://www.youtube.com/channel/UCmtyQOKKmrMVaKuRXz02jbQ)
    * [EngiGames](https://www.youtube.com/channel/UCbAsfBmEHQpPERAVx8DHxZA)
    * À compléter
* **Architecture:**
    * [Game Programming Patterns (livre en ligne)](http://gameprogrammingpatterns.com/)
    * [Design patterns in Unity](https://www.patrykgalach.com/2019/05/06/design-patterns-in-unity/)
    * [Refactoring guru](https://refactoring.guru/)
    * [ECS Deep dive](https://rams3s.github.io/blog/2019-01-09-ecs-deep-dive/)
    * [Messaging architecture](https://medium.com/@tkomarnicki/messaging-architecture-in-unity-6e6409bdda02)
    * [Message bus pattern](https://github.com/franciscotufro/message-bus-pattern)
