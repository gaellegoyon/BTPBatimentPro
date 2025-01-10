# Application de gestion des salariés et des chantiers - BTPBatimentPro

### Vidéo de démonstration

Voici une vidéo de démonstration de l'application. Vous pouvez la télécharger et la visionner ci-dessous :

![Vidéo de démonstration](./video/capture.mp4)

## Description du projet

L'application de gestion des salariés et des chantiers est une solution complète pour la gestion des équipes et des projets dans une entreprise de construction. Elle permet à l'administrateur de gérer les salariés, les chantiers et les demandes de congés, tout en permettant aux utilisateurs (salariés) de consulter leurs affectations, pointer leur présence, et soumettre des demandes de congé.

L'application se compose de deux parties principales :

1. **Backend API (ASP.NET Core Web API)** - Pour gérer les données de l'entreprise (salariés, chantiers, pointages, congés, etc.).
2. **Frontend Blazor Server** - Pour offrir une interface utilisateur interactive, permettant à l'administrateur et aux salariés d'interagir avec l'application.

## Fonctionnalités principales

### Côté API (ASP.NET Core Web API)

1. **Gestion des salariés** : Ajout, suppression et récupération des informations des salariés.
2. **Gestion des chantiers** : Création, modification, suppression et gestion des affectations des salariés à un chantier.
3. **Pointage des salariés** : Enregistrement des pointages (entrée et sortie) pour chaque salarié.
4. **Gestion des congés** : Soumission, validation ou rejet des demandes de congé.
5. **Calcul des distances** : Calcul de la distance entre l'entreprise et un chantier via une API tierce (ex: Google Maps API).

### Côté Front-End (Blazor Server)

1. **Pages pour l'administrateur** : Tableau de bord, gestion des salariés et des chantiers, validation des congés.
2. **Pages pour l'utilisateur** : Affichage des chantiers affectés, page de pointage et gestion des congés.

## Prérequis

- **.NET SDK 6.0 ou supérieur**
- **Visual Studio** ou tout autre éditeur de code supportant .NET et Blazor
- **SQL Server** pour la base de données (local ou distant)
- **API Google Maps** (optionnelle pour le calcul des distances)

## Installation et utilisation

### 1. Cloner le repository

```bash
git clone https://github.com/gaellegoyon/BTPBatimentPro.git
cd BTPBatimentPro
```

### 2. Configuration de la base de données

- Ouvrez une fenêtre de commande et naviguez vers le dossier `BTPBatimentPro.API`.
- Executez le script sql db.sql pour créer la base de données SQL

-Ensuite, exécutez la commande suivante pour appliquer la migration et mettre à jour la base de données.

cd BTPBatimentPro.API
dotnet ef migrations add InitialCreate
dotnet ef database update

### 3. Lancer l'API (Backend)

Une fois la base de données configurée, vous pouvez démarrer l'API. Pour ce faire, assurez-vous d'être dans le dossier BTPBatimentPro.API et exécutez la commande suivante :

dotnet run

### 4. Lancer l'application Blazor (Frontend)

Ouvrez une nouvelle fenêtre de commande et accédez au dossier BlazorAuth du projet :
cd BlazorAuth
dotnet run

### 5. Connexion

Pour tester l'application, vous pouvez vous connecter avec les identifiants suivants :

Utilisateur Admin :
Nom d'utilisateur : johndoe
Mot de passe : password123
Rôle : Admin
Utilisateur classique :
Nom d'utilisateur : janesmith
Mot de passe : securePass456
Rôle : User
Technicien (Utilisateur) :
Nom d'utilisateur : alicejohnson
Mot de passe : technician789
Rôle : User
Ces identifiants sont fournis pour tester les différentes fonctionnalités de l'application.

```bash

```
