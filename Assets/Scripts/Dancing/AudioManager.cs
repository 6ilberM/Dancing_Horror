using UnityEngine;

[ExecuteAlways]
public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioSource monsterIntro = null;
    [SerializeField] private AudioSource monsterShortUpL1 = null;
    [SerializeField] private AudioSource monsterShortDownL1 = null;
    [SerializeField] private AudioSource monsterShortLeftL1 = null;
    [SerializeField] private AudioSource monsterShortRightL1 = null;
    [SerializeField] private AudioSource monsterShortTwistL1 = null;
    [SerializeField] private AudioSource monsterShortPoseL1 = null;
    [SerializeField] private AudioSource monsterLongUpL1 = null;
    [SerializeField] private AudioSource monsterLongDownL1 = null;
    [SerializeField] private AudioSource monsterLongLeftL1 = null;
    [SerializeField] private AudioSource monsterLongRightL1 = null;
    [SerializeField] private AudioSource monsterLongTwistL1 = null;
    [SerializeField] private AudioSource monsterLongPoseL1 = null;

    [Space]
    [SerializeField] private AudioSource monsterShortUpL2 = null;
    [SerializeField] private AudioSource monsterShortDownL2 = null;
    [SerializeField] private AudioSource monsterShortLeftL2 = null;
    [SerializeField] private AudioSource monsterShortRightL2 = null;
    [SerializeField] private AudioSource monsterShortTwistL2 = null;
    [SerializeField] private AudioSource monsterShortPoseL2 = null;
    [SerializeField] private AudioSource monsterLongUpL2 = null;
    [SerializeField] private AudioSource monsterLongDownL2 = null;
    [SerializeField] private AudioSource monsterLongLeftL2 = null;
    [SerializeField] private AudioSource monsterLongRightL2 = null;
    [SerializeField] private AudioSource monsterLongTwistL2 = null;
    [SerializeField] private AudioSource monsterLongPoseL2 = null;

    [Space]
    [SerializeField] private AudioSource monsterShortUpL3 = null;
    [SerializeField] private AudioSource monsterShortDownL3 = null;
    [SerializeField] private AudioSource monsterShortLeftL3 = null;
    [SerializeField] private AudioSource monsterShortRightL3 = null;
    [SerializeField] private AudioSource monsterShortTwistL3 = null;
    [SerializeField] private AudioSource monsterShortPoseL3 = null;
    [SerializeField] private AudioSource monsterLongUpL3 = null;
    [SerializeField] private AudioSource monsterLongDownL3 = null;
    [SerializeField] private AudioSource monsterLongLeftL3 = null;
    [SerializeField] private AudioSource monsterLongRightL3 = null;
    [SerializeField] private AudioSource monsterLongTwistL3 = null;
    [SerializeField] private AudioSource monsterLongPoseL3 = null;

    private static AudioManager instance;

    private static AudioSource[] monsterSfx;

    void Awake() {
        instance = this;
        monsterSfx = new AudioSource[] {
            monsterShortUpL1,
            monsterShortDownL1,
            monsterShortLeftL1,
            monsterShortRightL1,
            monsterShortTwistL1,
            monsterShortPoseL1,
            monsterLongUpL1,
            monsterLongDownL1,
            monsterLongLeftL1,
            monsterLongRightL1,
            monsterLongTwistL1,
            monsterLongPoseL1,
            monsterShortUpL2,
            monsterShortDownL2,
            monsterShortLeftL2,
            monsterShortRightL2,
            monsterShortTwistL2,
            monsterShortPoseL2,
            monsterLongUpL2,
            monsterLongDownL2,
            monsterLongLeftL2,
            monsterLongRightL2,
            monsterLongTwistL2,
            monsterLongPoseL2,
            monsterShortUpL3,
            monsterShortDownL3,
            monsterShortLeftL3,
            monsterShortRightL3,
            monsterShortTwistL3,
            monsterShortPoseL3,
            monsterLongUpL3,
            monsterLongDownL3,
            monsterLongLeftL3,
            monsterLongRightL3,
            monsterLongTwistL3,
            monsterLongPoseL3
        };
    }

    public static void PlayMonsterShortUpSoundL1() => instance.monsterShortUpL1.Play();
    public static void PlayMonsterShortDownSoundL1() => instance.monsterShortDownL1.Play();
    public static void PlayMonsterShortLeftSoundL1() => instance.monsterShortLeftL1.Play();
    public static void PlayMonsterShortRightSoundL1() => instance.monsterShortRightL1.Play();
    public static void PlayMonsterShortTwistSoundL1() => instance.monsterShortTwistL1.Play();
    public static void PlayMonsterShortPoseSoundL1() => instance.monsterShortPoseL1.Play();

    public static void PlayMonsterLongUpSoundL1() => instance.monsterShortUpL1.Play();
    public static void PlayMonsterLongDownSoundL1() => instance.monsterLongDownL1.Play();
    public static void PlayMonsterLongLeftSoundL1() => instance.monsterLongLeftL1.Play();
    public static void PlayMonsterLongRightSoundL1() => instance.monsterLongRightL1.Play();
    public static void PlayMonsterLongTwistSoundL1() => instance.monsterLongTwistL1.Play();
    public static void PlayMonsterLongPoseSoundL1() => instance.monsterLongPoseL1.Play();
    public static void PlayMonsterIntro() => instance.monsterIntro.Play();

    public static void StopMonsterSfx() {
        foreach (var sfx in monsterSfx) {
            sfx.Stop();
        }
    }

#if UNITY_EDITOR
    private void OnValidate() {
        instance = this;
    }
#endif
}
