using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameConstants gameConstants;
    public FloatVariable health;
    
    public FloatVariable decay;
    public void LoadScene(string sceneName)
    {
        health.SetValue(gameConstants.startingTimer);
        decay.SetValue(gameConstants.startingDecay);
        SceneManager.LoadScene(sceneName);
    }
}