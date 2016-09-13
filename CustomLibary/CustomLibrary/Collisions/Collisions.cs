﻿using UnityEngine;

namespace CustomLibrary.Collisions
{
    public class GoodCollisions : MonoBehaviour
    {

        /// <summary>
        /// Controleer of er iets in de weg staat met een automatische korte distance (0.05f)
        /// </summary>
        /// <param name="thisComponent">dit object</param>
        /// <param name="direction">De richting waarin we controleren of er iets is</param>
        /// <returns>Of er iets in de weg staat (true) of niet (false)</returns>
        public static bool CheckSide(Component thisComponent, Vector2 direction)
        {
            GameObject gameObject = thisComponent.gameObject;

            Vector2 sideOffset = CalcSideOffset(direction);
            float _distance = CalcDistance(sideOffset, gameObject.GetComponent<BoxCollider2D>());

            RaycastHit2D hit = PerformRaycasts(gameObject.transform.position, direction, sideOffset, _distance);

            return hit;
        }

        /// <summary>
        /// Controleer of er iets in de weg staat met een bepaalde tag met een automatische korte distance (0.05)
        /// </summary>
        /// <param name="thisComponent">dit object</param>
        /// <param name="direction">De richting waarin we controleren of er iets is</param>
        /// <param name="withTag">Welk tag het obstakel moet hebben</param>
        /// <returns>Of er iets in de weg staat (true) of niet (false)</returns>
        public static bool CheckSide(Component thisComponent, Vector2 direction, string withTag)
        {
            GameObject gameObject = thisComponent.gameObject;

            Vector2 sideOffset = CalcSideOffset(direction);
            float _distance = CalcDistance(sideOffset, gameObject.GetComponent<BoxCollider2D>());

            RaycastHit2D hit = PerformRaycasts(gameObject.transform.position, direction, sideOffset, _distance);

            return hit && hit.collider.CompareTag(withTag);
        }

        private static RaycastHit2D PerformRaycasts(Vector3 origin, Vector2 direction, Vector2 sideoffset, float distance)
        {
            Color drawColor = new Color(0, 1, 0);

            RaycastHit2D hit;
            bool hitSomething =
           ((hit = Physics2D.Raycast((Vector2)origin, direction, distance )) ||
            (hit = Physics2D.Raycast((Vector2)origin + sideoffset * distance, direction, distance + 0.05f )) ||
            (hit = Physics2D.Raycast((Vector2)origin + -sideoffset * distance, direction, distance + 0.05f )));

            if (hitSomething)
                drawColor = new Color(1, 0, 0); //Rays rood tekenen

            Debug.DrawRay(origin, direction * distance, drawColor);
            Debug.DrawRay((Vector2)origin + sideoffset * distance, direction * (distance + 0.05f), drawColor);
            Debug.DrawRay((Vector2)origin + -sideoffset * distance, direction * (distance + 0.05f), drawColor);

            return hit;
        }

        private static Vector2 CalcSideOffset(Vector2 direction)
        {
            if (direction == Vector2.left || direction == Vector2.right) return Vector2.up;
            return Vector2.left;
        }

        private static float CalcDistance(Vector2 sideOffset, BoxCollider2D collider)
        {
            if (sideOffset == Vector2.up)
            {
                return (collider.size.y / 2) + 0.05f;
            }
            return (collider.size.x / 2) + 0.05f;
        }
    }
}      