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
}

public static class DanceSequencePresets {

    private static DanceSequence[] easyDanceSequences = new DanceSequence[] {
        new DanceSequence {
            move1 = DanceType.UP,
            move5 = DanceType.DOWN,
        },
        //new DanceSequence {
        //    move1 = DanceType.LEFT,
        //    move5 = DanceType.RIGHT,
        //},
    };

    private static DanceSequence[] mediumDanceSequences = new DanceSequence[] {
        new DanceSequence {
            move1 = DanceType.UP,
            move3 = DanceType.DOWN,
            move5 = DanceType.UP,
            move7 = DanceType.TWIST,
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
            move6 = DanceType.NONE,
            move7 = DanceType.DOWN,
            move8 = DanceType.NONE,
            move9 = DanceType.RIGHT,
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
