using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    [SerializeField] private Animator _animator;

    public static SceneLoader instance;

    // Shortcut
    public static void LoadScene(string name) => instance.Load(name);

    // Loading function
    public void Load(string name) {
        StartCoroutine(LoadCoroutine(name));
    }

    private void Awake() {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private IEnumerator LoadCoroutine(string name) {
        _animator.SetBool("IsLoading", true);
        var state = _animator.GetCurrentAnimatorStateInfo(0);
        
        yield return new WaitForSeconds(state.length);

        var result = SceneManager.LoadSceneAsync(name);

        while (!result.isDone)
            yield return null;

        _animator.SetBool("IsLoading", false);
    }
}