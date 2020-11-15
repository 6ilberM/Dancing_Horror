using UnityEngine;
using UnityEngine.InputSystem;

public class DanceKeyController : MonoBehaviour {

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
    [SerializeField] private DanceKeyGroup danceKeyGroup2 = null;
    [SerializeField] private DanceKeyGroup danceKeyGroup3 = null;
    [SerializeField] private DanceKeyGroup danceKeyGroup4 = null;
    [SerializeField] private DanceKeyGroup danceKeyGroup5 = null;
    [SerializeField] private DanceKeyGroup danceKeyGroup6 = null;
    [SerializeField] private DanceKeyGroup danceKeyGroup7 = null;

    private static DanceSequence activeDanceSequence = null;
    private static DanceKeyGroup activeDanceKeyGroup = null;
    private static int danceIndex = 0;

    private static DanceKeyController instance;

    private void Awake() {
        instance = this;
    }

    public static void DisplayControlls(DanceSequence danceSequence) {
        activeDanceSequence = new DanceSequence();

        int cnt = 0;
        int activeIndex = 1;
        for (int i = 1; i <= 9; ++i) {
            var danceMove = danceSequence.GetMoveFromIndex(i);
            if (danceMove != DanceType.NONE) { 
                ++cnt;
                activeDanceSequence.SetMoveFromIndex(activeIndex++, danceMove);
            }
        }

        switch (cnt) {
            case 2: activeDanceKeyGroup = instance.danceKeyGroup2; break;
            case 3: activeDanceKeyGroup = instance.danceKeyGroup3; break;
            case 4: activeDanceKeyGroup = instance.danceKeyGroup4; break;
            case 5: activeDanceKeyGroup = instance.danceKeyGroup5; break;
            case 6: activeDanceKeyGroup = instance.danceKeyGroup6; break;
            case 7: activeDanceKeyGroup = instance.danceKeyGroup7; break;
            default: Debug.LogError("Can't use dance key group that has <2 or >7 keys."); break;
        }

        DisplayControlls(activeDanceKeyGroup, danceSequence);
    }

    private static void DisplayControlls(DanceKeyGroup danceKeyGroup, DanceSequence danceSequence) {
        int cnt = 0;
        for (int i = 1; i <= 9; ++i) {
            var danceType = danceSequence.GetMoveFromIndex(i);
            if (danceType != DanceType.NONE) {
                Sprite sprite = null;
                switch (danceType) {
                    case DanceType.UP: sprite = instance.redUpArrow; break;
                    case DanceType.DOWN: sprite = instance.redDownArrow; break;
                    case DanceType.LEFT: sprite = instance.redLeftArrow; break;
                    case DanceType.RIGHT: sprite = instance.redRightArrow; break;
                    case DanceType.TWIST: sprite = instance.redXArrow; break;
                    case DanceType.POSE: sprite = instance.redCArrow; break;
                }

                danceKeyGroup.SetKeySprite(cnt++, sprite);
            }
        }
    }

    public static void ResolveKeyPress(Key key) {
        int index = danceIndex++;
        // TODO: If its the final key in the sequence then do something
        var danceType = activeDanceSequence.GetMoveFromIndex(danceIndex);
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
            case Key.UpArrow: activeDanceKeyGroup.
                    SetKeySprite(index, instance.greenUpArrow); break;
            case Key.DownArrow: activeDanceKeyGroup.
                    SetKeySprite(index, instance.greenDownArrow); break;
            case Key.LeftArrow: activeDanceKeyGroup.
                    SetKeySprite(index, instance.greenLeftArrow); break;
            case Key.RightArrow: activeDanceKeyGroup.
                    SetKeySprite(index, instance.greenRightArrow); break;
            case Key.X: activeDanceKeyGroup.
                    SetKeySprite(index, instance.greenXArrow); break;
            case Key.C: activeDanceKeyGroup.
                    SetKeySprite(index, instance.greenCArrow); break;
        }
    }

    private static void ResolveDanceKeyWrongHit() {
        // TODO: Shake screen and play growl and maybe play end cutscene
        print("Missed character");
    }

    private static void ResolveDanceKeyMiss() {
        // TODO: Shake screen and play growl and maybe play end cutscene
    }
}
