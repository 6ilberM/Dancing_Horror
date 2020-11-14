using UnityEngine;

[ExecuteAlways]
public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioSource monsterUp = null;
    [SerializeField] private AudioSource monsterDown = null;
    [SerializeField] private AudioSource monsterLeft = null;
    [SerializeField] private AudioSource monsterRight = null;
    [SerializeField] private AudioSource monsterTwist = null;
    [SerializeField] private AudioSource monsterPose = null;

    private static AudioManager instance;

    void Awake() {
        instance = this;
    }

    public static void PlayMonsterUpSound() {
        instance.monsterUp.Play();
    }

    public static void PlayMonsterDownSound() {
        instance.monsterDown.Play();
    }
    public static void PlayMonsterLeftSound() {
        instance.monsterLeft.Play();
    }
    public static void PlayMonsterRightSound() {
        instance.monsterRight.Play();
    }

    public static void PlayMonsterTwistSound() {
        instance.monsterTwist.Play();
    }

    public static void PlayMonsterPoseSound() {
        instance.monsterPose.Play();
    }

    public static void StopMonsterSfx() {
        instance.monsterUp.Stop();
        instance.monsterDown.Stop();
        instance.monsterLeft.Stop();
        instance.monsterRight.Stop();
        instance.monsterTwist.Stop();
        instance.monsterPose.Stop();
    }

    #if UNITY_EDITOR
    private void OnValidate() {
        instance = this;
    }
    #endif
}
