# ToolsColliders

## Description

**FR:**
Ce script Unity permet de regrouper et de gérer les colliders dans une hiérarchie de GameObjects. Il offre des options de filtrage par tag et par layer, et permet de visualiser les colliders en mode wireframe ou solide.

**EN:**
This Unity script allows grouping and managing colliders within a hierarchy of GameObjects. It provides filtering options by tag and layer, and allows visualizing colliders in wireframe or solid mode.

## Important Note

**FR:**
Ce script doit être attaché à un GameObject et doit être placé dans une hiérarchie de GameObjects comme suit:

**EN:**
This script must be attached to a GameObject and placed within a hierarchy of GameObjects as follows:

----------------------------------------------------------------

Group 1 

 group 1.1 
 
==> gameobject collider 1 

==> gameobject collider 2 

==> gameobject collider 3 
      
group 1.2 

==> gameobject collider 1 
==> gameobject collider 2 
==> gameobject collider 3 

Group 2 

group 2.1 

==> gameobject collider 1 
==> gameobject collider 2 
==> gameobject collider 3 

group 2.2 

==> gameobject collider 1 
==> gameobject collider 2 
==> gameobject collider 3 

.....

----------------------------------------------------------------


Sinon, le script ne fonctionnera pas correctement.
Otherwise, the script will not function correctly.

## Utilisation

1. **Attacher le script:**
   - Attachez le script `ToolsColliders` à un GameObject dans votre scène Unity.

2. **Configurer les options de filtrage:**
   - Utilisez les options de filtrage par tag et par layer pour inclure ou exclure certains colliders.

3. **Visualiser les colliders:**
   - Utilisez les boutons "Show Colliders" et "Hide Colliders" pour afficher ou masquer les colliders en mode wireframe ou solide.

## Usage

1. **Attach the script:**
   - Attach the `ToolsColliders` script to a GameObject in your Unity scene.

2. **Configure filtering options:**
   - Use the filtering options by tag and layer to include or exclude certain colliders.

3. **Visualize the colliders:**
   - Use the "Show Colliders" and "Hide Colliders" buttons to display or hide the colliders in wireframe or solid mode.

## Propriétés

- `groupingMode`: Mode de regroupement des colliders (par nom de parent, par tag, par layer). / Mode of grouping colliders (by parent name, by tag, by layer).
- `useTagFilter`: Activer ou désactiver le filtrage par tag. / Enable or disable filtering by tag.
- `allowedTags`: Liste des tags autorisés pour le filtrage. / List of allowed tags for filtering.
- `useLayerFilter`: Activer ou désactiver le filtrage par layer. / Enable or disable filtering by layer.
- `allowedLayerMask`: LayerMask autorisé pour le filtrage. / Allowed LayerMask for filtering.
- `colliderGroups`: Liste des groupes de colliders. / List of collider groups.
- `collidersList`: Liste des colliders. / List of colliders.

## Méthodes

- `GetGroups()`: Retourne la liste des groupes de colliders. / Returns the list of collider groups.
- `GetColliders()`: Retourne la liste des colliders. / Returns the list of colliders.
- `RefreshCollidersList()`: Rafraîchit la liste des colliders. / Refreshes the list of colliders.
- `ShowGizmoColliders()`: Affiche les colliders en mode Gizmo. / Displays the colliders in Gizmo mode.
- `HideGizmosColliders()`: Masque les colliders en mode Gizmo. / Hides the colliders in Gizmo mode.
- `ClearTags()`: Vide la liste des tags autorisés. / Clears the list of allowed tags.
- `AddAllTags()`: Ajoute tous les tags disponibles à la liste des tags autorisés. / Adds all available tags to the list of allowed tags.
- `PassesFilter(Collider collider)`: Vérifie si un collider passe les filtres définis. / Checks if a collider passes the defined filters.
- `OnValidate()`: Méthode appelée lors de la validation de l'objet dans l'éditeur. / Method called when the object is validated in the editor.
- `OnDrawGizmos()`: Méthode appelée pour dessiner les Gizmos dans l'éditeur. / Method called to draw Gizmos in the editor.

## Author

- **Astrify Dev**
- **Date:** 2023-10-25
- **Version:** V1

