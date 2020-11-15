using UnityEngine;
using UnityEngine.SceneManagement;

public class DanceSequenceController : MonoBehaviour {

    [SerializeField] private MonsterDancer monster = null;
    [SerializeField] private PlayerDancer player = null;

    private DanceSequence danceMoveSequence = new DanceSequence();
    private int danceSequenceIndex = 1;
    private int realMoveDanceSequenceIndex = 0;
    private bool playedIntro = false;

    private int maxMissCount = 5;
    private int totalMissCount = 0;

    private int difficultyCounter = 1;

    public void PlayIntro() {
        if (!playedIntro) {
            AudioManager.PlayMonsterIntro();
            playedIntro = true;
        }
    }

    public void MonsterResetDanceSequence() {
        danceSequenceIndex = 1;
        realMoveDanceSequenceIndex = 0;
    }

    public void MonsterSetupDanceSequence() {
        // TODO: Choose a dance sequence difficulty and determine if 
        MonsterResetDanceSequence();
        switch (difficultyCounter++) {
            case 1: danceMoveSequence = DanceSequencePresets.GetDanceSequence(
                    DanceSequenceDifficulty.EASY); break;
            case 2: danceMoveSequence = DanceSequencePresets.GetDanceSequence(
                    DanceSequenceDifficulty.MEDIUM); break;
            default: danceMoveSequence = DanceSequencePresets.GetDanceSequence(
                    DanceSequenceDifficulty.HARD); break;
        }
    }

    public void PlayerSetupKeySequence() => player.SetupDanceKeyGroup(danceMoveSequence);

    public void ClearDanceKeyGroup() => player.ClearDanceKeyGroup();

    public void MonsterHitNextDanceMoveInSequence() {
        int index = danceSequenceIndex++;
        DanceType danceMoveType = danceMoveSequence.GetMoveFromIndex(index);

        if (danceMoveType == DanceType.NONE) {
            return;
        }
        switch (danceMoveType) {
            case DanceType.UP:
                if (HasDoubleTime(index)) { monster.HitUpMove(shortSound: false); } 
                else { monster.HitUpMove(shortSound: true); }
                break;
            case DanceType.DOWN:
                if (HasDoubleTime(index)) { monster.HitDownMove(shortSound: false); } 
                else { monster.HitDownMove(shortSound: true); }
                break;
            case DanceType.LEFT:
                if (HasDoubleTime(index)) { monster.HitLeftMove(shortSound: false); } 
                else { monster.HitLeftMove(shortSound: true); }
                break;
            case DanceType.RIGHT:
                if (HasDoubleTime(index)) { monster.HitRightMove(shortSound: false); } 
                else { monster.HitRightMove(shortSound: true); }
                break;
            case DanceType.TWIST:
                if (HasDoubleTime(index)) { monster.HitTwistMove(shortSound: false); } 
                else { monster.HitTwistMove(shortSound: true); }
                break;
            case DanceType.POSE:
                if (HasDoubleTime(index)) { monster.HitPoseMove(shortSound: false); } 
                else { monster.HitPoseMove(shortSound: true); }
                break;
            case DanceType.NONE: break;
        }

        player.DisplayNextRedCharacterInDanceKeyGroup();
    }

    public void PreMonsterSetup() {
        ClearDanceKeyGroup();
        MonsterSetupDanceSequence();
        PlayerSetupKeySequence();
    }

    public void PrePlayerSetup() {
        PlayerSetupKeySequence();
        PlayerStartInputSequence();
        MonsterResetDanceSequence();
    }

    public void PostPlayerSetup() {
        ClearDanceKeyGroup();
        PlayerStopInputSequence();
    }

    public void GoToRoom() {
        SceneManager.LoadScene("First_floor");
    }

    public void PlayerStartInputSequence() => player.StartInputtingSequence();

    public void PlayerStopInputSequence() => player.StopInputtingSequence();

    public void PlayerHitNextDanceMoveInSequence() {
        int index = danceSequenceIndex++;
        DanceType danceMoveType = danceMoveSequence.GetMoveFromIndex(index);

        if (danceMoveType == DanceType.NONE) {
            return;
        }

        if (!player.HasHitMove(realMoveDanceSequenceIndex++)) {
            monster.Growl();
            if (++totalMissCount > maxMissCount) {
                SceneManager.LoadScene("DeathScene");
            }
            return;
        }

        switch (danceMoveType) {
            case DanceType.UP:
                player.HitUpMove();
                monster.HitUpMove(shortSound: false, soundOn: false);
                break;
            case DanceType.DOWN:
                player.HitDownMove();
                monster.HitDownMove(shortSound: false, soundOn: false);
                break;
            case DanceType.LEFT:
                player.HitLeftMove();
                monster.HitLeftMove(shortSound: false, soundOn: false);
                break;
            case DanceType.RIGHT:
                player.HitRightMove();
                monster.HitRightMove(shortSound: false, soundOn: false);
                break;
            case DanceType.TWIST:
                player.HitTwistMove();
                monster.HitTwistMove(shortSound: false, soundOn: false);
                break;
            case DanceType.POSE:
                player.HitPoseMove();
                monster.HitPoseMove(shortSound: false, soundOn: false);
                break;
            case DanceType.NONE: break;
        }
    }

    private bool HasDoubleTime(int index) {
        return danceMoveSequence.GetMoveFromIndex(index + 1) == DanceType.NONE;
    }
}
