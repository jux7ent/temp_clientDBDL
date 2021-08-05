using System;

public enum ECharacter {
    Shade, //0
    Shadow, //1
    SpiderKing, //2
    Spook, //3
    
    Egglet, //4
    Bird
}

public static class CharacterHelper {
    private static Random random;
    private static int escapersStartIndex = -1;
    private static int charactersCount = -1;
    
    public static bool IsCatcher(ECharacter eCharacter) {
        return (int) eCharacter < GetEscapersStartIndex();
    }

    public static int GetEscapersStartIndex() {
        if (escapersStartIndex == -1) {
            escapersStartIndex = (int) ECharacter.Egglet;
        }

        return escapersStartIndex;
    }

    public static int GetCharactersCount() {
        if (charactersCount == -1) {
            charactersCount = Enum.GetValues(typeof(ECharacter)).Length;
        }
        
        return charactersCount;
    }

    public static ECharacter GetRandomCatcher() {
        return (ECharacter)(GetRandomNumber(0, GetEscapersStartIndex()));
    }

    public static ECharacter GetRandomEscaper() {
        return (ECharacter)(GetRandomNumber(GetEscapersStartIndex(), GetCharactersCount()));
    }

    public static ECharacter GetRandomCharacter() {
        return (ECharacter) (GetRandomNumber(0, GetCharactersCount()));
    }

    private static int GetRandomNumber(int startIndex, int endIndex) {
        if (random == null) {
            random = new Random();
        }

        return random.Next(startIndex, endIndex);
    }
}