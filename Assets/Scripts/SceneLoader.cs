using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public static SceneLoader Instance { get; private set; }
    public float Progress { get; private set; }

    [SerializeField] private Animator _animator;
    [SerializeField] private string _animatorVariable = "IsLoading";
    private Coroutine _coroutine = null;

    private void Awake() {
        if (!Instance) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            Debug.LogWarning($"More than one {this} instance found.");
        }
    }

    public static void LoadScene(string name, LoadSceneMode loadSceneMode = LoadSceneMode.Single) => Instance.Load(name, loadSceneMode);

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
            Progress = result.progress;
            yield return null;
        }

        _animator.SetBool(_animatorVariable, false);

        _coroutine = null;
    }
}