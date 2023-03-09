using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleCount : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int count;
    [SerializeField] AudioSource audioSFX;
    private void Awake()
    {
        audioSFX = GetComponent<AudioSource>();
    }
    private void Start()
    {
        PlayerPrefs.SetInt("Score", 0);
    }
    void OnEnable() => Rotator.OnCollected += OnCollectibleCollected;
    void OnDisable() => Rotator.OnCollected -= OnCollectibleCollected;

    public void OnCollectibleCollected()
    {
        audioSFX.Play();
        count += 100;
        PlayerPrefs.SetInt("Score", count);
        text.text = count.ToString();
    }

}
