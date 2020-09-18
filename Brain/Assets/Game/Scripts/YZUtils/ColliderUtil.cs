using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderUtil : MonoBehaviour {
    public Action<GameObject> onCollisionEnter2D;
    public Action<GameObject> onCollisionExit2D;
    public Action<GameObject> onCollisionStay2D;
    public Action<GameObject> onTriggerEnter2D;
    public Action<GameObject> onTriggerStay2D;
    public Action<GameObject> onTriggerExit2D;
    
    private void OnCollisionEnter2D(Collision2D other) {
        onCollisionEnter2D?.Invoke(other.gameObject);
    }
    private void OnCollisionStay2D(Collision2D other) {
        onCollisionStay2D?.Invoke(other.gameObject);
    }
    private void OnCollisionExit2D(Collision2D other) {
        onCollisionExit2D?.Invoke(other.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        onTriggerEnter2D?.Invoke(other.gameObject);
    }
    private void OnTriggerStay2D(Collider2D other) {
        onTriggerStay2D?.Invoke(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other) {
        onTriggerExit2D?.Invoke(other.gameObject);
    }
    
}
