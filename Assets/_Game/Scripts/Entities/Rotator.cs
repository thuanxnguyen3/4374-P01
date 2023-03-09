using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Rotator : MonoBehaviour
{
    public static event Action OnCollected;
    [SerializeField] Vector3 rotateForce = new Vector3(0, 100, 0);
    //public TextMeshProUGUI scoreText;
    //public int counter = 0;
    
    
    void Update()
    {
        // rotate this object each frame
        Debug.Log("rotate");
        this.transform.Rotate(rotateForce * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollected?.Invoke();
            //counter += 100;
            //scoreText.text = counter.ToString();
            Destroy(gameObject);
        }
    }
}
