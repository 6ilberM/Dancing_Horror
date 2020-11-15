using UnityEngine;

public class MonsterDancer : MonoBehaviour {

    [SerializeField] private Animator enemyAnimator = null;

    private DanceSequence danceMoveSequence = new DanceSequence();
    private int danceSequenceIndex = 1;
    private bool playedIntro = false;

    private int difficultyCounter = 1;

    public void PlayIntro() {
        if (!playedIntro) {
            AudioManager.PlayMonsterIntro();
            playedIntro = true;
        }
    }

    public void ResetDanceSequence() {
        danceSequenceIndex = 1;
    }

    public void SetupDanceSequence() {
        // TODO: Choose a dance sequence difficulty and determine if 
        ResetDanceSequence();
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
        DanceType danceMoveType = danceMoveSequence.GetMoveFromIndex(index);

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

    private bool HasDoubleTime(int index) {
        return danceMoveSequence.GetMoveFromIndex(index + 1) == DanceType.NONE;
    }

    public void HitUpMove(bool shortSound, bool soundOn = true) {
        enemyAnimator.SetTrigger("Up");
        if (soundOn) {
            AudioManager.StopSfx();
            if (shortSound) { AudioManager.PlayMonsterShortUpSoundL1(); } 
            else { AudioManager.PlayMonsterLongUpSoundL1(); }
        }
    }

    public void HitDownMove(bool shortSound, bool soundOn = true) {
        enemyAnimator.SetTrigger("Down");
        if (soundOn) {
            AudioManager.StopSfx();
            if (shortSound) { AudioManager.PlayMonsterShortDownSoundL1(); } 
            else { AudioManager.PlayMonsterLongDownSoundL1(); }
        }
    }

    public void HitLeftMove(bool shortSound, bool soundOn = true) {
        enemyAnimator.SetTrigger("Left");
        if (soundOn) {
            AudioManager.StopSfx();
            if (shortSound) { AudioManager.PlayMonsterShortLeftSoundL1(); } 
            else { AudioManager.PlayMonsterLongLeftSoundL1(); }
        }
    }

    public void HitRightMove(bool shortSound, bool soundOn = true) {
        enemyAnimator.SetTrigger("Right");
        if (soundOn) {
            AudioManager.StopSfx();
            if (shortSound) { AudioManager.PlayMonsterShortRightSoundL1(); } 
            else { AudioManager.PlayMonsterLongRightSoundL1(); }
        }
    }

    public void HitTwistMove(bool shortSound, bool soundOn = true) {
        enemyAnimator.SetTrigger("Twist");
        if (soundOn) {
            AudioManager.StopSfx();
            if (shortSound) { AudioManager.PlayMonsterShortTwistSoundL1(); } 
            else { AudioManager.PlayMonsterLongTwistSoundL1(); }
        }
    }

    public void HitPoseMove(bool shortSound, bool soundOn = true) {
        enemyAnimator.SetTrigger("Pose");
        if (soundOn) {
            AudioManager.StopSfx();
            if (shortSound) { AudioManager.PlayMonsterShortPoseSoundL1(); } else { AudioManager.PlayMonsterLongPoseSoundL1(); }
        }
    }

    public void Roar(bool soundOn = true) {
        enemyAnimator.SetTrigger("Roar");
        if (soundOn) {
            AudioManager.PlayMonsterRoar();
        }
    }

    public void Growl(bool soundOn = true) {
        enemyAnimator.SetTrigger("Roar");
        if (soundOn) {
            AudioManager.PlayMonsterGrowl();
        }
    }
}
