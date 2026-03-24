using UnityEngine;
using System.Collections;
public class Pause : MonoBehaviour
{
    public static bool paused = false;
    [SerializeField] private GameObject pauseMenu;
    
        void Update(){
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (paused == false)
                {
                    Debug.Log("paused");
                    paused = true;
                    Paused();
                }
                else
                {
                    paused = false;
                    Unpaused();
                }
            }
            
        }
        private void Paused()
        {
            pauseMenu.SetActive(true);
            Animator[] animators = FindObjectsOfType<Animator>();
            foreach (Animator anim in animators) {
                anim.enabled = false;
            }
            Time.timeScale = 0;
        }

        private void Unpaused()
        {
            pauseMenu.SetActive(false);
            Animator[] animators = FindObjectsOfType<Animator>();
            foreach (Animator anim in animators) {
                anim.enabled = true;
            }
            Time.timeScale = 1;
        }
}
