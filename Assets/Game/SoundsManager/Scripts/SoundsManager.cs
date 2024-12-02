using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip ambiantAudio;
    public AudioClip fightAudio;
    
    public bool isPlayingFight = false;

    private float lastSoundChangeTime = 0f; // Pour le verrou temporel
    private float soundChangeCooldown = 2f; // Temps en secondes avant de pouvoir changer de son � nouveau

    private bool playCorountine = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("Player/Main Camera").GetComponent<AudioSource>();
        ambiantAudio = Resources.Load<AudioClip>("Sounds/Ambient 7");
        fightAudio = Resources.Load<AudioClip>("Sounds/Action 4 (Loop)");
     
        audioSource.clip = ambiantAudio;
        audioSource.loop = true;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsPlayingFightSound() // Savoir si le son de combat est en train de jouer
    {
        return isPlayingFight;
    }


    public void PlaySound(AudioSource audioSource, AudioClip audioClip, float delay = 0) // Lancer un son sur un AudioSource sp�cifique et pour un son sp�cifique
    {
        if (audioSource.clip == audioClip) // Si le son est d�j� en train de jouer
        {
            return;
        }
        
        StartCoroutine(PlaySoundAfter(audioClip, delay, audioSource)); // On lance le son
    }

    public void PlaySound(AudioClip audioClip, float delay = 0) // Lancer un son sur l'AudioSource d'ambience et pour un son sp�cifique
    {
        if (audioSource.clip == audioClip) // Si le son est d�j� en train de jouer
        {
            return;
        }
        StartCoroutine(PlaySoundAfter(audioClip, delay)); // On lance le son
    }

    IEnumerator PlaySoundAfter(AudioClip audioClip, float delay, AudioSource audioSource = null) // Coroutine pour lancer un son apr�s un certain d�lai
    {
        playCorountine = true;
        yield return new WaitForSeconds(delay); // Attendre le d�lai
        if (audioSource == null) // Si l'AudioSource n'est pas sp�cifi�
        {
            audioSource = this.audioSource; // On prend l'AudioSource d'ambience
        }
        lastSoundChangeTime = Time.time; // On met � jour le verrou temporel
        audioSource.clip = audioClip; // On met le son � jouer
        audioSource.Play(); // On joue le son
        playCorountine = false;
    }

    public void ResetAmbienceSound() // Remettre le son d'ambiance
    {
        if (audioSource.clip == ambiantAudio) // Si le son d'ambiance est d�j� en train de jouer
        {
            return;
        }

        isPlayingFight = false;
        if (playCorountine) // Si une coroutine est en train de jouer
        {
            StopAllCoroutines(); // On arr�te la coroutine
        }
        PlaySound(ambiantAudio, 3f); // On remet le son d'ambiance
    }

    public void PlaySoundsFight() // Lancer le son de combat
    {
        if (isPlayingFight) // Si le son de combat est d�j� en train de jouer
        {
            return;
        }
        if (playCorountine) // Si une coroutine est en train de jouer
        {
            StopAllCoroutines(); // On arr�te la coroutine
        }
        isPlayingFight = true;
        PlaySound(fightAudio, 0.5f); // On lance le son de combat
    }


}
