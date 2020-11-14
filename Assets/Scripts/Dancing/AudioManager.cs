using UnityEngine;

[ExecuteAlways]
public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioSource monsterShortUp = null;
    [SerializeField] private AudioSource monsterShortDown = null;
    [SerializeField] private AudioSource monsterShortLeft = null;
    [SerializeField] private AudioSource monsterShortRight = null;
    [SerializeField] private AudioSource monsterShortTwist = null;
    [SerializeField] private AudioSource monsterShortPose = null;
    [SerializeField] private AudioSource monsterLongUp = null;
    [SerializeField] private AudioSource monsterLongDown = null;
    [SerializeField] private AudioSource monsterLongLeft = null;
    [SerializeField] private AudioSource monsterLongRight = null;
    [SerializeField] private AudioSource monsterLongTwist = null;
    [SerializeField] private AudioSource monsterLongPose = null;
    [SerializeField] private AudioSource monsterIntro = null;

    private static AudioManager instance;

    void Awake() {
        instance = this;
    }

    public static void PlayMonsterShortUpSound() => instance.monsterShortUp.Play();
    public static void PlayMonsterShortDownSound() => instance.monsterShortDown.Play();
    public static void PlayMonsterShortLeftSound() => instance.monsterShortLeft.Play();
    public static void PlayMonsterShortRightSound() => instance.monsterShortRight.Play();
    public static void PlayMonsterShortTwistSound() => instance.monsterShortTwist.Play();
    public static void PlayMonsterShortPoseSound() => instance.monsterShortPose.Play();

    public static void PlayMonsterLongUpSound() => instance.monsterShortUp.Play();
    public static void PlayMonsterLongDownSound() => instance.monsterLongDown.Play();
    public static void PlayMonsterLongLeftSound() => instance.monsterLongLeft.Play();
    public static void PlayMonsterLongRightSound() => instance.monsterLongRight.Play();
    public static void PlayMonsterLongTwistSound() => instance.monsterLongTwist.Play();
    public static void PlayMonsterLongPoseSound() => instance.monsterLongPose.Play();
    public static void PlayMonsterIntro() => instance.monsterIntro.Play();

    public static void StopMonsterSfx() {
        instance.monsterShortUp.Stop();
        instance.monsterShortDown.Stop();
        instance.monsterShortLeft.Stop();
        instance.monsterLongRight.Stop();
        instance.monsterLongTwist.Stop();
        instance.monsterLongPose.Stop();
        instance.monsterLongUp.Stop();
        instance.monsterLongDown.Stop();
        instance.monsterLongLeft.Stop();
        instance.monsterLongRight.Stop();
        instance.monsterLongTwist.Stop();
        instance.monsterLongPose.Stop();
    }

    #if UNITY_EDITOR
    private void OnValidate() {
        instance = this;
    }
    #endif
}
