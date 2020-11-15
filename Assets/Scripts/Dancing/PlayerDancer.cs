using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDancer : MonoBehaviour {

    [SerializeField] private Sprite redUpArrow = null;
    [SerializeField] private Sprite redDownArrow = null;
    [SerializeField] private Sprite redLeftArrow = null;
    [SerializeField] private Sprite redRightArrow = null;
    [SerializeField] private Sprite redXArrow = null;
    [SerializeField] private Sprite redCArrow = null;

    [Space]
    [SerializeField] private Sprite greenUpArrow = null;
    [SerializeField] private Sprite greenDownArrow = null;
    [SerializeField] private Sprite greenLeftArrow = null;
    [SerializeField] private Sprite greenRightArrow = null;
    [SerializeField] private Sprite greenXArrow = null;
    [SerializeField] private Sprite greenCArrow = null;

    [Space]
    [SerializeField] private Sprite grayUpArrow = null;
    [SerializeField] private Sprite grayDownArrow = null;
    [SerializeField] private Sprite grayLeftArrow = null;
    [SerializeField] private Sprite grayRightArrow = null;
    [SerializeField] private Sprite grayXArrow = null;
    [SerializeField] private Sprite grayCArrow = null;

    [Space]
    [SerializeField] private DanceKeyGroup danceKeyGroup2 = null;
    [SerializeField] private DanceKeyGroup danceKeyGroup3 = null;
    [SerializeField] private DanceKeyGroup danceKeyGroup4 = null;
    [SerializeField] private DanceKeyGroup danceKeyGroup5 = null;
    [SerializeField] private DanceKeyGroup danceKeyGroup6 = null;
    [SerializeField] private DanceKeyGroup danceKeyGroup7 = null;

    [SerializeField] private Animator playerAnimator = null;

    //private DanceSequence referenceDanceSequence = new DanceSequence();
    public DanceSequence ActiveTrimmedDanceSequence { get; set; } = null;
    private DanceKeyGroup activeDanceKeyGroup = null;
    private bool[] hitMoves = { false, false, false, false, false, false, false, false, false };
    private int danceIndex = 0;

    private int danceKeyAppearIndex = 0;
    private int danceKeyHitIndex = 1;

    private int maxMissCount = 5;
    private int totalMissCount = 0;

    private bool isPlayerTurn = false;

    public void SetupDanceKeyGroup(DanceSequence danceSequence) {
        //referenceDanceSequence = danceSequence;
        for (int i = 0; i < 9; ++i) { hitMoves[i] = false; }
        danceKeyAppearIndex = 0;
        ActiveTrimmedDanceSequence = new DanceSequence();

        int cnt = 0;
        int activeIndex = 1;
        for (int i = 1; i <= 9; ++i) {
            var danceMove = danceSequence.GetMoveFromIndex(i);
            if (danceMove != DanceType.NONE) {
                ++cnt;
                ActiveTrimmedDanceSequence.SetMoveFromIndex(activeIndex++, danceMove);
            }
        }

        switch (cnt) {
            case 2: activeDanceKeyGroup = danceKeyGroup2; break;
            case 3: activeDanceKeyGroup = danceKeyGroup3; break;
            case 4: activeDanceKeyGroup = danceKeyGroup4; break;
            case 5: activeDanceKeyGroup = danceKeyGroup5; break;
            case 6: activeDanceKeyGroup = danceKeyGroup6; break;
            case 7: activeDanceKeyGroup = danceKeyGroup7; break;
            default: Debug.LogError("Can't use dance key group that has <2 or >7 keys."); break;
        }
    }

    public void ClearDanceKeyGroup() {
        if (activeDanceKeyGroup == null) {
            return;
        }
        for (int i = 0; i < activeDanceKeyGroup.NumberOfKeys; ++i) {
            activeDanceKeyGroup.SetKeySprite(i, null);
        }
    }

    public void DisplayNextRedCharacterInDanceKeyGroup() {
        int index = ++danceKeyAppearIndex;

        if (index > activeDanceKeyGroup.NumberOfKeys) {
            return;
        }

        var danceType = ActiveTrimmedDanceSequence.GetMoveFromIndex(index);
        Sprite keySprite = null;
        switch (danceType) {
            case DanceType.UP: keySprite = grayUpArrow; break;
            case DanceType.DOWN: keySprite = grayDownArrow; break;
            case DanceType.LEFT: keySprite = grayLeftArrow; break;
            case DanceType.RIGHT: keySprite = grayRightArrow; break;
            case DanceType.TWIST: keySprite = grayXArrow; break;
            case DanceType.POSE: keySprite = grayCArrow; break;
        }
        activeDanceKeyGroup.SetKeySprite(index - 1, keySprite);
    }

    //public void DisplayControlls(DanceSequence danceSequence) {

    //    DisplayControlls(activeDanceKeyGroup, danceSequence);
    //}

    //private void DisplayControlls(DanceKeyGroup danceKeyGroup, DanceSequence danceSequence) {
    //    int cnt = 0;
    //    for (int i = 1; i <= 9; ++i) {
    //        var danceType = danceSequence.GetMoveFromIndex(i);
    //        if (danceType != DanceType.NONE) {
    //            Sprite sprite = null;
    //            switch (danceType) {
    //                case DanceType.UP: sprite = redUpArrow; break;
    //                case DanceType.DOWN: sprite = redDownArrow; break;
    //                case DanceType.LEFT: sprite = redLeftArrow; break;
    //                case DanceType.RIGHT: sprite = redRightArrow; break;
    //                case DanceType.TWIST: sprite = redXArrow; break;
    //                case DanceType.POSE: sprite = redCArrow; break;
    //            }

    //            danceKeyGroup.SetKeySprite(cnt++, sprite);
    //        }
    //    }
    //}

    public void ResolveKeyPress(Key key) {
        int index = danceIndex++;
        // TODO: If its the final key in the sequence then do something
        var danceType = ActiveTrimmedDanceSequence.GetMoveFromIndex(danceIndex);
        switch (danceType) {
            case DanceType.UP: if (key != Key.UpArrow) { ResolveDanceKeyWrongHit(); } break;
            case DanceType.DOWN: if (key != Key.DownArrow) { ResolveDanceKeyWrongHit(); } break;
            case DanceType.LEFT: if (key != Key.LeftArrow) { ResolveDanceKeyWrongHit(); } break;
            case DanceType.RIGHT: if (key != Key.RightArrow) { ResolveDanceKeyWrongHit(); } break;
            case DanceType.TWIST: if (key != Key.X) { ResolveDanceKeyWrongHit(); } break;
            case DanceType.POSE: if (key != Key.C) { ResolveDanceKeyWrongHit(); } break;
            default: break;
        }

        if (danceIndex >= activeDanceKeyGroup.NumberOfKeys) {
            for (int i = 0; i < activeDanceKeyGroup.NumberOfKeys; ++i) {
                activeDanceKeyGroup.SetKeySprite(i, null);
            }
            return;
        }

        switch (key) {
            case Key.UpArrow:
                activeDanceKeyGroup.
                    SetKeySprite(index, greenUpArrow); break;
            case Key.DownArrow:
                activeDanceKeyGroup.
                    SetKeySprite(index, greenDownArrow); break;
            case Key.LeftArrow:
                activeDanceKeyGroup.
                    SetKeySprite(index, greenLeftArrow); break;
            case Key.RightArrow:
                activeDanceKeyGroup.
                   SetKeySprite(index, greenRightArrow); break;
            case Key.X:
                activeDanceKeyGroup.
                    SetKeySprite(index, greenXArrow); break;
            case Key.C:
                activeDanceKeyGroup.
                    SetKeySprite(index, greenCArrow); break;
        }
    }

    private void ResolveDanceKeyWrongHit() {
        // TODO: Shake screen and play growl and maybe play end cutscene
        print("Missed character");
    }

    private void ResolveDanceKeyMiss() {
        // TODO: Shake screen and play growl and maybe play end cutscene
    }

    void Update() {
        if (!isPlayerTurn || danceKeyHitIndex > activeDanceKeyGroup.NumberOfKeys) {
            return;
        }

        DanceType nextDanceKey = ActiveTrimmedDanceSequence.GetMoveFromIndex(danceKeyHitIndex);

        Sprite redArrow = null;
        switch (nextDanceKey) {
            case DanceType.UP: redArrow = redUpArrow; break;
            case DanceType.DOWN: redArrow = redDownArrow; break;
            case DanceType.LEFT: redArrow = redLeftArrow; break;
            case DanceType.RIGHT: redArrow = redRightArrow; break;
            case DanceType.TWIST: redArrow = redXArrow; break;
            case DanceType.POSE: redArrow = redCArrow; break;
        }

        if (Keyboard.current.upArrowKey.wasPressedThisFrame) {
            CheckDanceKeyPressed(nextDanceKey, DanceType.UP, greenUpArrow, redArrow);
        } 
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame) {
            CheckDanceKeyPressed(nextDanceKey, DanceType.DOWN, greenDownArrow, redArrow);
        } 
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame) {
            CheckDanceKeyPressed(nextDanceKey, DanceType.LEFT, greenLeftArrow, redArrow);
        } 
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame) {
            CheckDanceKeyPressed(nextDanceKey, DanceType.RIGHT, greenRightArrow, redArrow);
        } 
        else if (Keyboard.current.xKey.wasPressedThisFrame) {
            CheckDanceKeyPressed(nextDanceKey, DanceType.TWIST, greenXArrow, redArrow);
        } 
        else if (Keyboard.current.cKey.wasPressedThisFrame) {
            CheckDanceKeyPressed(nextDanceKey, DanceType.POSE, greenCArrow, redArrow);
        }
    }

    public void CheckDanceKeyPressed(DanceType nextDanceKey, DanceType wantedDanceKey, Sprite greenSprite, Sprite redSprite) {
        int index = danceKeyHitIndex++ - 1;
        if (nextDanceKey == wantedDanceKey) {
            activeDanceKeyGroup.SetKeySprite(index, greenSprite);
            hitMoves[index] = true;
        } else {
            activeDanceKeyGroup.SetKeySprite(index, redSprite);
            activeDanceKeyGroup.ShakeKey(index);
            AudioManager.PlayWrongNoteSound();
        }
    }

    public bool HasHitMove(int index) {
        return hitMoves[index];
    }

    public void StartInputtingSequence() {
        // TODO: Add spotlight on player
        danceKeyHitIndex = 1;
        isPlayerTurn = true;
        AudioManager.PlayClapSound();
    }

    public void StopInputtingSequence() {
        // TODO: Remove spotlight from player (also add spotlight to monster in MonsterDancer)
        isPlayerTurn = false;
    }

    public void HitUpMove() {
        playerAnimator.SetTrigger("Up");
        AudioManager.StopSfx();
        AudioManager.PlayPlayerUpSound();
    }

    public void HitDownMove() {
        playerAnimator.SetTrigger("Down");
        AudioManager.StopSfx();
        AudioManager.PlayPlayerDownSound();
    }

    public void HitLeftMove() {
        playerAnimator.SetTrigger("Left");
        AudioManager.StopSfx();
        AudioManager.PlayPlayerLeftSound();
    }

    public void HitRightMove() {
        playerAnimator.SetTrigger("Right");
        AudioManager.StopSfx();
        AudioManager.PlayPlayerRightSound();
    }

    public void HitTwistMove() {
        playerAnimator.SetTrigger("Twist");
        AudioManager.StopSfx();
        AudioManager.PlayPlayerTwistSound();
    }

    public void HitPoseMove() {
        playerAnimator.SetTrigger("Pose");
        AudioManager.StopSfx();
        AudioManager.PlayPlayerPoseSound();
    }
}
