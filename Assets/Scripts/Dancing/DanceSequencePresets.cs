using UnityEngine;

public enum DanceSequenceDifficulty {
    EASY,
    MEDIUM,
    HARD,
}

public enum DanceType {
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
public class DanceSequence {
    public DanceType move1 = DanceType.NONE;
    public DanceType move2 = DanceType.NONE;
    public DanceType move3 = DanceType.NONE;
    public DanceType move4 = DanceType.NONE;
    public DanceType move5 = DanceType.NONE;
    public DanceType move6 = DanceType.NONE;
    public DanceType move7 = DanceType.NONE;
    public DanceType move8 = DanceType.NONE;
    public DanceType move9 = DanceType.NONE;

    public DanceType GetMoveFromIndex(int index) {
        switch (index) {
            case 1: return move1;
            case 2: return move2;
            case 3: return move3;
            case 4: return move4;
            case 5: return move5;
            case 6: return move6;
            case 7: return move7;
            case 8: return move8;
            case 9: return move9;
            default: return DanceType.NONE;
        }
    }

    public void SetMoveFromIndex(int index, DanceType type) {
        switch (index) {
            case 1: move1 = type; break;
            case 2: move2 = type; break;
            case 3: move3 = type; break;
            case 4: move4 = type; break;
            case 5: move5 = type; break;
            case 6: move6 = type; break;
            case 7: move7 = type; break;
            case 8: move8 = type; break;
            case 9: move9 = type; break;
            default: break;
        }
    }
}

public static class DanceSequencePresets {

    private static DanceSequence[] easyDanceSequences = new DanceSequence[] {
        new DanceSequence {
            move1 = DanceType.UP,
            move5 = DanceType.DOWN,
        },
        new DanceSequence {
            move1 = DanceType.LEFT,
            move5 = DanceType.RIGHT,
        },
        new DanceSequence {
            move1 = DanceType.TWIST,
            move5 = DanceType.POSE,
        },
    };

    private static DanceSequence[] mediumDanceSequences = new DanceSequence[] {
        new DanceSequence {
            move1 = DanceType.UP,
            move3 = DanceType.DOWN,
            move5 = DanceType.UP,
            move7 = DanceType.TWIST,
        },
        new DanceSequence {
            move1 = DanceType.POSE,
            move3 = DanceType.TWIST,
            move4 = DanceType.LEFT,
            move6 = DanceType.RIGHT,
        },
        new DanceSequence {
            move1 = DanceType.TWIST,
            move3 = DanceType.UP,
            move4 = DanceType.RIGHT,
            move6 = DanceType.DOWN,
        },
    };

    private static DanceSequence[] hardDanceSequences = new DanceSequence[] {
        new DanceSequence {
            move1 = DanceType.UP,
            move2 = DanceType.DOWN,
            move3 = DanceType.UP,
            move4 = DanceType.DOWN,
            move5 = DanceType.LEFT,
            move6 = DanceType.RIGHT,
            move9 = DanceType.TWIST,
        },
        new DanceSequence {
            move1 = DanceType.UP,
            move2 = DanceType.DOWN,
            move3 = DanceType.UP,
            move4 = DanceType.DOWN,
            move5 = DanceType.UP,
            move7 = DanceType.TWIST,
            move9 = DanceType.POSE,
        },
        new DanceSequence {
            move1 = DanceType.POSE,
            move3 = DanceType.RIGHT,
            move4 = DanceType.UP,
            move5 = DanceType.TWIST,
            move7 = DanceType.DOWN,
            move8 = DanceType.UP,
            move9 = DanceType.RIGHT,
        },
        new DanceSequence {
            move1 = DanceType.UP,
            move3 = DanceType.LEFT,
            move4 = DanceType.DOWN,
            move6 = DanceType.LEFT,
            move7 = DanceType.UP,
            move9 = DanceType.RIGHT,
        },
        new DanceSequence {
            move1 = DanceType.DOWN,
            move2 = DanceType.UP,
            move3 = DanceType.RIGHT,
            move5 = DanceType.RIGHT,
            move7 = DanceType.DOWN,
            move9 = DanceType.LEFT,
        },
    };

    public static DanceSequence GetDanceSequence(DanceSequenceDifficulty difficulty) {
        switch (difficulty) {
            case DanceSequenceDifficulty.EASY: 
                return easyDanceSequences[Random.Range(0, easyDanceSequences.Length)];
            case DanceSequenceDifficulty.MEDIUM:
                return mediumDanceSequences[Random.Range(0, mediumDanceSequences.Length)];
            case DanceSequenceDifficulty.HARD:
                return hardDanceSequences[Random.Range(0, hardDanceSequences.Length)];
            default: return null;
        }
    }

}
