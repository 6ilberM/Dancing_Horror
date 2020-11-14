using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private static DanceKeyController instance;

    private void Awake() {
        instance = this;
    }

    public void DisplayControlls(DanceSequence danceSequence) {

        int cnt = 0;
        for (int i = 1; i <= 9; ++i) {
            if (danceSequence.GetMoveFromIndex(i) != DanceType.NONE) { 
                ++cnt; 
            }
        }

        switch (cnt) {
            case 2: DisplayControlls(danceKeyGroup2, danceSequence); break;
            case 3: DisplayControlls(danceKeyGroup3, danceSequence); break;
            case 4: DisplayControlls(danceKeyGroup4, danceSequence); break;
            case 5: DisplayControlls(danceKeyGroup5, danceSequence); break;
            case 6: DisplayControlls(danceKeyGroup6, danceSequence); break;
            case 7: DisplayControlls(danceKeyGroup7, danceSequence); break;
            default: break;
        }
    }

    private void DisplayControlls(DanceKeyGroup danceKeyGroup, DanceSequence danceSequence) {
        int cnt = 0;
        for (int i = 1; i <= 9; ++i) {
            var danceType = danceSequence.GetMoveFromIndex(i);
            if (danceType != DanceType.NONE) {
                Sprite sprite = null;
                switch (danceType) {
                    case DanceType.UP: sprite = redUpArrow; break;
                    case DanceType.DOWN: sprite = redDownArrow; break;
                    case DanceType.LEFT: sprite = redLeftArrow; break;
                    case DanceType.RIGHT: sprite = redRightArrow; break;
                    case DanceType.TWIST: sprite = redXArrow; break;
                    case DanceType.POSE: sprite = redCArrow; break;
                }

                danceKeyGroup.SetKeySprite(cnt++, sprite);
            }
        }
    }
}
