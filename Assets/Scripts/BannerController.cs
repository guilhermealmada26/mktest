using UnityEngine;
using System.Collections;

public class BannerController : MonoBehaviour {

    [SerializeField]private Animator animator;
    private AudioSource audioSource;
    private bool animating;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        // animator = GetComponent<Animator>();
	}
	
    public bool isAnimating()
    {
        return animating;
    }

    public void animationEnded()
    {
        animating = false;
    }

    public void ShowROF()
    {
        animator.SetTrigger("Show_RO1");
        animating = true;
    }

    public void ShowYW()
    {
        animator.SetTrigger("YouWin");
        animating = true;
    }

    public void ShowYL()
    {
        animator.SetTrigger("YouLose");
        animating = true;
    }

    public void PlayVoice(AudioClip clip)
    {
        GameAuxiliary.PlaySound(audioSource, clip);
    }

    public void StopVoice()
    {
        GameAuxiliary.StopSounds(audioSource);
    }
}
