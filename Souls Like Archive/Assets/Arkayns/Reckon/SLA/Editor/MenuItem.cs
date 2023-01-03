using UnityEditor;
using UnityEngine;

namespace Arkayns.Reckon.SLA {

    public static class MenuItem {

        // -- Methods --
        [UnityEditor.MenuItem("Arkayns/Reckon/Souls Like Archive/New Character")]
        public static void CreateNewCharacter() {

            var characterObject = new GameObject("New Character", typeof(Rigidbody), typeof(InputHandler), typeof(StateManager));

            var internalComponents = new GameObject("Internal Component");
            internalComponents.transform.SetParent(characterObject.transform);
            
            var components = new GameObject("Component");
            components.transform.SetParent(characterObject.transform);
            
            var colliders = new GameObject("Collider");
            colliders.transform.SetParent(components.transform);
            
            var mainCollider = new GameObject("Body", typeof(CapsuleCollider));
            mainCollider.transform.SetParent(colliders.transform);
            
            
            var graphic = new GameObject("GFX");
            graphic.transform.SetParent(characterObject.transform);
            
            var sound = new GameObject("SFX");
            sound.transform.SetParent(characterObject.transform);
            
            Selection.activeObject = characterObject;

        } // CreateNewCharacter ()

    } // Class MenuItem

} // Namespace Arkayns Reckon SLA