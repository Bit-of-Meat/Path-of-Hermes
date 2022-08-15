using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public static SceneLoader Instance { get; private set; }
    public float Progress { get => _progress; }

    [SerializeField] private Animator _animator;
    [SerializeField] private string _animatorVariable = "IsLoading";
    
    private Coroutine _coroutine = null;
    private float _progress;

    private void Awake() {
        if (!Instance) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(gameObject);
            Debug.LogWarning($"More than one {this} instance found.");
        }
    }

    // Shortcut
    public static void LoadScene(string name, LoadSceneMode loadSceneMode = LoadSceneMode.Single) => Instance.Load(name, loadSceneMode);

    // Loading function
    public void Load(string name, LoadSceneMode loadSceneMode = LoadSceneMode.Single) {
        if (_coroutine == null)
            _coroutine = StartCoroutine(LoadCoroutine(name, loadSceneMode));
    }

    private IEnumerator LoadCoroutine(string name, LoadSceneMode loadSceneMode) {
        _animator.SetBool(_animatorVariable, true);
        var state = _animator.GetCurrentAnimatorStateInfo(0);
        
        yield return new WaitForSeconds(state.length);

        var result = SceneManager.LoadSceneAsync(name, loadSceneMode);

        while (!result.isDone) {
            _progress = result.progress;
            yield return null;
        }

        _animator.SetBool(_animatorVariable, false);

        _coroutine = null;
    }
}