using UnityEngine;

enum DanceMoveType {
    NONE,
    UP,
    DOWN,
    LEFT,
    RIGHT,
    TWIST,
    POSE,
}

/// <summary>
/// Each sequence is 200 frames long.
/// Each move in the sequence is played every 25 frames.
/// To skip a frame set the move to NONE.
/// </summary>
struct DanceMoveSequence {
    DanceMoveType move1;
    DanceMoveType move2;
    DanceMoveType move3;
    DanceMoveType move4;
    DanceMoveType move5;
    DanceMoveType move6;
    DanceMoveType move7;
    DanceMoveType move8;
    DanceMoveType move9;
}

public class MonsterDancer : MonoBehaviour {

    [SerializeField] private Animator enemyAnimator = null;

    public void SetupDanceSequence() {

    }

    public void HitNextDanceMoveInSequence() {

    }

    public void HitUpMove() {
        enemyAnimator.SetTrigger("Up");
        AudioManager.StopMonsterSfx();
        AudioManager.PlayMonsterUpSound();
    }

    public void HitDownMove() {
        enemyAnimator.SetTrigger("Down");
        AudioManager.StopMonsterSfx();
        AudioManager.PlayMonsterDownSound();
    }

    public void HitLeftMove() {
        enemyAnimator.SetTrigger("Left");
        AudioManager.StopMonsterSfx();
        AudioManager.PlayMonsterLeftSound();
    }

    public void HitRightMove() {
        enemyAnimator.SetTrigger("Right");
        AudioManager.StopMonsterSfx();
        AudioManager.PlayMonsterRightSound();
    }

    public void HitTwistMove() {
        enemyAnimator.SetTrigger("Twist");
        AudioManager.StopMonsterSfx();
        AudioManager.PlayMonsterTwistSound();
    }

    public void HitPoseMove() {
        enemyAnimator.SetTrigger("Pose");
        AudioManager.StopMonsterSfx();
        AudioManager.PlayMonsterPoseSound();
    }
}
