using UnityEngine;

public class MonsterDancer : MonoBehaviour {

    [SerializeField] private Animator enemyAnimator = null;

    private DanceSequence danceMoveSequence = new DanceSequence();
    private int danceSequenceIndex = 1;
    private bool playedIntro = false;

    private int difficultyCounter = 1;

    private void Start() {
        Time.timeScale = 1.05f;
    }

    public void PlayIntro() {
        if (!playedIntro) {
            AudioManager.PlayMonsterIntro();
            playedIntro = true;
        }
    }

    public void SetupDanceSequence() {
        // TODO: Choose a dance sequence difficulty and determine if 
        danceSequenceIndex = 1;
        switch (difficultyCounter++) {
            case 1: danceMoveSequence = DanceSequencePresets.GetDanceSequence(
                    DanceSequenceDifficulty.EASY); break;
            case 2:
                danceMoveSequence = DanceSequencePresets.GetDanceSequence(
                    DanceSequenceDifficulty.MEDIUM); break;
            default:
                danceMoveSequence = DanceSequencePresets.GetDanceSequence(
                    DanceSequenceDifficulty.HARD); break;
        }
    }

    public void HitNextDanceMoveInSequence() {
        int index = danceSequenceIndex++;
        DanceType danceMoveType = GetMoveFromIndex(index);

        if (danceMoveType == DanceType.NONE) {
            return;
        }
        switch (danceMoveType) {
            case DanceType.UP:
                if (HasDoubleTime(index)) { HitUpMove(shortSound: false); } 
                else { HitUpMove(shortSound: true); }
                break;
            case DanceType.DOWN:
                if (HasDoubleTime(index)) { HitDownMove(shortSound: false); } 
                else { HitDownMove(shortSound: true); }
                break;
            case DanceType.LEFT:
                if (HasDoubleTime(index)) { HitLeftMove(shortSound: false); } 
                else { HitLeftMove(shortSound: true); }
                break;
            case DanceType.RIGHT:
                if (HasDoubleTime(index)) { HitRightMove(shortSound: false); } 
                else { HitRightMove(shortSound: true); }
                break;
            case DanceType.TWIST:
                if (HasDoubleTime(index)) { HitTwistMove(shortSound: false); } 
                else { HitTwistMove(shortSound: true); }
                break;
            case DanceType.POSE:
                if (HasDoubleTime(index)) { HitPoseMove(shortSound: false); } 
                else { HitPoseMove(shortSound: true); }
                break;
            case DanceType.NONE: break;
        }
    }

    private DanceType GetMoveFromIndex(int index) {
        switch (index) {
            case 1: return danceMoveSequence.move1;
            case 2: return danceMoveSequence.move2;
            case 3: return danceMoveSequence.move3;
            case 4: return danceMoveSequence.move4;
            case 5: return danceMoveSequence.move5;
            case 6: return danceMoveSequence.move6;
            case 7: return danceMoveSequence.move7;
            case 8: return danceMoveSequence.move8;
            case 9: return danceMoveSequence.move9;
            default: return DanceType.NONE;
        }
    }

    private bool HasDoubleTime(int index) {
        return GetMoveFromIndex(index + 1) == DanceType.NONE;
    }

    public void HitUpMove(bool shortSound) {
        enemyAnimator.SetTrigger("Up");
        AudioManager.StopMonsterSfx();
        if (shortSound) { AudioManager.PlayMonsterShortUpSound(); } 
        else {            AudioManager.PlayMonsterLongUpSound();  }
    }

    public void HitDownMove(bool shortSound) {
        enemyAnimator.SetTrigger("Down");
        AudioManager.StopMonsterSfx();
        if (shortSound) { AudioManager.PlayMonsterShortDownSound(); } 
        else { AudioManager.PlayMonsterLongDownSound(); }
    }

    public void HitLeftMove(bool shortSound) {
        enemyAnimator.SetTrigger("Left");
        AudioManager.StopMonsterSfx();
        if (shortSound) { AudioManager.PlayMonsterShortLeftSound(); } 
        else { AudioManager.PlayMonsterLongLeftSound(); }
    }

    public void HitRightMove(bool shortSound) {
        enemyAnimator.SetTrigger("Right");
        AudioManager.StopMonsterSfx();
        if (shortSound) { AudioManager.PlayMonsterShortRightSound(); } 
        else { AudioManager.PlayMonsterLongRightSound(); }
    }

    public void HitTwistMove(bool shortSound) {
        enemyAnimator.SetTrigger("Twist");
        AudioManager.StopMonsterSfx();
        if (shortSound) { AudioManager.PlayMonsterShortTwistSound(); } 
        else { AudioManager.PlayMonsterLongTwistSound(); }
    }

    public void HitPoseMove(bool shortSound) {
        enemyAnimator.SetTrigger("Pose");
        AudioManager.StopMonsterSfx();
        if (shortSound) { AudioManager.PlayMonsterShortPoseSound(); } 
        else { AudioManager.PlayMonsterLongPoseSound(); }
    }
}
