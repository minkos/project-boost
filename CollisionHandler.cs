// using System.Collections; // barely in use; able to delete
// using System.Collections.Generic; // barely in use; able to delete
using UnityEngine;
using UnityEngine.SceneManagement;

// MonoBehaviour: contains all the essential methods, Start(), Update()
// Don't need Start(), Update(); deleted
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip landingSound;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransistioning = false;
    bool collisionDisabled = false;

     void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        EnterDebugKeys();
    }

    void EnterDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        } 
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // toggle collision
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransistioning || collisionDisabled) { return; }

        switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Hit Launch Pad");
                    break;
                case "Finish":
                    // LoadNextLevel();
                    // Invoke("LoadNextLevel", delay);
                    StartLandingSequence();
                    break;
                case "Fuel":
                    Debug.Log("Hit Fuel");
                    break;
                default:
                    // ReloadLevel();
                    // Invoke("ReloadLevel", 1f);
                    StartCrashSequence();
                    break;
            }

    }

    void StartCrashSequence()
    {
        // add SFX and particle effects upon crash

        isTransistioning = true;
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartLandingSequence()
    {
        // add SFX and particle effects upon crash

        isTransistioning = true;
        successParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(landingSound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ReloadLevel() 
    {
        Debug.Log("Hit Obstacle. Game Over!");
        // SceneManager.LoadScene("Over"); // Or enter the scene index

        // Passing in the current active scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        //Debug.Log("Hit Landing Pad");

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        } 
        SceneManager.LoadScene(nextSceneIndex);
    }
}
