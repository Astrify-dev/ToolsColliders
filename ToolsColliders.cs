/**
 * Astrify Dev
 * Date: 2023-10-25
 * Version: V1
 * 
 * Description (FR):
 * Ce script Unity permet de regrouper et de gérer les colliders dans une hiérarchie de GameObjects. 
 * Il offre des options de filtrage par tag et par layer, et permet de visualiser les colliders en mode wireframe ou solide.
 * 
 * Description (EN):
 * This Unity script allows grouping and managing colliders within a hierarchy of GameObjects.
 * It provides filtering options by tag and layer, and allows visualizing colliders in wireframe or solid mode.
 * 
 * Note importante: Ce script doit être attaché à un GameObject et doit être placé dans une hiérarchie de GameObjects comme suit:
 * Important note: This script must be attached to a GameObject and placed within a hierarchy of GameObjects as follows:
 * 
 * -- Group 1
 *       group 1.1
 *          ==> gameobject collider 1
 *          ==> gameobject collider 2
 *          ==> gameobject collider 3
 *       group 1.2
 *          ==> gameobject collider 1
 *          ==> gameobject collider 2
 *          ==> gameobject collider 3
 *       ...
 * -- Group 2
 *       group 2.1
 *          ==> gameobject collider 1
 *          ==> gameobject collider 2
 *          ==> gameobject collider 3
 *       group 2.2
 *          ==> gameobject collider 1
 *          ==> gameobject collider 2
 *          ==> gameobject collider 3
 *       ...
 * .....
 * 
 */


using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Astrify.Tools{
    public class ToolsColliders : MonoBehaviour{
        public enum GroupingMode{
            ByParentName,
            ByTag,
            ByLayer
        }

        [BoxGroup("Settings")]
        [field: SerializeField] public GroupingMode groupingMode = GroupingMode.ByParentName;

        [BoxGroup("Filter Options")]
        [SerializeField] private bool _useTagFilter = false;

        [ShowIf(nameof(_useTagFilter)), BoxGroup("Filter Options")]
        [SerializeField] private List<string> _allowedTags = new List<string>();

        [BoxGroup("Filter Options")]
        [SerializeField] private bool _useLayerFilter = false;

        [ShowIf(nameof(_useLayerFilter)), BoxGroup("Filter Options")]
        [SerializeField] private LayerMask _allowedLayerMask;

        [SerializeField] private List<ColliderGroup> _colliderGroups = new List<ColliderGroup>();
        private List<Collider> _collidersList = new List<Collider>();
        private bool _collidersVisible = false;

        public List<ColliderGroup> GetGroups() => _colliderGroups;
        public List<Collider> GetColliders() => _collidersList;

        [System.Serializable]
        public class ColliderGroup{
            [ReadOnly] public string name;
            [ReadOnly] public List<Collider> colliders = new List<Collider>();
            public Color gizmoColor = Color.green;
            public bool isSelected;
            public bool showWireframe = true;
            public bool showSolid = true;
        }

        [Button("Refresh Collider List")]
        private void RefreshCollidersList(){
            _collidersList.Clear();

            var previousColors = new Dictionary<string, Color>();
            var previousSelections = new Dictionary<string, bool>();
            var previousWireframes = new Dictionary<string, bool>();
            var previousSolids = new Dictionary<string, bool>();

            foreach (var group in _colliderGroups){
                previousColors[group.name] = group.gizmoColor;
                previousSelections[group.name] = group.isSelected;
                previousWireframes[group.name] = group.showWireframe;
                previousSolids[group.name] = group.showSolid;
            }

            _colliderGroups.Clear();
            var groupsByKey = new Dictionary<string, ColliderGroup>();
            Collider[] allColliders = GetComponentsInChildren<Collider>(includeInactive: true);

            foreach (var collider in allColliders){
                if (!PassesFilter(collider))
                    continue;

                _collidersList.Add(collider);

                string key = GetGroupingKey(collider.transform);

                if (!groupsByKey.ContainsKey(key)){
                    var group = new ColliderGroup { name = key };

                    if (previousColors.TryGetValue(key, out var color))
                        group.gizmoColor = color;

                    if (previousSelections.TryGetValue(key, out var isSelected))
                        group.isSelected = isSelected;

                    if (previousWireframes.TryGetValue(key, out var showWireframe))
                        group.showWireframe = showWireframe;

                    if (previousSolids.TryGetValue(key, out var showSolid))
                        group.showSolid = showSolid;

                    groupsByKey[key] = group;
                }

                groupsByKey[key].colliders.Add(collider);
            }

            _colliderGroups.AddRange(groupsByKey.Values);
        }

        [Button("Show Colliders"), ShowIf(nameof(Get_collidersVisible))]
        private void ShowGizmoColliders(){
            _collidersVisible = true;
        }

        [Button("Hide Colliders"), ShowIf(nameof(_collidersVisible))]
        private void HideGizmosColliders(){
            _collidersVisible = false;
        }

        private bool PassesFilter(Collider collider){
            if (_useTagFilter && !_allowedTags.Contains(collider.tag))
                return false;

            if (_useLayerFilter && (_allowedLayerMask.value & (1 << collider.gameObject.layer)) == 0)
                return false;

            return true;
        }

        private void OnValidate({
#if UNITY_EDITOR
            if (Application.isPlaying) return;
            RefreshCollidersList();
#endif
        }

        private void OnDrawGizmos(){
            if (!_collidersVisible) return;

            foreach (var group in _colliderGroups){
                if (group.isSelected){
                    Gizmos.color = group.gizmoColor;

                    foreach (var collider in group.colliders){
                        if (collider == null) continue;

                        Gizmos.matrix = Matrix4x4.TRS(
                            collider.transform.position,
                            collider.transform.rotation,
                            collider.transform.lossyScale
                        );

                        if (group.showWireframe){
                            switch (collider){
                                case BoxCollider box:
                                    Gizmos.DrawWireCube(box.center, box.size);
                                    break;

                                case SphereCollider sphere:
                                    Gizmos.DrawWireSphere(sphere.center, sphere.radius);
                                    break;

                                case CapsuleCollider capsule:
                                    Gizmos.DrawWireSphere(capsule.center, capsule.radius);
                                    break;
                            }
                        }

                        if (group.showSolid){
                            switch (collider){
                                case BoxCollider box:
                                    Gizmos.DrawCube(box.center, box.size);
                                    break;

                                case SphereCollider sphere:
                                    Gizmos.DrawSphere(sphere.center, sphere.radius);
                                    break;

                                case CapsuleCollider capsule:
                                    Gizmos.DrawSphere(capsule.center, capsule.radius);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public bool Get_collidersVisible(){
            return !_collidersVisible;
        }

        private string GetGroupingKey(Transform t){
            string key = t.name;
            if (t.parent != null && t.parent != transform){
                key = t.parent.name + "/" + key;
                if (t.parent.parent != null && t.parent.parent != transform){
                    key = t.parent.parent.name + "/" + t.parent.name;
                }
            }
            return key;
        }
    }
}
